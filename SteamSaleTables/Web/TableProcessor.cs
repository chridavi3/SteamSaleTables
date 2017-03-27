using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using RedditSharp;

namespace SteamSaleTables
{
    /// <summary>
    /// Manages creating Reddit formatted tables for app data used in templates.
    /// </summary>
    class TableProcessor
    {
        /// <summary>
        /// Represents datasets and conditions corresponding to a particular table read from a template.
        /// </summary>
        private struct Table
        {
            /// <summary>
            /// String representations of the datasets contained in the table.
            /// </summary>
            public List<string> DataSets;
            /// <summary>
            /// List of conditions in the table.
            /// </summary>
            public List<Condition> Conditions;
        }

        /// <summary>
        /// Table condition to determine whether a game can fit in the table.
        /// </summary>
        private struct Condition
        {
            /// <summary>
            /// Variable to check.
            /// </summary>
            public AppVariable Variable;
            /// <summary>
            /// Operator to use against the variable.
            /// </summary>
            public ConditionOperator Operator;
            /// <summary>
            /// Value to compare the variable against.
            /// </summary>
            public string Value;
        }

        /// <summary>
        /// Valid operators that can be used by conditions.
        /// </summary>
        private enum ConditionOperator
        {
            Equal, NotEqual
        }

        /// <summary>
        /// Valid variables that can be used by conditions.
        /// </summary>
        private enum AppVariable
        {
            Developer, Publisher, Discount, Description
        }

        /// <summary>
        /// Header to be used in creatting Reddit tables from templates.
        /// </summary>
        private const string TableHeader = "Name | Price | Sale | Lowest | Rating  \n----|----|----|----|----|---- \n";


        /// <summary>
        /// Processes a template file using app data. 
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="currencies"></param>
        /// <param name="reddit"></param>
        public static void UpdateReddit(SortedList<string, SortedList<string, SortedList<string, App>>> groups,
            List<Currency> currencies, Reddit reddit, string subreddit)
        {
            if (groups == null)
            {
                throw new ArgumentNullException("groups");
            }
            try
            {
                var sub = reddit.GetSubreddit("/r/" + subreddit);

                var groupNames = groups.Keys;

                foreach (var groupName in groupNames)
                {
                    var paths = FileManager.GetTemplatePaths(groupName);

                    foreach (var path in paths)
                    {
                        var input = FileManager.ReadTemplateLines(path);

                        foreach (var currency in currencies)
                        {
                            var str = input.Aggregate(string.Empty,
                                (current, line) => current + ProcessLine(line, groups[groupName], currency) + '\n');

                            sub.Wiki.EditPage((currency.Abbreviation + '/' + path).Replace('\\', '/'), str);
                        }
                    }
                }

            }
            catch (WebException)
            {
                return;
            }

        }

        /// <summary>
        /// Processes a line of a template file.
        /// </summary>
        /// <param name="line">String containing a line to process</param>
        /// <param name="subgroups">Dataset containing the data from main and alternate dataset combined</param>
        /// <param name="currency"></param>
        /// <returns>The processed line</returns>
        public static string ProcessLine(string line, SortedList<string, SortedList<string, App>> subgroups, Currency currency)
        {
            // Check to see if the line needs to be formatted
            if (!line.StartsWith("|||") || !line.EndsWith("|||"))
            {
                return line;
            }

            var table = ReadTable(line);


            if (table.DataSets.Count == 0)
            {
                return line;
            }

            // Pointer to dataset to start from
            List<App> apps;

            // If there is only one dataset, just use a pointer
            if (table.DataSets.Count == 1)
            {
                // Determine dataset to start with based on first arg
                apps = subgroups[table.DataSets[0].ToUpper()].Values.ToList();

            }
            // If there are multiple datasets included, need to create a new dataset that includes all of them
            else
            {
                apps = new List<App>();

                foreach (var dataSet in table.DataSets)
                {
                    apps.AddRange(subgroups[dataSet.ToUpper()].Values.ToList());
                }
            }

            var finalApps =
                (from app in apps where table.Conditions.All(cond => ConditionPass(cond, app, currency)) select app)
                    .ToList();

            return CreateTable(finalApps, currency);
        }

        /// <summary>
        /// Checks if a condition passes against an app.
        /// </summary>
        /// <param name="condition">Condition to check the app against</param>
        /// <param name="app">App to check against the condition</param>
        /// <param name="currency"></param>
        /// <returns>True if the app meets the condition</returns>
        private static bool ConditionPass(Condition condition, App app, Currency currency)
        {
            string var;

            switch (condition.Variable)
            {
                case AppVariable.Developer:
                    switch (condition.Operator)
                    {
                        case ConditionOperator.Equal:
                            return app.Developers.Contains(condition.Value, StringComparer.OrdinalIgnoreCase);
                        case ConditionOperator.NotEqual:
                            return !app.Developers.Contains(condition.Value, StringComparer.OrdinalIgnoreCase);
                        default:
                            return true;
                    }
                case AppVariable.Publisher:
                    switch (condition.Operator)
                    {
                        case ConditionOperator.Equal:
                            return app.Publishers.Contains(condition.Value, StringComparer.OrdinalIgnoreCase);
                        case ConditionOperator.NotEqual:
                            return !app.Publishers.Contains(condition.Value, StringComparer.OrdinalIgnoreCase);
                        default:
                            return true;
                    }
                case AppVariable.Discount:
                    var = app.PriceInfo[currency.Abbreviation].LowestDiscount.Trim().ToUpper();
                    break;
                default:
                    return true;
            }


            switch (condition.Operator)
            {
                case ConditionOperator.NotEqual:
                    return var != condition.Value;
                case ConditionOperator.Equal:
                    return var == condition.Value;
                default:
                    return true;
            }
        }

        /// <summary>
        /// Creates a Reddit formatted table using the passed apps.
        /// </summary>
        /// <param name="apps">Dataset of apps to use in table creation</param>
        /// <param name="currency"></param>
        /// <returns>String representation of the table to create</returns>
        private static string CreateTable(IEnumerable<App> apps, Currency currency)
        {
            // Add a row for each app to the table header
            return apps.Aggregate(TableHeader, (current, app) => current + GetTableRow(app, currency));
        }

        /// <summary>
        /// Regex that splits the datasets and the conditions.
        /// </summary>
        private static readonly Regex TableDataRegex = new Regex(@"\|\|", RegexOptions.Compiled);

        /// <summary>
        /// Reads a template string representing a table and provides the corresponding table.
        /// </summary>
        /// <param name="tableStr">String of data representing a table</param>
        /// <returns>Table structure that allows a table to be created easily</returns>
        private static Table ReadTable(string tableStr)
        {
            var t = new Table
            {
                DataSets = new List<string>(),
                Conditions = new List<Condition>()
            };

            // Remove '|||' from each side of the string
            tableStr = tableStr.Substring(3, tableStr.Length - 6).ToUpper();

            // Split the data into datasets and conditions
            var tableParts = TableDataRegex.Split(tableStr);

            // Read dataSet names split by '|' (ignoring escaped '|')
            var dataSets = Regex.Matches(tableParts[0], @"(?:\\.|[^\|\\]+)+");


            foreach (Match dataSet in dataSets)
            {
                t.DataSets.Add(dataSet.Value);
            }

            // Ensure that there are conditions
            if (tableParts.Length <= 1)
            {
                return t;
            }

            // Read all conditions seperated by '|'
            var conditions = Regex.Matches(tableParts[1], @"(?:\\.|[^\|\\]+)+");


            foreach (Match condition in conditions)
            {
                var cond = new Condition();

                // Read the parts of the condition in the format: "variable operator value"
                var condParts = Regex.Match(condition.Value, @"((?:\\.|[^=\!\\]+)+)(!=|==)((?:\\.|[^=\!\\]+)+)").Groups;

                // A valid condition has 4 parts (full match, variable, operator, value)
                if (condParts.Count < 4)
                {
                    continue;
                }

                // Read variable from the condition
                switch (condParts[1].Value.Trim())
                {
                    case "DEV":
                        cond.Variable = AppVariable.Developer;
                        break;
                    case "PUB":
                        cond.Variable = AppVariable.Publisher;
                        break;
                    case "DISC":
                        cond.Variable = AppVariable.Discount;
                        break;
                    case "DESC":
                        cond.Variable = AppVariable.Description;
                        break;
                    default:
                        continue;
                }

                // Read operator from the condition
                switch (condParts[2].Value.Trim())
                {
                    case "==":
                        cond.Operator = ConditionOperator.Equal;
                        break;
                    case "!=":
                        cond.Operator = ConditionOperator.NotEqual;
                        break;
                    default:
                        continue;
                }

                // Read comparison value from the condition
                cond.Value = condParts[3].Value.Trim();


                t.Conditions.Add(cond);
            }

            return t;
        }

        /// <summary>
        /// Creates a table row with an app's data.
        /// </summary>
        /// <param name="app">App to create a row for</param>
        /// <param name="currency"></param>
        /// <returns>A Reddit formatted string representing the app row</returns>
        private static string GetTableRow(App app, Currency currency)
        {
            if (!app.PriceInfo.ContainsKey(currency.Abbreviation))
            {
                return string.Empty;
            }

            var priceInfo = app.PriceInfo[currency.Abbreviation];

            return string.Format("{0} | {1} | {2} | {3} \n", GetAppTableTitle(app),
                GetAppTablePrice(priceInfo, currency), GetAppTableDiscounts(priceInfo), GetAppTableRatings(app));
        }

        private static string GetAppTableTitle(App app)
        {
            return string.Format("[{0}](http://store.steampowered.com/app/{1})", app.Title, app.Id);
        }

        private static string GetAppTablePrice(PriceData priceInfo, Currency currency)
        {
            return string.Format("{0}", GetPriceString(priceInfo.Price, currency));
        }

        private static string GetAppTableDiscounts(PriceData priceInfo)
        {
            int lowestDiscount, discount;
            int.TryParse(priceInfo.LowestDiscount, out lowestDiscount);
            int.TryParse(priceInfo.Discount, out discount);

            if (discount == 0)
            {
                return string.Format("*N/A* | -{0}%", priceInfo.LowestDiscount);
            }
            else if (discount > lowestDiscount)
            {
                return string.Format("**-{0}%** | -{1}%", priceInfo.Discount, priceInfo.LowestDiscount);
            }
            else if (discount == lowestDiscount)
            {
                return string.Format("-{0}% | same", priceInfo.Discount);
            }
            else
            {
                return string.Format("*-{0}%* | -{1}%", priceInfo.Discount, priceInfo.LowestDiscount);
            }
        }

        private static string GetAppTableRatings(App app)
        {
            int ratingCount;
            int.TryParse(app.RatingCount, out ratingCount);

            if (ratingCount >= 100)
            {
                return string.Format("**{0}%**", app.Rating);
            }
            else if (ratingCount >= 20)
            {
                return string.Format("{0}%", app.Rating);
            }
            else
            {
                return string.Format("*{0}%*", app.Rating);
            }
        }



        private static string GetPriceString(string price, Currency currency)
        {
            if (string.IsNullOrEmpty(price))
            {
                return string.Empty;
            }

            price = price.Replace(",", "");
            price = price.Replace(".", "");

            if (string.IsNullOrEmpty(currency.PriceDecimal))
            {
                price = price.Substring(0, price.Length - 2);
            }
            else
            {
                price = price.Substring(0, price.Length - 2) + currency.PriceDecimal +
                        price.Substring(price.Length - 2, 2);
            }

            price = string.Format(currency.PriceFormat, price);

            if (price.Length < 6)
                price = '\'' + price;

            return price;
        }
    }
}

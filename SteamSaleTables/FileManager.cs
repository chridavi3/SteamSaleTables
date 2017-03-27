using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using RedditSharp;

namespace SteamSaleTables
{
    /// <summary>
    /// Manages the files read in and written to by the program.
    /// </summary>
    class FileManager
    {
        /// <summary>
        /// Path from the current user directory to the directory 
        /// where all of this program's data can be found.
        /// </summary>
        private const string ProgramPath = @"\Programs\SalesData\Steam\";

        private static string _mainPath = string.Empty;
        /// <summary>
        /// Main directory where all of this program's data can be found.
        /// </summary>
        private static string MainPath
        {
            get
            {
                if (_mainPath != string.Empty)
                    return _mainPath;

                var path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
                if (Environment.OSVersion.Version.Major >= 6)
                    path = Directory.GetParent(path).ToString();

                _mainPath = path + ProgramPath;

                if (!Directory.Exists(_mainPath))
                {
                    Directory.CreateDirectory(_mainPath);
                }

                return _mainPath;
            }
        }

        private const string CurrenciesFile = @"_currencies.tsv";

        private const string DataFile = @"_data.tsv";

        private const string PriceFile = @"_{0}.tsv";

        private const string RatingFile = @"_ratings.tsv";

        private const string TemplateFile = @"_template.txt";

        private const string RedditFile = @"_reddit.txt";

        private const string ItadFile = @"_itad.txt";

        private const string SubredditFile = @"_subreddit.txt";

        /// <summary>
        /// Reads all groups, their subgroups, and their apps.
        /// Groups are represented by folders off of the main program path.
        /// Subgroups are represented by CSVs, containing all app ids in that subgroup.
        /// </summary>
        /// <param name="allApps">
        /// SortedList containing all apps, where the keys are the app ids
        /// </param>
        /// <returns>
        /// A SortedList containing groups, where the keys are the group names.
        /// The groups are represented by SortedLists containing subgroups, 
        /// where the keys are the subgroup names.
        /// The subgroups are represented by SortedLists containing apps, 
        /// where the keys are the app ids.
        /// </returns>
        public static SortedList<string, SortedList<string, SortedList<string, App>>> ReadGroups(
            IDictionary<string, App> allApps)
        {
            var groups = new SortedList<string, SortedList<string, SortedList<string, App>>>();

            var dirs = Directory.GetDirectories(MainPath);

            foreach (var dir in dirs)
            {
                groups.Add(dir.Replace(MainPath, "").ToUpper(), ReadSubgroups(dir + '\\', allApps));
            }

            return groups;
        }

        /// <summary>
        /// Adds a group to the program's file system through the adding of its folder.
        /// </summary>
        /// <param name="group">Name of the group to add</param>
        public static void AddGroup(string group)
        {
            var groupPath = MainPath + group;

            if (!Directory.Exists(groupPath))
            {
                Directory.CreateDirectory(groupPath);
            }
        }

        /// <summary>
        /// Reads all subgroups and their apps.
        /// Subgroups are represented by CSVs, containing all app ids in that subgroup.
        /// </summary>
        /// <param name="path">Path to the group folder</param>
        /// <param name="allApps">SortedList containing all apps, where the keys are the app ids</param>
        /// <returns>A SortedList representing the group containing the subgroups read.
        /// The group is represented by a SortedList containing subgroups, 
        /// where the keys are the subgroup names.
        /// The subgroups are represented by SortedLists containing apps, 
        /// where the keys are the app ids.</returns>
        private static SortedList<string, SortedList<string, App>> ReadSubgroups(string path,
            IDictionary<string, App> allApps)
        {
            var subgroups = new SortedList<string, SortedList<string, App>>();

            var files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                var filename = file.Replace(path, "");

                if (filename.Length <= 4 || filename.Substring(filename.Length - 4) != ".csv")
                {
                    continue;
                }

                filename = filename.Substring(0, filename.Length - 4);

                if (filename[0] != '_')
                {
                    subgroups.Add(filename.ToUpper(), ReadAppSubgroup(file, allApps));
                }
            }

            return subgroups;
        }

        /// <summary>
        /// Reads a subgroup of apps stored as a CSV containing their app ids.
        /// </summary>
        /// <param name="path">Path to the subgroup CSV to read</param>
        /// <param name="allApps">SortedList containing all apps, where the keys are the app ids</param>
        /// <returns>A sortedList containing apps,  where the keys are the app ids.</returns>
        private static SortedList<string, App> ReadAppSubgroup(string path, IDictionary<string, App> allApps)
        {
            var apps = new SortedList<string, App>();

            var ids = File.ReadAllText(path, Encoding.UTF8).Split(',');

            foreach (var id in ids.Where(App.ValidAppId))
            {
                if (!allApps.ContainsKey(id))
                {
                    allApps.Add(id, new App(id));
                }

                apps.Add(id, allApps[id]);
            }

            return apps;
        }

        /// <summary>
        /// Adds a subgroup to the group by adding a csv file with the
        /// subgroups name to the group folder.
        /// </summary>
        /// <param name="group">Name of group to add the subgroup to</param>
        /// <param name="subgroup">Name of subgroup to add</param>
        public static void AddSubgroup(string group, string subgroup)
        {
            var subgroupPath = string.Format("{0}{1}\\{2}.csv", MainPath, group, subgroup);

            if (!File.Exists(subgroupPath))
            {
                File.Create(subgroupPath);
            }
        }

        /// <summary>
        /// Writes a list of apps into a subgroup as a CSV containing their app ids.
        /// </summary>
        /// <param name="group">Name of group containing the subgroup</param>
        /// <param name="subgroup">Name of subgroup to write to</param>
        /// <param name="apps">Apps to write in the subgroup</param>
        public static void WriteSubgroup(string group, string subgroup, App[] apps)
        {
            var subgroupPath = string.Format("{0}{1}\\{2}.csv", MainPath, group, subgroup);

            var str = apps.Aggregate(string.Empty, (current, app) => current + app.Id + ',');

            File.WriteAllText(subgroupPath, str, Encoding.UTF8);
        }

        /// <summary>
        /// Reads basic app data from a data file.
        /// </summary>
        /// <returns>A SortedList containing apps where the key is the app id.</returns>
        public static SortedList<string, App> ReadAppData()
        {
            var dataFile = MainPath + DataFile;

            if (!File.Exists(dataFile))
            {
                return new SortedList<string, App>();
            }


            var appLines = ParseData(File.ReadAllText(dataFile, Encoding.UTF8));

            var apps = new SortedList<string, App>();

            foreach (var appLine in appLines)
            {
                var devs = new List<string>();
                var pubs = new List<string>();

                for (var i = 2; i < appLine.Length - 1; i++)
                {
                    if (appLine[i].Length <= 0)
                    {
                        continue;
                    }

                    switch (appLine[i][0])
                    {
                        case 'd':
                            devs.Add(appLine[i].Substring(1, appLine[i].Length - 1));
                            break;
                        case 'p':
                            pubs.Add(appLine[i].Substring(1, appLine[i].Length - 1));
                            break;
                    }
                }


                apps.Add(appLine[0], new App(appLine[0], appLine[1], devs.ToArray(), pubs.ToArray(), appLine[appLine.Length - 1]));
            }

            apps = ReadRatings(apps);

            return apps;
        }

        /// <summary>
        /// Writes basic app data to the data file.
        /// </summary>
        /// <param name="apps">Apps to write data for</param>
        /// <returns>True if successful</returns>
        public static bool WriteAppData(App[] apps)
        {
            var str = apps.Aggregate(string.Empty, (current, app) => current + app.GetDataString() + '\n');

            File.WriteAllText(MainPath + DataFile, str, Encoding.UTF8);

            return true;
        }

        /// <summary>
        /// Reads all currencies used by the program.
        /// </summary>
        /// <returns>A list containing the currencies read from the file.</returns>
        public static List<Currency> ReadCurrencies()
        {
            var currenciesFile = MainPath + CurrenciesFile;

            if (!File.Exists(currenciesFile))
            {
                currenciesFile = "_defaultcurrency.tsv";
            }

            var input = File.ReadAllText(currenciesFile, Encoding.UTF8);

            var currencyLines = input.Split('\n');

            return (from currencyParts in currencyLines.
                        Select(currencyLine => currencyLine.Split('\t'))
                    where currencyParts.Count() >= 5
                    select new Currency
                    {
                        Abbreviation = currencyParts[0],
                        CountryCode = currencyParts[1],
                        ItadCc = currencyParts[2],
                        PriceFormat = currencyParts[3].Replace("X", "{0}"),
                        PriceDecimal = currencyParts[4].Replace("\r", "")
                    }).ToList();
        }

        /// <summary>
        /// Reads price data from files for each currency.
        /// </summary>
        /// <param name="apps">SortedList of apps, where the key is the app id, to update</param>
        /// <param name="currencies">Currencies to read prices for</param>
        /// <returns>An updated SortedList of apps, where the key is the app id</returns>
        public static SortedList<string, App> ReadPrices(SortedList<string, App> apps, List<Currency> currencies)
        {
            var pricesFormat = MainPath + PriceFile;

            foreach (
                var currency in
                    currencies.Where(currency => File.Exists(string.Format(pricesFormat, currency.Abbreviation))))
            {
                var appLines =
                    ParseData(File.ReadAllText(string.Format(pricesFormat, currency.Abbreviation), Encoding.UTF8));

                foreach (var appLine in appLines)
                {
                    if (apps.ContainsKey(appLine[0]))
                    {
                        var app = apps[appLine[0]];

                        if (app.PriceInfo.ContainsKey(currency.Abbreviation))
                        {
                            app.PriceInfo[currency.Abbreviation] = new PriceData
                            {
                                Price = appLine[1],
                                LowestPrice = appLine[2],
                                Discount = appLine[3],
                                LowestDiscount = appLine[4]
                            };
                        }
                        else
                        {
                            app.PriceInfo.Add(currency.Abbreviation, new PriceData
                            {
                                Price = appLine[1],
                                LowestPrice = appLine[2],
                                Discount = appLine[3],
                                LowestDiscount = appLine[4]
                            });
                        }
                    }
                    else
                    {
                        var app = new App(appLine[0]);
                        app.PriceInfo.Add(currency.Abbreviation, new PriceData
                        {
                            Price = appLine[1],
                            LowestPrice = appLine[2],
                            Discount = appLine[3],
                            LowestDiscount = appLine[4]
                        });

                        apps.Add(appLine[0], app);
                    }
                }
            }

            return apps;
        }

        /// <summary>
        /// Writes price data to files for each currency.
        /// </summary>
        /// <param name="apps">Apps to write data for</param>
        /// <param name="currencies">Currencies to write prices for</param>
        /// <returns>True if successful</returns>
        public static bool WritePrices(List<App> apps, List<Currency> currencies)
        {
            var pricesFormat = MainPath + PriceFile;

            foreach (var currency in currencies)
            {
                var str = apps.Select(app => app.GetPriceString(currency))
                    .Where(price => price != string.Empty)
                    .Aggregate(string.Empty, (current, price) => current + price + '\n');

                File.WriteAllText(string.Format(pricesFormat, currency.Abbreviation), str, Encoding.UTF8);
            }

            return true;
        }


        /// <summary>
        /// Reads ratings from a file.
        /// </summary>
        /// <param name="apps">SortedList of apps, where the key is the app id, to update</param>
        /// <returns>An updated SortedList of apps, where the key is the app id</returns>
        public static SortedList<string, App> ReadRatings(SortedList<string, App> apps)
        {
            var ratingsFile = MainPath + RatingFile;

            if (!File.Exists(ratingsFile))
            {
                return null;
            }


            var appLines = ParseData(File.ReadAllText(ratingsFile, Encoding.UTF8));

            foreach (var appLine in appLines) {
                if (apps.ContainsKey(appLine[0]))
                {
                    var app = apps[appLine[0]];

                    app.Rating = appLine[1];
                    app.RatingCount = appLine[2];
                }
                else
                {
                    apps.Add(appLine[0], new App(appLine[0], appLine[1], appLine[2]));
                }
            }

            return apps;
        }



        /// <summary>
        /// Writes rating data to the rating file.
        /// </summary>
        /// <param name="apps">Apps to write data for</param>
        /// <returns>True if successful</returns>
        public static bool WriteRatings(List<App> apps)
        {
            var ratingPath = MainPath + RatingFile;

            var str = apps.Aggregate(string.Empty, (current, app) => current + app.GetRatingString() + '\n');

            File.WriteAllText(ratingPath, str, Encoding.UTF8);

            return true;
        }

        /// <summary>
        /// Reads Reddit API information from table and logs in with it.
        /// </summary>
        /// <returns>Logged in Reddit instance, or null if invalid</returns>
        public static Reddit ReadReddit()
        {
            var redditFilePath = MainPath + RedditFile;

            if (!File.Exists(redditFilePath))
            {
                return null;
            }

            var lines = File.ReadAllLines(redditFilePath, Encoding.UTF8);

            if (lines.Length < 5)
            {
                return null;
            }

            try
            {
                var webAgent = new BotWebAgent(lines[0], lines[1], lines[2], lines[3], lines[4]);

                return new Reddit(webAgent, true) { RateLimit = WebAgent.RateLimitMode.Burst };
            }
            catch (WebException)
            {
                return null;
            }
        }

        /// <summary>
        /// Writes the Reddit API information to file.
        /// </summary>
        /// <param name="user">Reddit username</param>
        /// <param name="pass">User's password</param>
        /// <param name="clientId">Client ID</param>
        /// <param name="clientSecret">Client Secret</param>
        /// <param name="redirectUri">Redirect UI</param>
        public static void WriteReddit(string user, string pass, string clientId, string clientSecret,
            string redirectUri)
        {
            var str = string.Format("{0}\n{1}\n{2}\n{3}\n{4}", user, pass, clientId, clientSecret, redirectUri);

            File.WriteAllText(MainPath + RedditFile, str, Encoding.UTF8);
        }

        /// <summary>
        /// Reads the subreddit to update.
        /// </summary>
        /// <returns>True if successful</returns>
        public static string ReadSubreddit()
        {
            var subredditFilePath = MainPath + SubredditFile;

            return File.Exists(subredditFilePath) ? File.ReadAllText(subredditFilePath, Encoding.UTF8) : null;
        }

        /// <summary>
        /// Writes the subreddit name to file.
        /// </summary>
        /// <param name="subreddit">Subreddit to store</param>
        public static void WriteSubreddit(string subreddit)
        {
            var subredditFilePath = MainPath + SubredditFile;

            File.WriteAllText(subredditFilePath, subreddit, Encoding.UTF8);
        }

        /// <summary>
        /// Reads the ITAD Key from the ITAD Key file.
        /// </summary>
        /// <returns>The ITAD Key stored in the ITAD Key file</returns>
        public static string ReadItad()
        {
            var itadFilePath = MainPath + ItadFile;

            return File.Exists(itadFilePath) ? File.ReadAllText(itadFilePath, Encoding.UTF8) : null;
        }

        /// <summary>
        /// Writes the passed ITAD Key to the ITAD Key file.
        /// </summary>
        /// <param name="itadKey">ITAD Key to write to</param>
        public static void WriteItad(string itadKey)
        {
            File.WriteAllText(MainPath + ItadFile, itadKey, Encoding.UTF8);
        }

        /// <summary>
        /// Reads all lines of a template file.
        /// </summary>
        /// <param name="path">Path to subdirectory containing the template</param>
        /// <returns>An array of all lines in the template</returns>
        public static string[] ReadTemplateLines(string path)
        {
            var templateFilePath = MainPath + path + '\\' + TemplateFile;

            return File.ReadAllLines(templateFilePath, Encoding.UTF8);
        }

        /// <summary>
        /// Gets the path to all directories containg a template within the directory pointed to by the path.
        /// </summary>
        /// <param name="path">Path pointing to the directory to search for directories in</param>
        /// <returns>A list containing paths to all directories within the directory that contain templates</returns>
        public static List<string> GetTemplatePaths(string path = "")
        {
            var paths = new List<string>();


            if (File.Exists(MainPath + path + '\\' + TemplateFile))
            {
                paths.Add(path);
            }

            var dirs = Directory.GetDirectories(MainPath + path);

            foreach (var dir in dirs)
            {
                paths.AddRange(GetTemplatePaths(dir.Replace(MainPath, "")));
            }

            return paths;
        }

        /// <summary>
        /// Parses the data using the '\t' character as the delimeter.
        /// The first element in the string array is expected to be an app id, and must be a six digit number.
        /// The others can be variable.
        /// </summary>
        /// <param name="data">Data to read in and parse</param>
        /// <returns>A list of arrays of strings for each group of data</returns>
        private static IEnumerable<string[]> ParseData(string data)
        {
            data = data.Replace("\r", "");
            var strs = data.Split('\n');
            var output = new List<string[]>(strs.Length + 10);
            output.AddRange(strs.Select(str => str.Split('\t')).Where(x => App.ValidAppId(x[0])));

            return output;
        }
    }
}

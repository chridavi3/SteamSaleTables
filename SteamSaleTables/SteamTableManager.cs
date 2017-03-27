using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using RedditSharp;
using SteamSaleTables.Web;

namespace SteamSaleTables
{
    /// <summary>
    /// Class responsible for managing all tasks the UI must perform.
    /// </summary>
    class SteamTableManager
    {
        private static SortedList<string, SortedList<string, SortedList<string, App>>> _groups;
        private static List<Currency> _currencies;
        private static Currency _defaultCurrency;
        private static SortedList<string, App> _apps;
        private static Reddit _reddit;
        private static string _subreddit;
        private static string _itadKey;
        private static RichTextBox _console;
        private static Thread _thUpdateReviewData, _thUpdatePrices, _thUpdateAppData, _thUpdateReddit;
        private static bool _thUpdateReviewDataFinished, _thUpdatePricesFinished, _thUpdateAppDataFinished, _thUpdateRedditFinished;


        /// <summary>
        /// Initializes the SteamTableManager class.  
        /// Must be ran before any other method of the SteamTableManager can be ran reliably. 
        /// </summary>
        /// <param name="console">Console to print output data to</param>
        public static void InitializeSteamTableManager(RichTextBox console)
        {
            _console = console;
            _currencies = FileManager.ReadCurrencies();
            _defaultCurrency = _currencies[0];
            _apps = FileManager.ReadAppData();
            _groups = FileManager.ReadGroups(_apps);
            ConsoleWriteLine(string.Format("{0} apps and {1} groups read.", _apps.Count, _groups.Count),
                PrintType.Notice);
            _apps = FileManager.ReadPrices(_apps, _currencies);
            _itadKey = FileManager.ReadItad();
            _reddit = FileManager.ReadReddit();
            _subreddit = FileManager.ReadSubreddit();

            ConsoleWriteLine(
                _reddit != null
                    ? string.Format("Connected to reddit account {0}.", _reddit.User)
                    : "Could not connect connect to reddit.",
                PrintType.Notice);
        }

        /// <summary>
        /// Gets the current default currency.
        /// Initialized to the first currency read in the currency file.
        /// </summary>
        /// <returns>Return the default currency.</returns>
        public static Currency GetDefaultCurrency()
        {
            return _defaultCurrency;
        }

        /// <summary>
        /// Sets the default currency.
        /// </summary>
        /// <param name="abbreviation">String abbreviation of the currency to set as default</param>
        /// <returns>True if successful</returns>
        public static bool SetDefaultCurrency(string abbreviation)
        {
            foreach (var currency in _currencies.Where(currency => currency.Abbreviation == abbreviation)) 
            {
                _defaultCurrency = currency;

                ConsoleWriteLine(string.Format("Default currency set to {0}.", abbreviation), PrintType.Success);

                return true;
            }


            ConsoleWriteLine(string.Format("Could not set default currency to {0}.", abbreviation), PrintType.Error);

            return false;
        }

        /// <summary>
        /// Adds an app to a subgroup.
        /// </summary>
        /// <param name="group">Group containing the subgroup</param>
        /// <param name="subgroup">Subgroup to add to</param>
        /// <param name="appId">App ID of the app to add</param>
        /// <returns>True if succesful</returns>
        public static bool AddApp(string group, string subgroup, string appId)
        {
            if (!App.ValidAppId(appId))
            {
                ConsoleWriteLine(string.Format("App ID {0} is invalid.", appId), PrintType.Error);

                return false;
            }

            if (!_apps.ContainsKey(appId))
                _apps.Add(appId, new App(appId));

            var app = _apps[appId];


            if (_groups[group][subgroup].ContainsKey(appId))
            {
                ConsoleWriteLine(
                    string.Format("App ID {0} already exists in the {1} subgroup in the {2} group.", appId, subgroup,
                        group), PrintType.Error);

                return false;
            }

            _groups[group][subgroup].Add(appId, app);

            FileManager.WriteSubgroup(group, subgroup, _groups[group][subgroup].Values.ToArray());

            ConsoleWriteLine(
                string.Format("Added App with ID {0} to the {1} subgroup in the {2} group.", appId, subgroup, group),
                PrintType.Success);

            return true;
        }

        /// <summary>
        /// Removes an app from a subgroup.
        /// </summary>
        /// <param name="group">Group containing the subgroup</param>
        /// <param name="subgroup">Subgroup to remove from</param>
        /// <param name="appId">App ID of the app to remove</param>
        /// <returns>True if succesful</returns>
        public static bool RemoveApp(string group, string subgroup, string appId)
        {
            if (!_groups[group][subgroup].ContainsKey(appId))
            {
                ConsoleWriteLine(
                    string.Format("Could not remove an app with ID {0} from the {1} subgroup in the {2} group.", appId,
                        subgroup, group), PrintType.Success);

                return false;
            }

            _groups[group][subgroup].Remove(appId);

            FileManager.WriteSubgroup(group, subgroup, _groups[group][subgroup].Values.ToArray());

            ConsoleWriteLine(
                string.Format("Removed the app with the ID {0} from the {1} subgroup in the {2} group.", appId, subgroup,
                    group), PrintType.Success);

            return true;
        }

        /// <summary>
        /// Adds a currency to the currency list
        /// </summary>
        /// <param name="abbreviation">Abbreviation of the currency</param>
        /// <param name="countryCode">Steam country code of the currency</param>
        /// <param name="itadCc">ITAD country code</param>
        /// <param name="priceFormat">Format used in numerical prices</param>
        /// <param name="priceDecimal">Symbol used as the decimal in the price</param>
        /// <returns>True if successful</returns>
        public static bool AddCurrency(string abbreviation, string countryCode, string itadCc, string priceFormat,
            string priceDecimal)
        {
            var currency = new Currency
            {
                Abbreviation = abbreviation,
                CountryCode = countryCode,
                ItadCc = itadCc,
                PriceFormat = priceFormat,
                PriceDecimal = priceDecimal
            };

            _currencies.Add(currency);

            ConsoleWriteLine(string.Format("Added the {0} currency.", abbreviation), PrintType.Success);

            return true;
        }

        /// <summary>
        /// Removes a currency from the currency list
        /// </summary>
        /// <param name="abbreviation">Abbreviation of the currency</param>
        /// <param name="countryCode">Steam country code of the currency</param>
        /// <param name="itadCc">ITAD country code</param>
        /// <param name="priceFormat">Format used in numerical prices</param>
        /// <param name="priceDecimal">Symbol used as the decimal in the price</param>
        /// <returns>True if successful</returns>
        public static bool RemoveCurrency(string abbreviation, string countryCode, string itadCc, string priceFormat,
            string priceDecimal)
        {
            foreach (var currency in _currencies.Where(currency => currency.Abbreviation == abbreviation)) 
            {
                _currencies.Remove(currency);

                ConsoleWriteLine(string.Format("Removed the {0} currency.", abbreviation), PrintType.Success);

                return true;
            }

            ConsoleWriteLine(string.Format("Could not remove the {0} currency.", abbreviation), PrintType.Error);

            return false;
        }

        /// <summary>
        /// Gets all currencies.
        /// </summary>
        /// <returns>A list containing all currencies</returns>
        public static List<Currency> GetCurrencies()
        {
            return _currencies;
        }

        /// <summary>
        /// Adds a group to store subgroups.
        /// </summary>
        /// <param name="group">Name of the group to add</param>
        /// <returns>True if successful</returns>
        public static bool AddGroup(string group)
        {
            group = group.ToUpper();

            if (_groups.ContainsKey(group))
            {
                ConsoleWriteLine(string.Format("Could not add the {0} group.", group), PrintType.Error);

                return false;
            }

            _groups.Add(group, new SortedList<string, SortedList<string, App>>());

            FileManager.AddGroup(group);

            ConsoleWriteLine(string.Format("Added the {0} group.", group), PrintType.Success);

            return true;
        }

        /// <summary>
        /// Adds a subgroup to store a list of apps.
        /// </summary>
        /// <param name="group">Name of the group to add the subgroup to</param>
        /// <param name="subgroup">Name of the subgroup to add</param>
        /// <returns>True if successful</returns>
        public static bool AddSubgroup(string group, string subgroup)
        {
            group = group.ToUpper();
            subgroup = subgroup.ToUpper();

            if (!_groups.ContainsKey(group) || _groups[group].ContainsKey(subgroup))
            {
                ConsoleWriteLine(string.Format("Could not add the {0} subgroup.", subgroup), PrintType.Error);

                return false;
            }

            _groups[group].Add(subgroup, new SortedList<string, App>());

            FileManager.AddSubgroup(group, subgroup);

            ConsoleWriteLine(string.Format("Added the {0} subgroup to the {1} group.", subgroup, group), PrintType.Success);

            return true;
        }

        /// <summary>
        /// Sets Reddit API information.
        /// </summary>
        /// <param name="user">Reddit username</param>
        /// <param name="pass">User's password</param>
        /// <param name="clientId">Client ID</param>
        /// <param name="clientSecret">Client Secret</param>
        /// <param name="redirectUri">Redirect UI</param>
        /// <returns>True if successful</returns>
        public static bool SetReddit(string user, string pass, string clientId, string clientSecret,
            string redirectUri)
        {
            try
            {
                var webAgent = new BotWebAgent(user, pass, clientId, clientSecret, redirectUri);

                _reddit = new Reddit(webAgent, true) {RateLimit = WebAgent.RateLimitMode.Burst};

                FileManager.WriteReddit(user, pass, clientId, clientSecret, redirectUri);

                ConsoleWriteLine(string.Format("Connected to reddit account {0}.", user), PrintType.Success);

                return true;
            }
            catch (WebException)
            {
                ConsoleWriteLine(string.Format("Could not connect to reddit account {0}.", user), PrintType.Error);

                return false;
            }
        }

        /// <summary>
        /// Sets the ITAD Key.
        /// </summary>
        /// <param name="itadKey">ITAD Key</param>
        /// <returns>True if successful</returns>
        public static bool SetItadKey(string itadKey)
        {
            _itadKey = itadKey;

            FileManager.WriteItad(itadKey);

            ConsoleWriteLine("ITAD Key set.", PrintType.Info);

            return true;
        }

        /// <summary>
        /// Sets the subreddit to use in updating Reddit.
        /// </summary>
        /// <param name="subreddit">Subreddit to update</param>
        /// <returns>True if successful</returns>
        public static bool SetSubreddit(string subreddit)
        {
            _subreddit = subreddit;

            FileManager.WriteSubreddit(subreddit);

            ConsoleWriteLine("Subbreddit set.", PrintType.Info);

            return true;
        }

        /// <summary>
        /// Searches for apps from the Steam store using the passed string.
        /// </summary>
        /// <param name="searchStr">String to search with</param>
        /// <returns>List of arrays of strings, where each array has an app ID and an app title</returns>
        public static List<string[]> GetSearchApps(string searchStr)
        {
            return SteamWebUpdate.SearchApps(searchStr);
        }

        /// <summary>
        /// Gets group names.
        /// </summary>
        /// <returns>An array of group names</returns>
        public static string[] GetGroupNames()
        {
            return _groups.Keys.ToArray();
        }

        /// <summary>
        /// Gets subgroup names.
        /// </summary>
        /// <param name="group">Name of group containing subgroups to read names of</param>
        /// <returns>An array of subgroup names</returns>
        public static string[] GetSubgroupNames(string group)
        {
            var subgroups = _groups[group].Keys.ToArray();

            return subgroups;
        }

        /// <summary>
        /// Gets all apps in the passed subgroup in the passed group.
        /// </summary>
        /// <param name="group">Group containing the subgroup</param>
        /// <param name="subgroup">Subgroup to read</param>
        /// <returns>List of apps in the subgroup</returns>
        public static IList<App> GetApps(string group, string subgroup)
        {
            var apps = _groups[group][subgroup].Values;

            return apps;
        }

        /// <summary>
        /// Updates app review data using the steam webpage.
        /// </summary>
        /// <returns>True if successful</returns>
        public static bool UpdateReviewData()
        {
            if (_thUpdateReviewData != null && _thUpdateReviewData.IsAlive)
            {
                ConsoleWriteLine("Review data currently being updated.", PrintType.Error);
                return false;
            }
            
            _thUpdateReviewData = new Thread(UpdateReviewDataThread);
            _thUpdateReviewData.Start();

            ConsoleWriteLine("Updating review details.", PrintType.Info);

            return true;
        }

        /// <summary>
        /// Updates app prices using the Steam API.
        /// </summary>
        /// <returns>True if successful</returns>
        public static bool UpdatePrices()
        {
            if (_thUpdatePrices != null && _thUpdatePrices.IsAlive)
            {
                ConsoleWriteLine("Prices currently being updated.", PrintType.Error);
                return false;
            }

            _thUpdatePrices = new Thread(UpdatePricesThread);
            _thUpdatePrices.Start();

            ConsoleWriteLine("Updating price details.", PrintType.Info);

            return true;
        }

        /// <summary>
        /// Updates app data using the Steam API.
        /// </summary>
        /// <returns>True if successful</returns>
        public static bool UpdateAppData()
        {
            if (_thUpdateAppData != null && _thUpdateAppData.IsAlive)
            {
                ConsoleWriteLine("App data currently being updated.", PrintType.Error);
                return false;
            }

            _thUpdateAppData = new Thread(UpdateAppDataThread);
            _thUpdateAppData.Start();

            ConsoleWriteLine("Updating app details.", PrintType.Info);

            return true;
        }

        /// <summary>
        /// Updates Reddit with the current app data using templates.
        /// </summary>
        /// <returns>True if successful</returns>
        public static bool UpdateReddit()
        {
            if (_thUpdateReddit != null && _thUpdateReddit.IsAlive)
            {
                ConsoleWriteLine("Reddit currently being updated.", PrintType.Error);
                return false;
            }

            _thUpdateReddit = new Thread(UpdateRedditThread);
            _thUpdateReddit.Start();

            ConsoleWriteLine("Updating Reddit.", PrintType.Info);

            return true;
        }

        /// <summary>
        /// Signifies the style in which to print out a line.
        /// </summary>
        public enum PrintType
        {
            /// <summary>
            /// Message used for information that occurs in the background.
            /// </summary>
            Notice, 
            /// <summary>
            /// Message used for information that occured as a result of user interaction,
            /// that isn't a pass or failure.
            /// </summary>
            Info, 
            /// <summary>
            /// Message used for information that occurs as a result of a failure.
            /// </summary>
            Error, 
            /// <summary>
            /// Message used for information that occurs as a result of a success.
            /// </summary>
            Success
        }

        /// <summary>
        /// Prints a string to the actual console and a colored string to the RichTextBox console.
        /// </summary>
        /// <param name="str">String to print</param>
        /// <param name="type">Type of string to print, specifying prefix and color of text to output 
        /// to the RichTextBox.</param>
        public static void ConsoleWriteLine(string str, PrintType type)
        {
            Console.Out.WriteLine(str);

            _console.SelectionStart = _console.TextLength;
            _console.SelectionLength = 0;
            switch (type)
            {
                case PrintType.Notice:
                    _console.SelectionColor = Color.DimGray;
                    _console.AppendText("[NOTICE] ");
                    break;
                case PrintType.Info:
                    _console.SelectionColor = Color.DarkBlue;
                    _console.AppendText("[INFO] ");
                    break;
                case PrintType.Error:
                    _console.SelectionColor = Color.DarkRed;
                    _console.AppendText("[FAILURE] ");
                    break;
                case PrintType.Success:
                    _console.SelectionColor = Color.DarkGreen;
                    _console.AppendText("[SUCCESS] ");
                    break;
            }

            _console.AppendText(str + '\n');
        }

        /// <summary>
        /// Update review data task for threads.
        /// </summary>
        private static void UpdateReviewDataThread()
        {
            SteamWebUpdate.UpdateReviewData(_apps.Values);
            FileManager.WriteRatings(_apps.Values.ToList());

            _thUpdateReviewDataFinished = true;
        }


        /// <summary>
        /// Update price data task for threads.
        /// </summary>
        private static void UpdatePricesThread()
        {
            SteamApiUpdate.UpdatePriceData(_apps.Values.ToList(), _currencies);
            FileManager.WritePrices(_apps.Values.ToList(), _currencies);

            _thUpdatePricesFinished = true;
        }

        /// <summary>
        /// Update app data task for threads.
        /// </summary>
        private static void UpdateAppDataThread()
        {
            SteamApiUpdate.UpdateAppData(_apps.Values.ToList(), GetDefaultCurrency());
            FileManager.WriteAppData(_apps.Values.ToArray());

            _thUpdateAppDataFinished = true;
        }

        /// <summary>
        /// Update Reddit task for threads.
        /// </summary>
        private static void UpdateRedditThread()
        {
            TableProcessor.UpdateReddit(_groups, _currencies, _reddit, _subreddit);
            FileManager.WriteAppData(_apps.Values.ToArray());

            _thUpdateRedditFinished = true;
        }

        /// <summary>
        /// Checks if various threads have finished, and prints a message accordingly.
        /// </summary>
        public static void ShortUpdate()
        {
            if (_thUpdateReviewDataFinished)
            {
                ConsoleWriteLine("Review details updated.", PrintType.Success);

                _thUpdateReviewDataFinished = false;
            }

            if (_thUpdatePricesFinished)
            {
                ConsoleWriteLine("Price details updated.", PrintType.Success);

                _thUpdatePricesFinished = false;
            }

            if (_thUpdateAppDataFinished)
            {
                ConsoleWriteLine("App details updated.", PrintType.Success);

                _thUpdateAppDataFinished = false;
            }

            if (_thUpdateRedditFinished)
            {
                ConsoleWriteLine("Reddit updated.", PrintType.Success);

                _thUpdateRedditFinished = false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace SteamSaleTables.Web
{
    class SteamWebUpdate
    {
        /// <summary>
        /// Url to access apps
        /// </summary>
        private const string AppUrl = "http://store.steampowered.com/app/";

        /// <summary>
        /// Url to access apps
        /// </summary>
        private const string SearchUrl = "http://store.steampowered.com/search/?term=";

        /// <summary>
        /// Regex string used to grab the app's rating and rating count.
        /// </summary>
        private const string SearchRating = @"<div class=""user_reviews_summary_row"" data-store-tooltip=""(\d{1,3})% of the (\d+?,?\d*) user reviews for this";


        /// <summary>
        /// Updates the dataset of app data for the apps based on their Steam data
        /// </summary>
        /// <param name="apps">Dataset of app data</param>
        /// <returns>True if successful</returns>
        public static void UpdateReviewData(IEnumerable<App> apps)
        {
            foreach (var app in apps)
            {
                var cookieJar = new CookieContainer(3);

                var target = new Uri("http://store.steampowered.com/app/");

                // Set mature age cookies to bypass an age check
                cookieJar.Add(new Cookie("mature_content", "1", "/app/" + app.Id, target.Host));
                cookieJar.Add(new Cookie("lastagecheckage", "1-January-1980", "/app/" + app.Id, target.Host));
                cookieJar.Add(new Cookie("birthtime", "315550801", "/app/" + app.Id, target.Host));

                var client = new CookieAwareWebClient(cookieJar);
                var html = client.DownloadString(AppUrl + app.Id);

                var fullRating = Regex.Match(html, SearchRating).Groups;

                app.Rating = fullRating[fullRating.Count - 2].Value;
                app.RatingCount = fullRating[fullRating.Count - 1].Value;

                if (string.IsNullOrEmpty(app.Rating))
                {
                    app.Rating = "0";
                }
                if (string.IsNullOrEmpty(app.RatingCount))
                {
                    app.RatingCount = "0";
                }
            }
        }

        /// <summary>
        /// Searches for apps from the Steam store using the passed string.
        /// </summary>
        /// <param name="searchStr">String to search with</param>
        /// <returns>List of arrays of strings, where each array has an app ID and an app title</returns>
        public static List<string[]> SearchApps(string searchStr)
        {
            var titles = new List<string[]>(25);

            // Replaces all characters with their %hex representation
            var url = searchStr.Select(Convert.ToByte)
                .Aggregate(SearchUrl, (current, b) => current + ('%' + b.ToString("x")));

            var client = new WebClient();

            var htmlData = client.DownloadData(url);
            var html = Encoding.UTF8.GetString(htmlData);

            var matches = Regex.Matches(html,
                @"apps/(\d{6})/.*\n" +
                @".*<div class=""responsive_search_name_combined"">.*\n" + 
                @".*<div class=""col search_name ellipsis"">.*\n" + 
                @".*<span class=""title"">(.*)</span>");

            titles.AddRange(from Match match in matches select 
                                new[] {match.Groups[1].Value, match.Groups[2].Value});

            return titles;
        }
    }
}

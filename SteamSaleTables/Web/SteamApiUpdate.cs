using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SteamSaleTables.Web
{
    /// <summary>
    /// Uses the Steam API to update various aspects of the app.
    /// </summary>
    class SteamApiUpdate
    {
        /// <summary>
        /// Url to access app information.
        /// </summary>
        private const string UrlFormat = "http://store.steampowered.com/api/appdetails/?appids={0}&filters={1}&cc={2}&l=english";

        private const string DataFilters = "basic,developers,publishers";

        private const string PriceFilters = "price_overview";

        /// <summary>
        /// Updates the dataset of app data for the apps based on their Steam data
        /// </summary>
        /// <param name="apps">Dataset of app data</param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static List<App> UpdateAppData(List<App> apps, Currency currency)
        {
            var client = new WebClient();

            foreach (var app in apps)
            {
                try
                {
                    var appUrl = string.Format(UrlFormat, app.Id, DataFilters, currency.CountryCode);

                    dynamic appData = JsonConvert.DeserializeObject(client.DownloadString(appUrl));

                    appData = appData[app.Id];

                    if (appData == null || appData.success == false)
                    {
                        continue;
                    }

                    appData = appData.data;

                    app.Title = appData.name;
                    JArray devs = appData.developers;
                    app.Developers = devs.Select(jv => (string)jv).ToArray();
                    JArray pubs = appData.publishers;
                    app.Publishers = pubs.Select(jv => (string)jv).ToArray();
                }
                catch (WebException)
                {
                    continue;
                }

            }

            return null;
        }

        /// <summary>
        /// Updates the dataset of app data for the apps based on their Steam data
        /// </summary>
        /// <param name="apps">Dataset of app data</param>
        /// <param name="currencies"></param>
        public static void UpdatePriceData(List<App> apps, List<Currency> currencies)
        {
            var client = new WebClient();

            var appIds = string.Empty;

            foreach (var app in apps)
            {
                if (appIds != string.Empty)
                {
                    appIds += ',';
                }

                appIds += app.Id;
            }


            foreach (var currency in currencies)
            {
                var url = string.Format(UrlFormat, appIds, PriceFilters, currency.CountryCode);

                dynamic allAppData = JsonConvert.DeserializeObject(client.DownloadString(url));

                foreach (var app in apps)
                {
                    dynamic appData = allAppData[app.Id];

                    if (appData == null || appData.success == false)
                    {
                        continue;
                    }

                    var priceOverview = appData.data.price_overview;


                    var priceData = app.PriceInfo.ContainsKey(currency.Abbreviation.ToLower())
                        ? app.PriceInfo[currency.Abbreviation]
                        : new PriceData();

                    int currPrice = priceOverview.final;

                    priceData.Price = GetPrice(currPrice);

                    var newDiscount = priceOverview.discount_percent.ToString();

                    if (newDiscount != priceData.Discount &&
                        CompareNums(priceData.Discount, priceData.LowestDiscount) > 0)
                    {
                        priceData.LowestDiscount = priceData.Discount;
                        priceData.LowestPrice = priceData.Price;
                    }

                    priceData.Discount = newDiscount;

                    if (app.PriceInfo.ContainsKey(currency.Abbreviation))
                    {
                        app.PriceInfo[currency.Abbreviation] = priceData;
                    }
                    else
                    {
                        app.PriceInfo.Add(currency.Abbreviation, priceData);
                    }
                }
            }
        }

        /// <summary>
        /// Gets a string representation of the passed integer price.
        /// </summary>
        /// <param name="price">Price to process</param>
        /// <returns>String representation of the passed price</returns>
        public static string GetPrice(int price)
        {
            double pr = price;

            pr /= 100;

            return pr.ToString("0.00");
        }


        /// <summary>
        /// Compares two numerical strings with the same precision.
        /// </summary>
        /// <param name="num1">First number to compare</param>
        /// <param name="num2">Second number to compare</param>
        /// <returns>
        /// 0 if the numbers are equal.
        /// -1 if the first number is smaller than the firstsecond
        /// 1 if the first number is larger than the second
        /// </returns>
        static int CompareNums(string num1, string num2)
        {
            if (num1.Length < num2.Length)
            {
                return -1;
            }
            else if (num1.Length > num2.Length)
            {
                return 1;
            }
            else
            {
                return (string.Compare(num1, num2, StringComparison.Ordinal));
            }
        }
    }
}

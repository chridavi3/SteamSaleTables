using System;
using System.Collections.Generic;
using System.Linq;

namespace SteamSaleTables
{
    /// <summary>
    /// Data saved for a Steam App.
    /// </summary>
    class App : IComparable<App>
    {
        public string Id { get; private set; }

        public string Title { get; set; }

        public SortedList<string, PriceData> PriceInfo { get; set; }

        public string[] Developers { get; set; }

        public string[] Publishers { get; set; }

        public string Description { get; set; }

        public string Rating { get; set; }

        public string RatingCount { get; set; }


        /// <summary>
        /// Creates a new app with all fields initialized.
        /// </summary>
        /// <param name="id">The 6 digit steam app id found in an app's url</param>
        /// <param name="rating">Percent app rating out of 100</param>
        /// <param name="ratingCount">Number of people who have rated the app</param>
        public App(string id, string rating, string ratingCount)
        {
            Id = id;
            Title = string.Empty;
            Developers = null;
            Publishers = null;
            Description = string.Empty;
            Rating = rating;
            RatingCount = ratingCount;
            PriceInfo = new SortedList<string, PriceData>();
        }
 
        /// <summary>
        /// Creates a new app with all fields initialized.
        /// </summary>
        /// <param name="id">The 6 digit steam app id found in an app's url</param>
        /// <param name="title">App title</param>
        /// <param name="developers">Developers of the app</param>
        /// <param name="publishers">Publishers of the app</param>
        /// <param name="description">Brief description to include next to the app</param>
        public App(string id, string title = "", string[] developers = null,
            string[] publishers = null, string description = "")
        {
            Id = id;
            Title = title;
            Developers = developers;
            Publishers = publishers;
            Description = description;
            Rating = string.Empty;
            RatingCount = string.Empty;
            PriceInfo = new SortedList<string, PriceData>();
        }


        /// <summary>
        /// Compares two apps solely based on title.
        /// </summary>
        /// <param name="other">App to compare to</param>
        /// <returns>True if this app has the same title as the other app.</returns>
        public int CompareTo(App other)
        {
            return string.Compare(Title.ToUpper(), other.Title.ToUpper(), StringComparison.Ordinal);
        }

        /// <summary>
        /// Gets a string representation of the data in the app.
        /// </summary>
        /// <returns>A string containing the sandboxed data values in the app</returns>
        public string GetDataString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}", Id, Title, GetDevsOutString(), GetPubsOutString(), Description);
        }

        /// <summary>
        /// Gets a string representation of the price data in the app.
        /// </summary>
        /// <param name="currency">Currency to use in fetching the price data</param>
        /// <returns>
        /// A string with the following data seperated by tabs: 
        /// Id, Price, Lowest Price, Discount, Lowest Discount
        /// </returns>
        public string GetPriceString(Currency currency)
        {
            if (PriceInfo.ContainsKey(currency.Abbreviation))
            {
                return string.Format("{0}\t{1}\t{2}\t{3}\t{4}", Id, PriceInfo[currency.Abbreviation].Price,
                    PriceInfo[currency.Abbreviation].LowestPrice, PriceInfo[currency.Abbreviation].Discount,
                    PriceInfo[currency.Abbreviation].LowestDiscount);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets a string representation of the rating data in the app.
        /// </summary>
        /// <returns>
        /// A string with the following data seperated by tabs: 
        /// Id, Rating, Rating Count
        /// </returns>
        public string GetRatingString()
        {
            return string.Format("{0}\t{1}\t{2}", Id, Rating, RatingCount);
        }

        /// <summary>
        /// Gets a string representation of the developers in the app to be used in data storage.
        /// </summary>
        /// <returns>A string where each developer is preluded by 'd' and has a tab inbetween it and the next.</returns>
        public string GetDevsOutString()
        {
            return Developers == null
                ? string.Empty
                : 'd' + string.Join("\t", Developers);
        }

        /// <summary>
        /// Gets a string representation of the publishers in the app to be used in data storage.
        /// </summary>
        /// <returns>A string where each publisher is preluded by 'p' and has a tab inbetween it and the next.</returns>
        public string GetPubsOutString()
        {
            return Publishers == null
                ? string.Empty
                : 'p' + string.Join("\t", Publishers);
        }

        /// <summary>
        /// Gets a string representation of the developers in the app to be used in output.
        /// </summary>
        /// <returns>A string where each developer has ", " inbetween it and the next.</returns>
        public string GetDevsFormattedString()
        {
            return string.Join(", ", Developers);
        }

        /// <summary>
        /// Gets a string representation of the publishers in the app to be used in output.
        /// </summary>
        /// <returns>A string where each publisher has ", " inbetween it and the next.</returns>
        public string GetPubsFormattedString()
        {
            return string.Join(", ", Publishers);
        }


        /// <summary>
        /// Gets an array representation of the data in the app.
        /// </summary>
        /// <returns>An array representation of the data in the app</returns>
        public string[] ToArray(Currency currency)
        {
            if (PriceInfo.Count != 0)
            {

                if (PriceInfo.ContainsKey(currency.Abbreviation)) {
                    return new[]
                    {
                        Id, PriceInfo[currency.Abbreviation].LowestDiscount, PriceInfo[currency.Abbreviation].Discount, Title,
                        PriceInfo[currency.Abbreviation].Price, GetDevsFormattedString(), GetPubsFormattedString(), Rating,
                        RatingCount, Description
                    };
                }
                else {
                    return new[]
                    {
                        Id, PriceInfo.Values[0].LowestDiscount, PriceInfo.Values[0].Discount, Title, PriceInfo.Values[0].Price,
                        GetDevsFormattedString(), GetPubsFormattedString(), Rating, RatingCount, Description
                    };
                }
            }
            else
            {
                return new[]
                {
                    Id, "?", "?", Title, "?", GetDevsFormattedString(), GetPubsFormattedString(), Rating, RatingCount,
                    Description
                };
            }
        }

        /// <summary>
        /// Checks if a Steam app ID is valid.
        /// </summary>
        /// <param name="id">App ID to verify.</param>
        /// <returns>True if valid</returns>
        public static bool ValidAppId(string id)
        {
            return id.Length == 6 && id.All(char.IsDigit);
        }
    }
}

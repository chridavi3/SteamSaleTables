using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamSaleTables
{
    /// <summary>
    /// Represents a currency managed by the program.
    /// </summary>
    struct Currency
    {
        /// <summary>
        /// Currency abbreviation.
        /// </summary>
        public string Abbreviation;

        /// <summary>
        /// Country code for a country that uses this currency.
        /// </summary>
        public string CountryCode;

        /// <summary>
        /// ITAD CC used to get price history from SteamDB.
        /// </summary>
        public string ItadCc;

        /// <summary>
        /// Format used to display a price, where {0} is the currency symbol and {1} is the numberical price.
        /// </summary>
        public string PriceFormat;

        /// <summary>
        /// String used to represent a decimal in the file
        /// </summary>
        public string PriceDecimal;
    }
}

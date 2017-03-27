namespace SteamSaleTables
{
    /// <summary>
    /// Price data for a Steam app in a particular currency.
    /// </summary>
    struct PriceData
    {
        /// <summary>
        /// Current Steam price for this currency.
        /// </summary>
        public string Price;

        /// <summary>
        /// Lowest Steam price for this currency.
        /// </summary>
        public string LowestPrice;

        /// <summary>
        /// Current Steam discount for this currency.
        /// </summary>
        public string Discount;

        /// <summary>
        /// Lowest Steam discount for this currency.
        /// </summary>
        public string LowestDiscount;
    }
}

namespace Demo_ApiConsume.Models
{
    public class ExchangeViewListModel
    {
        //Edit-Paste Special-Paste JSON As Classes

        //Kök objenin içinde olanlar

        public Exchange_Rates[] exchange_rates { get; set; }
        public string base_currency { get; set; }
        public string base_currency_date { get; set; }

        //nasted yapısı(içiçe)
        public class Exchange_Rates
        {
            public string currency { get; set; }
            public string exchange_rate_buy { get; set; }
        }
    }
}
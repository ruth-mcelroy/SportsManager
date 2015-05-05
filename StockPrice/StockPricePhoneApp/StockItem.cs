// a StockListing i.e. ticker and price

namespace StockPrice.Models
{
    // a listing for a stock on the stock market
    public class StockListing
    {
        // ticker symbol e.g. AAPL, GOOG, IBM, MSFT
        public string TickerSymbol                      // cf Binding in priceList long list selector
        {
            get;
            set;
        }

        // price last trade in $
        public double Price                             // cf Binding in priceList long list selector
        {
            get;
            set;
        }

    }
}

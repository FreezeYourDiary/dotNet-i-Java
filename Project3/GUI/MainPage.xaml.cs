using API;

namespace GUI
{
    public partial class MainPage : ContentPage
    {
        private readonly APItest apiTest;
        private readonly PortfolioDB portfolio;

        public MainPage(APItest apiTest, PortfolioDB portfolio)
        {
            InitializeComponent();
            this.apiTest = apiTest;
            this.portfolio = portfolio;

            LoadStockList();
        }

        private async void OnAddStockClicked(object sender, EventArgs e)
        {
            string ticker = StockEntry.Text?.Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(ticker))
            {
                await DisplayAlert("Error", "Enter a stock ticker!", "OK");
                return;
            }

            string apiUrl = $"https://www.alphavantage.co/query?function=OVERVIEW&symbol={ticker}&apikey=9DOGPPXUNCAFM5U1";
            ApiCallLabel.Text = $"API Call: {apiUrl}";

            bool success = await apiTest.GetData(ticker);
            if (success)
            {
                ApiResponseLabel.Text = $"API: Stock {ticker} added/updated successfully!";
                LoadStockList();
            }
            else
            {
                ApiResponseLabel.Text = $"API: Error for data {ticker}.";
            }
        }

        private void OnRefreshStocksClicked(object sender, EventArgs e)
        {
            LoadStockList();
        }

        private void LoadStockList()
        {
            StockListView.ItemsSource = apiTest.GetStocksFromDatabase();
        }
        private void OnStockTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                var selectedStock = e.Item as Stock;

                if (selectedStock != null)
                {
                    StockDetailsLabel.Text = $"Stock: {selectedStock.Name}\n" +
                                             $"Symbol: {selectedStock.Symbol}\n" +
                                             $"Exchange: {selectedStock.Exchange}\n" +
                                             $"Currency: {selectedStock.Currency}\n" +
                                             $"Sector: {selectedStock.Sector}\n" +
                                             $"Market Cap: {selectedStock.MarketCapitalization}\n" +
                                             $"52W High: {selectedStock.Week52High}\n" +
                                             $"52W Low: {selectedStock.Week52Low}";
                }
            }
        }
    }
}
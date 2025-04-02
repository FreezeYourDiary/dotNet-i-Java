using API;

namespace GUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var portfolio = new PortfolioDB();
            var apiTest = new APItest(portfolio);
            MainPage = new MainPage(apiTest, portfolio);
        }
    }
}

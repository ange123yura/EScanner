using EScanner.Views;

namespace EScanner
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new FrontPage();
        }
    }
}

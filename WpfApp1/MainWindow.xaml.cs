using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Clicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public long EUValue;
        public long EUClick;
        public long PriceUpgradeEUClick;
        public long ClickUpgradeValue;

        public MainWindow()
        {
            this.EUValue = 0;
            this.EUClick = 1;
            this.PriceUpgradeEUClick = 10;
            this.ClickUpgradeValue = 1;

            InitializeComponent();

            CountTextBlock.Text = EUValue.ToString();
            UpgradeButton.Content = $"+{ClickUpgradeValue.ToString()}€ per click\n-{PriceUpgradeEUClick.ToString()}€";

        }

        private void UpgradeClickValue(object sender, RoutedEventArgs e)
        {
            EUValue += EUClick;
            CountTextBlock.Text = EUValue.ToString();
        }

        private void UpgradeEUPerClick(object sender, RoutedEventArgs e)
        {

            if (EUValue < PriceUpgradeEUClick) return;
            EUClick += ClickUpgradeValue;
            EUValue -= PriceUpgradeEUClick;
            ClickUpgradeValue += 1;
            PriceUpgradeEUClick += 50;
            UpgradeButton.Content = $"+{ClickUpgradeValue.ToString()}€ per click\n-{PriceUpgradeEUClick.ToString()}€";
            CountTextBlock.Text = EUValue.ToString();

        }
    }
}
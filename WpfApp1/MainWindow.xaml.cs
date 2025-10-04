using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
        public const long ClickUpgradeValue = 1;
        public DoubleAnimation Earning_animation = new DoubleAnimation
        {
            From = 0.0,
            To = 1.0,
            Duration = TimeSpan.FromMilliseconds(1000),
            AutoReverse = true
        };

        public MainWindow()
        {
            this.EUValue = 0;
            this.EUClick = 1;
            this.PriceUpgradeEUClick = 10;

            InitializeComponent();

            CountTextBlock.Text = $"{EUValue.ToString()}€";
            UpgradeButton.Content = $"+{ClickUpgradeValue.ToString()}€ per click\n-{PriceUpgradeEUClick.ToString()}€";

        }

        private void UpgradeClickValue(object sender, RoutedEventArgs e)
        {
            EUValue += EUClick;
            CountTextBlock.Text = $"{EUValue.ToString()}€";

            // Ensure layout is completed before reading ActualWidth/ActualHeight
            Dispatcher.BeginInvoke(new Action(() =>
            {

                Earning_textblock.Text = $"+{EUClick.ToString()}€";

                short Rand_case1 = (short)Random.Shared.Next(1, 3);
                short Rand_case2 = (short)Random.Shared.Next(1, 3);

                double maxX;
                double maxY;
                double minX;
                double minY;

                if (Rand_case1 == 1)
                {
                    maxX = Math.Max(0, Earning_canvas.ActualWidth/2 - Earning_textblock.ActualWidth - ClickButton.ActualWidth/2);
                    minX = 0;
                }
                else
                {
                    maxX = Math.Max(0, Earning_canvas.ActualWidth - Earning_textblock.ActualWidth - BoostsPanel.ActualWidth);
                    minX = Math.Max(0, Earning_canvas.ActualWidth/2 + ClickButton.ActualWidth/2);
                }

                if (Rand_case2 == 1)
                {
                    maxY = Math.Max(0, Earning_canvas.ActualHeight/2 - Earning_textblock.ActualHeight - ClickButton.ActualHeight/2);
                    minY = 0;
                }
                else
                {
                    maxY = Math.Max(0, Earning_canvas.ActualHeight - Earning_textblock.ActualHeight - CountTextBlock.ActualHeight - CountTextBlock.Margin.Top);
                    minY = Math.Max(0, Earning_canvas.ActualHeight/2 + ClickButton.ActualHeight/2);
                }

                
                int x = Random.Shared.Next(Math.Max(0, (int)minX), Math.Max(1, (int)maxX));
                int y = Random.Shared.Next(Math.Max(0, (int)minY), Math.Max(1, (int)maxY));

                Canvas.SetLeft(Earning_textblock, x);
                Canvas.SetTop(Earning_textblock, y);

                Earning_textblock.BeginAnimation(OpacityProperty, Earning_animation);
            }), System.Windows.Threading.DispatcherPriority.Loaded);
        }

        private void UpgradeEUPerClick(object sender, RoutedEventArgs e)
        {

            if (EUValue < PriceUpgradeEUClick) return;
            EUClick += ClickUpgradeValue;
            EUValue -= PriceUpgradeEUClick;
            PriceUpgradeEUClick = (long)Math.Ceiling(PriceUpgradeEUClick * 1.6);
            UpgradeButton.Content = $"+{ClickUpgradeValue.ToString()}€ per click\n-{PriceUpgradeEUClick.ToString()}€";
            CountTextBlock.Text = $"{EUValue.ToString()}€";
        }
    }
}
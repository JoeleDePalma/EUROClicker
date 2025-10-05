using System;
using System.Reflection;
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
using System.Windows.Threading;

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
        public long PriceUpgradeEUAutoClick;
        public long EUAutoClick;
        public const long CLICKUPGRADEVALUE = 1;
        public DoubleAnimation Earning_opacity_animation = new DoubleAnimation
        {
            From = 0.0,
            To = 1.0,
            Duration = TimeSpan.FromMilliseconds(500),
            AutoReverse = true
        };
        public void Earning_TextBlock_Animation
        (
            object? sender,
            EventArgs e,
            TextBlock Earning_block,
            Canvas Earning_canvas,
            long Earning_click
        )
        {
            Earning_block.Text = $"+{Earning_click.ToString()}€";

            short Rand_case1 = (short)Random.Shared.Next(1, 3);
            short Rand_case2 = (short)Random.Shared.Next(1, 3);

            double maxX;
            double maxY;
            double minX;
            double minY;

            if (Rand_case1 == 1)
            {
                maxX = Math.Max(0, Earning_canvas.ActualWidth / 2 - Earning_block.ActualWidth - ClickButton.ActualWidth / 2);
                minX = 0;
            }
            else
            {
                maxX = Math.Max(0, Earning_canvas.ActualWidth - Earning_block.ActualWidth - BoostsPanel.ActualWidth);
                minX = Math.Max(0, Earning_canvas.ActualWidth / 2 + ClickButton.ActualWidth / 2);
            }

            if (Rand_case2 == 1)
            {
                maxY = Math.Max(0, Earning_canvas.ActualHeight / 2 - Earning_block.ActualHeight - ClickButton.ActualHeight / 2);
                minY = 0;
            }
            else
            {
                maxY = Math.Max(0, Earning_canvas.ActualHeight - Earning_block.ActualHeight - CountTextBlock.ActualHeight - CountTextBlock.Margin.Top);
                minY = Math.Max(0, Earning_canvas.ActualHeight / 2 + ClickButton.ActualHeight / 2);
            }


            int x = Random.Shared.Next(Math.Max(0, (int)minX), Math.Max(1, (int)maxX));
            int y = Random.Shared.Next(Math.Max(0, (int)minY), Math.Max(1, (int)maxY));

            Canvas.SetLeft(Earning_block, x);
            Canvas.SetTop(Earning_block, y);

            Earning_block.BeginAnimation(OpacityProperty, Earning_opacity_animation);
        }

        public MainWindow()
        {
            this.EUValue = 0;
            this.EUClick = 1;
            this.PriceUpgradeEUClick = 10;
            this.PriceUpgradeEUAutoClick = 300;
            this.EUAutoClick = 0;

            InitializeComponent();

            CountTextBlock.Text = $"{EUValue.ToString()}€";
            UpgradeButton.Content = $"+{CLICKUPGRADEVALUE.ToString()}€ per click\n-{PriceUpgradeEUClick.ToString()}€";
            AutoclickUpgradeButton.Content = ($"+{CLICKUPGRADEVALUE.ToString()}€ per autoclick\n-{PriceUpgradeEUAutoClick.ToString()}€");

            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (sender, e) =>
            {
                if (EUAutoClick == 0) return;

                EUValue += EUAutoClick;
                CountTextBlock.Text = $"{EUValue.ToString()}€";

                Dispatcher.BeginInvoke(new Action(() =>
                {
                    Earning_TextBlock_Animation(sender, e, Earning_AutoClick_textblock, Earning_AutoClick_canvas, EUAutoClick);
                }), DispatcherPriority.Loaded);
            };
            timer.Start();
        }

        private void UpgradeClickValue(object sender, RoutedEventArgs e)
        {
            EUValue += EUClick;
            CountTextBlock.Text = $"{EUValue.ToString()}€";

            Dispatcher.BeginInvoke(new Action(() =>
            {
                Earning_TextBlock_Animation(sender, e, Earning_click_textblock, Earning_click_canvas, EUClick);
            }),
            DispatcherPriority.Loaded);
        }

        private void UpgradeEUPerClick(object sender, RoutedEventArgs e)
        {

            if (EUValue < PriceUpgradeEUClick) return;

            EUClick += CLICKUPGRADEVALUE;
            EUValue -= PriceUpgradeEUClick;

            PriceUpgradeEUClick = (long)Math.Ceiling(PriceUpgradeEUClick * 1.6);
            UpgradeButton.Content = $"+{CLICKUPGRADEVALUE.ToString()}€ per click\n-{PriceUpgradeEUClick.ToString()}€";
            CountTextBlock.Text = $"{EUValue.ToString()}€";

            if (!AutoclickUpgradeButton.IsVisible && EUClick > 10)
            {
                AutoclickUpgradeButton.Visibility = Visibility.Visible;
            }
        }

        private void UpgradeEUAutoClick(object sender, RoutedEventArgs e)
        {
            if (EUValue < PriceUpgradeEUAutoClick) return;

            EUValue -= PriceUpgradeEUAutoClick;
            EUAutoClick += CLICKUPGRADEVALUE;

            PriceUpgradeEUAutoClick = (long)Math.Ceiling(PriceUpgradeEUAutoClick * 1.6);
            AutoclickUpgradeButton.Content = $"+{CLICKUPGRADEVALUE.ToString()}€ per autoclick\n-{PriceUpgradeEUAutoClick.ToString()}€";
            CountTextBlock.Text = $"{EUValue.ToString()}€";
        }
    }
}
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
using Upgrades;

namespace Clicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static long EUValue; // Current Euro Liquidity
        public static long EUClick; // Euro per Click
        public static long PriceUpgradeEUClick; // Price of Upgrade Euro per Click
        public static long PriceUpgradeEUAutoClick; // Price of Upgrade Euro per Auto Click
        public static long EUAutoClick; // Euro per Auto Click
        public static long ClickUpgradeValue; // Euros to add on each upgrade
        public static long PriceUpgradeX3Earning; // Price of the x3 Earning Upgrade
        public static DoubleAnimation Earning_opacity_animation = new DoubleAnimation
        {
            From = 0.0,
            To = 1.0,
            Duration = TimeSpan.FromMilliseconds(500),
            AutoReverse = true
        }; // Animation for Earning TextBlock

        // The variables are static to be accessible from Upgrades_logic class
        public void Earning_TextBlock_Animation
        (
            object? sender,
            EventArgs e,
            TextBlock Earning_block,
            Canvas Earning_canvas,
            long Earning_click
        )
        {
            // Updating UI
            Earning_block.Text = $"+{Earning_click.ToString()}€";

            int x;
            int y;

            while (true)
            {
                try
                {
                    // Random position generation
                    short Rand_case1 = (short)Random.Shared.Next(1, 3);
                    short Rand_case2 = (short)Random.Shared.Next(1, 3);

                    // Defining max and min values for X and Y coordinates
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

                    // Defining random position with the max and min values
                    x = Random.Shared.Next(Math.Max(0, (int)minX), Math.Max(1, (int)maxX));
                    y = Random.Shared.Next(Math.Max(0, (int)minY), Math.Max(1, (int)maxY));

                    break;
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"Error generating random position: {ex.Message}");
                    return;
                }
            }

            // Setting the text position
            Canvas.SetLeft(Earning_block, x);
            Canvas.SetTop(Earning_block, y);

            // Starting the text animation
            Earning_block.BeginAnimation(OpacityProperty, Earning_opacity_animation);
        }

        public MainWindow()
        {
            // Setting initial values

            EUValue = 0; 
            EUClick = 1;
            PriceUpgradeEUClick = 10;
            PriceUpgradeEUAutoClick = 300;
            EUAutoClick = 0;
            ClickUpgradeValue = 1;
            PriceUpgradeX3Earning = 1000;

            // Initializing components
            InitializeComponent();

            // Setting initial UI values
            CountTextBlock.Text = $"{EUValue.ToString()}€";
            ClickUpgradeButton.Content = $"-{PriceUpgradeEUClick.ToString()}€";
            AutoclickClickUpgradeButton.Content = ($"-{PriceUpgradeEUAutoClick.ToString()}€");
            X3EarningUpgradeButton.Content = ($"-{PriceUpgradeX3Earning.ToString()}€");

            // Initializing timer for Auto Click
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
            Upgrades_logic.UpgradeClickValue_func
            (
                mainWindow: this,
                sender: sender,
                e: e
            );
        }

        private void UpgradeEUPerClick(object sender, RoutedEventArgs e)
        {
            Upgrades_logic.UpgradeEUPerClick_func
            (
                mainWindow: this
            );
        }

        private void UpgradeEUAutoClick(object sender, RoutedEventArgs e)
        {
            Upgrades_logic.UpgradeEUAutoClick_func
            (
                mainWindow: this
            );
        }

        private void UpgradeX3Earning(object sender, RoutedEventArgs e)
        {
            Upgrades_logic.UpgradeX3Earning_func
            (
                mainWindow: this
            );
        }
    }
}
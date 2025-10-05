using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Clicker;

namespace Upgrades
{
    /// <summary>
    /// Class that contains the logic for the upgrades
    /// </summary>
    internal static class Upgrades_logic
    {

        /// <summary>
        /// Function that handles the click event on the principal button to increase the EUValue by EUClick amount
        /// </summary>
        /// <param name="mainWindow"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static public void UpgradeClickValue_func(MainWindow mainWindow, object? sender, RoutedEventArgs e)
        {
            MainWindow.EUValue += MainWindow.EUClick;
            mainWindow.CountTextBlock.Text = $"{MainWindow.EUValue.ToString()}€";

            mainWindow.Dispatcher.BeginInvoke(new Action(() =>
            {
                mainWindow.Earning_TextBlock_Animation(sender, e, mainWindow.Earning_click_textblock, mainWindow.Earning_click_canvas, MainWindow.EUClick);
            }),
            DispatcherPriority.Loaded);
        }

        /// <summary>
        /// function that handles the click event on the upgrade click button to increase the EUClick by ClickUpgradeValue amount
        /// </summary>
        /// <param name="mainWindow"></param>
        static public void UpgradeEUPerClick_func(MainWindow mainWindow)
        {
            if (MainWindow.EUValue < MainWindow.PriceUpgradeEUClick) return;

            MainWindow.EUValue -= MainWindow.PriceUpgradeEUClick;
            MainWindow.EUClick += MainWindow.ClickUpgradeValue;

            MainWindow.PriceUpgradeEUClick = (long)Math.Ceiling(MainWindow.PriceUpgradeEUClick * 1.6);
            mainWindow.ClickUpgradeButton.Content = $"-{MainWindow.PriceUpgradeEUClick}€";
            mainWindow.CountTextBlock.Text = $"{MainWindow.EUValue}€";

            if (!mainWindow.AutoclickClickUpgradeBorder.IsVisible && MainWindow.EUClick > 10)
            {
                mainWindow.AutoclickClickUpgradeBorder.Visibility = Visibility.Visible;
            }

        }

        /// <summary>
        /// function that handles the click event on the upgrade auto click button to increase the EUAutoClick by ClickUpgradeValue amount
        /// </summary>
        /// <param name="mainWindow"></param>
        static public void UpgradeEUAutoClick_func(MainWindow mainWindow)
        {
            if (MainWindow.EUValue < MainWindow.PriceUpgradeEUAutoClick) return;

            MainWindow.EUValue -= MainWindow.PriceUpgradeEUAutoClick;
            MainWindow.EUAutoClick += MainWindow.ClickUpgradeValue;

            MainWindow.PriceUpgradeEUAutoClick = (long)Math.Ceiling(MainWindow.PriceUpgradeEUAutoClick * 1.6);
            mainWindow.AutoclickClickUpgradeButton.Content = $"-{MainWindow.PriceUpgradeEUAutoClick.ToString()}€";
            mainWindow.CountTextBlock.Text = $"{MainWindow.EUValue.ToString()}€";
        }
    }
}

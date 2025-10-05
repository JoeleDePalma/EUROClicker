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
    internal static class Upgrades_logic
    {
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
    
        static public void UpgradeEUPerClick_func(MainWindow mainWindow)
        {
            if (MainWindow.EUValue < MainWindow.PriceUpgradeEUClick) return;

            MainWindow.EUValue -= MainWindow.PriceUpgradeEUClick;
            MainWindow.EUClick += MainWindow.ClickUpgradeValue;

            MainWindow.PriceUpgradeEUClick = (long)Math.Ceiling(MainWindow.PriceUpgradeEUClick * 1.6);
            mainWindow.ClickUpgradeButton.Content = $"+{MainWindow.ClickUpgradeValue}€ per click\n-{MainWindow.PriceUpgradeEUClick}€";
            mainWindow.CountTextBlock.Text = $"{MainWindow.EUValue}€";

            if (!mainWindow.AutoclickClickUpgradeButton.IsVisible && MainWindow.EUClick > 10)
            {
                mainWindow.AutoclickClickUpgradeButton.Visibility = Visibility.Visible;
            }

        }

        static public void UpgradeEUAutoClick_func(MainWindow mainWindow)
        {
            if (MainWindow.EUValue < MainWindow.PriceUpgradeEUAutoClick) return;

            MainWindow.EUValue -= MainWindow.PriceUpgradeEUAutoClick;
            MainWindow.EUAutoClick += MainWindow.ClickUpgradeValue;

            MainWindow.PriceUpgradeEUAutoClick = (long)Math.Ceiling(MainWindow.PriceUpgradeEUAutoClick * 1.6);
            mainWindow.AutoclickClickUpgradeButton.Content = $"+{MainWindow.ClickUpgradeValue.ToString()}€ per autoclick\n-{MainWindow.PriceUpgradeEUAutoClick.ToString()}€";
            mainWindow.CountTextBlock.Text = $"{MainWindow.EUValue.ToString()}€";
        }
    }
}

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Winop.Pages;

namespace Winop
{
    /// <summary>
    /// Main window with left navigation menu and content frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ExtendsContentIntoTitleBar = true;
            SetTitleBar(TitleBarControl);
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigated += On_Navigated;
            ContentFrame.NavigationFailed += ContentFrame_NavigationFailed;

            NavView.SelectedItem = NavView.MenuItems[0];
            NavView_Navigate(typeof(HomePage), new EntranceNavigationTransitionInfo());
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer?.Tag is string tag)
            {
                var navPageType = Type.GetType(tag);
                if (navPageType is not null)
                {
                    NavView_Navigate(navPageType, args.RecommendedNavigationTransitionInfo);
                }
            }
        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            TryGoBack();
        }

        private void NavView_Navigate(Type navPageType, NavigationTransitionInfo transitionInfo)
        {
            var preNavPageType = ContentFrame.CurrentSourcePageType;

            if (navPageType is not null && !Equals(preNavPageType, navPageType))
            {
                ContentFrame.Navigate(navPageType, null, transitionInfo);
            }
        }

        private bool TryGoBack()
        {
            if (!ContentFrame.CanGoBack)
                return false;

            if (NavView.IsPaneOpen &&
                (NavView.DisplayMode == NavigationViewDisplayMode.Compact ||
                 NavView.DisplayMode == NavigationViewDisplayMode.Minimal))
                return false;

            ContentFrame.GoBack();
            return true;
        }

        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            NavView.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType is not null)
            {
                var selectedItem = NavView.MenuItems
                    .OfType<NavigationViewItem>()
                    .FirstOrDefault(i => i.Tag?.ToString() == ContentFrame.SourcePageType.FullName);

                if (selectedItem is not null)
                {
                    NavView.SelectedItem = selectedItem;
                    NavView.Header = selectedItem.Content;
                }
            }
        }

        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new InvalidOperationException("Failed to load Page " + e.SourcePageType?.FullName);
        }
    }
}

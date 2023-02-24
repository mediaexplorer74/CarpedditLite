﻿using Carpeddit.Api.Helpers;
using Carpeddit.Api.Services;
using Carpeddit.Api.Watchers;
using Carpeddit.App.ViewModels;
using Carpeddit.App.Views;
using Carpeddit.Common.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Carpeddit.App
{
    public sealed partial class LoadingPage : Page
    {
        public LoadingPage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            TitleBar.Loaded += (_, _1) => TitleBar.SetAsTitleBar();
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            var settings = App.Services.GetService<SettingsViewModel>();
            Frame.RequestedTheme = settings.Theme;

            // TODO: Handle setup.

            var message = await IsValidSessionAsync();

            if (message == "Successful")
            {
                Frame.Navigate(typeof(MainPage), null, new SuppressNavigationTransitionInfo());
                return;
            } else if (message == "NotLoggedIn")
            {
                // Token must not be reused, sign out the user.
                await AccountHelper.Instance.SignOutAsync(false);
                Frame.Navigate(typeof(LoginPage), null, new SuppressNavigationTransitionInfo());
                return;
            }

            Frame.Navigate(typeof(OfflinePage), null, new SuppressNavigationTransitionInfo());
        }

        private async Task<string> IsValidSessionAsync()
        {
            try
            {
                _ = await App.Services.GetService<IRedditService>().GetMeAsync();
                return "Successful";
            }
            catch (Exception e)
            {
                return (e is UnauthorizedAccessException || AccountHelper.Instance.GetCurrentInfo() == null) ? "NotLoggedIn" : "UnknownError";
            }
        }
    }
}

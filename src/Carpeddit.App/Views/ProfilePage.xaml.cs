﻿using Carpeddit.Common.Helpers;
using Carpeddit.Models;
using Carpeddit.Models.Api;
using System.Net;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Carpeddit.Common.Collections;
using System.Linq;
using Carpeddit.App.ViewModels;
using CommunityToolkit.Mvvm.Input;
using Carpeddit.App.Models;

namespace Carpeddit.App.Views
{
    public sealed partial class ProfilePage : Page
    {
        private User _user;
        private BulkObservableCollection<PostViewModel> _posts = new();
        private bool isLoadingMore;
        private bool _eventRegistered;

        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is User user)
            {
                _user = user;
                Loaded += Page_Loaded;
                _eventRegistered = true;
            }
            else if (e.Parameter is string userName)
            {
                _user = await App.Client.GetUserAsync(userName);
                Page_Loaded(null, null);
                Bindings.Update();
            }
            else
            {
                _user = await App.Client.Account.GetMeAsync();
                Page_Loaded(null, null);
                Bindings.Update();
            }

            base.OnNavigatedTo(e);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_eventRegistered)
                Loaded -= Page_Loaded;

            LoadingInfoRing.IsActive = false;
            LoadingInfoRing.Visibility = Visibility.Collapsed;

            if (_user.Subreddit.UserIsSubscriber)
                JoinButton.Content = "Unfollow";

            if (string.IsNullOrWhiteSpace(_user.Subreddit.Title))
                _ = VisualStateManager.GoToState(this, "NoDisplayName", false);

            try
            {
                SubredditHeaderImg.Source = new BitmapImage(new(WebUtility.HtmlDecode(_user.Subreddit.BannerImage)));
            }
            catch (UriFormatException)
            {

            }

            PostLoadingProgressRing.IsActive = true;
            PostLoadingProgressRing.Visibility = Visibility.Visible;

            var posts = (await App.Client.GetUserPostsAsync(_user.Name, new(limit: 50))).Select(p => new PostViewModel()
            {
                Post = p
            });

            _posts.AddRange(posts);

            MainList.ItemsSource = _posts;

            PostLoadingProgressRing.IsActive = false;
            PostLoadingProgressRing.Visibility = Visibility.Collapsed;

            var scrollViewer = ListHelpers.GetScrollViewer(MainList);

            scrollViewer.ViewChanged += ScrollViewer_ViewChanged;
        }

        private async void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;

            if (isLoadingMore || scrollViewer.VerticalOffset > scrollViewer.ScrollableHeight - 50)
                return;

            isLoadingMore = true;

            FooterProgress.Visibility = Visibility.Visible;

            var posts = (await App.Client.GetUserPostsAsync(_user.Name, new(after: _posts.Last().Post.Name, limit: 50))).Select(p => new PostViewModel()
            {
                Post = p
            });

            _posts.AddRange(posts);

            FooterProgress.Visibility = Visibility.Collapsed;

            isLoadingMore = false;
        }

        [RelayCommand]
        private void SubredditClick(string subreddit)
            => Frame.Navigate(typeof(SubredditInfoPage), subreddit.Substring(2));

        [RelayCommand]
        private void TitleClick(PostViewModel model)
            => ((Frame)Window.Current.Content).Navigate(typeof(PostDetailsPage), new PostDetailsNavigationInfo()
            {
                ShowFullPage = true,
                ItemData = model
            });
    }
}

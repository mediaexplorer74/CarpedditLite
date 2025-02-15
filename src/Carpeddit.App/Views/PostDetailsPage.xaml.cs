﻿using Carpeddit.Api.Helpers;
using Carpeddit.Api.Models;
using Carpeddit.Api.Services;
using Carpeddit.App.Models;
using Carpeddit.App.ViewModels;
using Carpeddit.Common.Collections;
using Carpeddit.Common.Messages;
using Carpeddit.Models.Api;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Carpeddit.App.Views
{
    public sealed partial class PostDetailsPage : Page
    {
        private PostViewModel ViewModel => DataContext as PostViewModel;

        private bool _showTitleBar;

        private BulkObservableCollection<IPostReplyable> comments = new();

        public PostDetailsPage()
        {
            InitializeComponent();

            Loaded += OnLoaded;

            //RnD
            //TitleBar.Loaded += (_, _1) => TitleBar.SetAsTitleBar();
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnLoaded;

            if (_showTitleBar)
            {
                //RnD
                //TitleBar.Visibility = Visibility.Visible;
                BackButton.Visibility = Visibility.Visible;
                //TitleBar.SetAsTitleBar();
            } else
            {
                //TitleBar.Visibility = Visibility.Collapsed;
                BackButton.Visibility = Visibility.Collapsed;
            }

            CommentsLoadingRing.IsActive = true;
            CommentsLoadingRing.Visibility = Visibility.Visible;

            var comments = await GetCommentsAsViewModelAsync(ViewModel.Post.Name.Substring(3));

            this.comments.AddRange(comments);

            MainList.ItemsSource = this.comments;

            CommentsLoadingRing.IsActive = false;
            CommentsLoadingRing.Visibility = Visibility.Collapsed;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var navInfo = e.Parameter as PostDetailsNavigationInfo;
            DataContext = navInfo.ItemData;

            _showTitleBar = navInfo.ShowFullPage;
        }

        private void OnBackButtonClick(object sender, RoutedEventArgs e)
        {
            if (!Frame.CanGoBack)
                return;

            Frame.GoBack();
        }

        private Task<IEnumerable<IPostReplyable>> GetCommentsAsViewModelAsync(string postName)
            => (Ioc.Default.GetService<IRedditService>() as RedditService).RunAsync(async () =>
            {
                var response = await WebHelper.GetDeserializedResponseAsync<IList<Listing<IList<ApiObjectWithKind<object>>>>>($"/comments/{postName}?raw_json=1");

                // First listing is always the post.
                response.RemoveAt(0);

                return response.FirstOrDefault().Data.Children.Select<ApiObjectWithKind<object>, IPostReplyable>(obj =>
                {
                    if (obj.Kind == "more")
                        return JsonSerializer.Deserialize<More>(obj.Data.ToString());

                    return new CommentViewModel()
                    {
                        Comment = JsonSerializer.Deserialize<Comment>(obj.Data.ToString())
                    };
                });
            });

        [RelayCommand]
        private async Task SubredditClick(string subreddit)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
            else
                Frame.Navigate(typeof(MainPage));

            await Task.Delay(400);

            WeakReferenceMessenger.Default.Send<MainFrameNavigationMessage>(new()
            {
                Page = typeof(SubredditInfoPage),
                Parameter = subreddit.Substring(2)
            });
        }

        [RelayCommand]
        private async Task UserClick(string username)
        {
            if (username == "[deleted]")
                return;

            if (Frame.CanGoBack)
                Frame.GoBack();
            else
                Frame.Navigate(typeof(MainPage));

            await Task.Delay(400);

            WeakReferenceMessenger.Default.Send<MainFrameNavigationMessage>(new()
            {
                Page = typeof(ProfilePage),
                Parameter = username
            });
        }
    }

    public sealed class CommentItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CommentItemTemplate { get; set; }

        public DataTemplate MoreItemTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item is CommentViewModel)
                return CommentItemTemplate;

            if (item is More)
                return MoreItemTemplate;

            return base.SelectTemplateCore(item);
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is CommentViewModel)
                return CommentItemTemplate;

            if (item is More)
                return MoreItemTemplate;

            return base.SelectTemplateCore(item, container);
        }
    }
}

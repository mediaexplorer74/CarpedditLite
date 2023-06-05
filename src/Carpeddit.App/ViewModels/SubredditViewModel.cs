using Carpeddit.Api.Models;
using Carpeddit.Api.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace Carpeddit.App.ViewModels
{
    public sealed partial class SubredditViewModel : ObservableObject
    {
        [ObservableProperty]
        private Subreddit subreddit;

        public string Title
        {
            get
            {
                return "Title";//string.IsNullOrWhiteSpace(Subreddit.Title) ? Subreddit.HeaderTitle : Subreddit.Title;
            }
        }

        public string FullName
        {
            get
            {
                return "FullName";//Subreddit.Name;
            }
        }

        public string DisplayName
        {
            get
            {
                return "DisplayName";//Subreddit.DisplayName;
            }
        }

        public string BannerUrl
        {
            get
            {
                return "BannerURL";//Subreddit.BannerBackgroundImage;
            }
        }

        public bool IsTitleBlank
        {
            get
            {
                //RnD
                return true;//string.IsNullOrWhiteSpace(Subreddit.Title) || Subreddit.Title.Equals(Subreddit.DisplayNamePrefixed);
            }
        }

        public bool IsSubscribed
        {
            get => false;//Subreddit.UserIsSubscriber ?? false;
            set
            {
                //Subreddit.UserIsSubscriber = value;
                OnPropertyChanged();
            }
        }

        public string Subreddit { get; set; }
               

        private readonly IRedditService service = Ioc.Default.GetService<IRedditService>();

        public SubredditViewModel(Subreddit subreddit)
        {
            Subreddit = "subreddit";//subreddit;
        }

        [RelayCommand]
        public async Task JoinAsync()
        {
            try
            {
                var subreddits = new[] { FullName };

                if (IsSubscribed)
                {
                    await service.UnsubscribeFromSubredditsAsync(subreddits);
                    IsSubscribed = false;
                    return;
                }

                await service.SubscribeToSubredditsAsync(subreddits);
                IsSubscribed = true;
            }
            catch (Exception) { }
        }
    }
}

﻿using Carpeddit.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace Carpeddit.App.ViewModels
{
    public sealed partial class PostViewModel : ObservableObject
    {
        private string description;
        private int? voteRatio;

        public Post Post { get; set; }

        public string Title
        {
            get => Post.Title;
            set
            {
                Post.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Description
            => description ??= Post.IsSelf ? Post.Selftext : Post.Url;

        public DateTime Created
        {
            get => Post.Created;
            set
            {
                Post.Created = value;
                OnPropertyChanged(nameof(Created));
            }
        }

        public int VoteRatio
        {
            get => voteRatio ??= Post.Ups - Post.Downs;
            private set
            {
                voteRatio = value;
                OnPropertyChanged(nameof(VoteRatio));
            }
        }
    }
}

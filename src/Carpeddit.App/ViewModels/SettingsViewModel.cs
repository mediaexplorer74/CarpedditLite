﻿using Carpeddit.App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Carpeddit.App.ViewModels
{
    public sealed class SettingsViewModel
    {
        private ISettingsService _settingsService = App.Services.GetService<ISettingsService>();

        public int SetupProgress
        {
            get => settingsService.GetValue(0);
            set => settingsService.SetValue(value);
        }
    }
}
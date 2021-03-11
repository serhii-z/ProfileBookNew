﻿using Prism.Navigation;
using Prism.Services;
using ProfileBook.Servises.Authentication;
using ProfileBook.Servises.Authorization;
using ProfileBook.Servises.Settings;
using ProfileBook.Views;
using System.Windows.Input;
using Xamarin.Forms;
using ProfileBook.Properties;
using ProfileBook.ResourceActivator;
using System.ComponentModel;

namespace ProfileBook.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        private ISettingsManager _settingsManager;
        private IAuthenticationService _authenticationService;
        private IAuthorizationService _authorizationService;
        private IPageDialogService _pageDialog;
        private ICultureActivator _cultureActivator;
        private IThemeActivator _themeActivator;

        public SignInViewModel(INavigationService navigationService,
            ISettingsManager settingsManager, IAuthorizationService authorizationService, 
            IAuthenticationService authenticationService, IPageDialogService pageDialog, 
            ICultureActivator cultureActivator, IThemeActivator themeActivator) :
            base(navigationService)
        {
            _settingsManager = settingsManager;
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
            _pageDialog = pageDialog;
            _cultureActivator = cultureActivator;
            _themeActivator = themeActivator;
        }

        #region --- Public Properties ---
        public ICommand LogInTapCommand => new Command(OnLogInTap);
        public ICommand SignUpTapCommand => new Command(OnSignUpTap);


        private string _entryLoginText;
        public string EntryLoginText
        {
            get => _entryLoginText;
            set => SetProperty(ref _entryLoginText, value);
        }

        private string _entryPasswordText;
        public string EntryPasswordText
        {
            get => _entryPasswordText;
            set => SetProperty(ref _entryPasswordText, value);
        }

        private bool _enabledButton = false;
        public bool EnabledButton
        {
            get => _enabledButton;
            set => SetProperty(ref _enabledButton, value);
        }

        #endregion

        #region --- Private Methods ---

        private void CheckTextInput(string elementText)
        {
            if (string.IsNullOrEmpty(elementText))
            {
                MakeButtonInActive();
            }
            else
            {
                MakeButtonActive();
            }
        }

        private void MakeButtonActive()
        {
            if (!string.IsNullOrEmpty(_entryLoginText) &&
                !string.IsNullOrEmpty(_entryPasswordText))
                EnabledButton = true;
        }

        private void MakeButtonInActive()
        {
            EnabledButton = false;
        }

        private bool IsAuthorization()
        {
            var userId = _authenticationService.VerifyUser(_entryLoginText, _entryPasswordText);

            if (userId > 0)
            {
                _authorizationService.AddOrUpdateAuthorization(userId);
                return true;
            }

            ShowAlert(Resource.SignInAlert);
            EntryLoginText = string.Empty;
            EntryPasswordText = string.Empty;

            return false; 
        }

        private async void ShowAlert(string message)
        {
            await _pageDialog.DisplayAlertAsync(Resource.AlertTitle, message, "OK");
        }

        #endregion

        #region --- Private Helpers ---

        private async void OnLogInTap()
        {
            if (IsAuthorization())
            {
                _cultureActivator.AplyCulture();

                await navigationService.NavigateAsync($"{nameof(MainListView)}");
            }
        }

        private async void OnSignUpTap()
        {
            await navigationService.NavigateAsync($"{nameof(SignUpView)}");
        }

        #endregion

        #region --- Overrides ---

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(EntryLoginText))
            {
                CheckTextInput(_entryLoginText);
            }

            if (args.PropertyName == nameof(EntryPasswordText))
            {
                CheckTextInput(_entryPasswordText);
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue("login", out string login))
            {
                EntryLoginText = login;
            }            
        }

        public override void Initialize(INavigationParameters parameters)
        {
            _themeActivator.AplyTheme();
        }

        #endregion
    }
}

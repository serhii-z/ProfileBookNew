using Prism.Navigation;
using ProfileBook.ResourceActivator;
using ProfileBook.Resources.Themes;
using ProfileBook.Servises.Settings;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private ISettingsManager _settingsManager;
        private ICultureActivator _cultureActivator;
        public SettingsViewModel(INavigationService navigationService, ISettingsManager settingsManager,
            ICultureActivator cultureActivator) :
            base(navigationService)
        {
            _settingsManager = settingsManager;
            _cultureActivator = cultureActivator;
        }

        #region --- Public Properties ---

        public ICommand GoBackButtonTapCommand => new Command(OnGoBackButtonTap);

        private bool _isName;
        public bool IsName
        {
            get => _isName;
            set => SetProperty(ref _isName, value);
        }

        private bool _isNickName;
        public bool IsNickName
        {
            get => _isNickName;
            set => SetProperty(ref _isNickName, value);
        }

        private bool _isTime;
        public bool IsTime
        {
            get => _isTime;
            set => SetProperty(ref _isTime, value);
        }

        private bool _isDark;
        public bool IsDark
        {
            get => _isDark;
            set => SetProperty(ref _isDark, value);
        }

        private bool _isUkrainian;
        public bool IsUkrainian
        {
            get => _isUkrainian;
            set => SetProperty(ref _isUkrainian, value);
        }

        private bool _isRussian;
        public bool IsRussian
        {
            get => _isRussian;
            set => SetProperty(ref _isRussian, value);
        }

        #endregion

        #region --- Private Methods ---

        private void ActivateControlsSorting()
        {
            IsName = _settingsManager.IsSortingByName;
            IsNickName = _settingsManager.IsSortingByNickName;
            IsTime = _settingsManager.IsSortingByTime;
        }

        private void ActivateControlsTheme()
        {
            IsDark = _settingsManager.IsDarkTheme;
        }

        private void ActivateControlsCulture()
        {
            IsUkrainian = _settingsManager.IsUkrainianCulture;
            IsRussian = _settingsManager.IsRussianCulture;
        }

        private void UncheckSorting(bool isName, bool isNickName, bool isTime)
        {
            IsName = isName;
            IsNickName = isNickName;
            IsTime = isTime;
        }

        private void UncheckCulture(bool isUkrainian, bool isRussian)
        {
            IsUkrainian = isUkrainian;
            IsRussian = isRussian;
        }

        #endregion

        #region --- Private Helpers ---
        private async void OnGoBackButtonTap()
        {
            await navigationService.GoBackAsync();
        }

        #endregion

        #region --- Overrides ---

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(IsName))
            {
                UncheckSorting(_isName, false, false);
                _settingsManager.IsSortingByName = _isName;

                if (_isName)
                    _settingsManager.SortingName = "Name";
                else
                    _settingsManager.SortingName = string.Empty;
           
            }

            if (args.PropertyName == nameof(IsNickName))
            {
                UncheckSorting(false, _isNickName, false);
                _settingsManager.IsSortingByNickName = _isNickName;

                if (_isNickName)
                    _settingsManager.SortingName = "NickName";
                else
                    _settingsManager.SortingName = string.Empty;

            }

            if (args.PropertyName == nameof(IsTime))
            {
                UncheckSorting(false, false, _isTime);
                _settingsManager.IsSortingByTime = _isTime;

                if (_isTime)
      
                    _settingsManager.SortingName = "CreationTime";
                else
                    _settingsManager.SortingName = string.Empty;
                
            }

            if (args.PropertyName == nameof(IsDark))
            {
                _settingsManager.IsDarkTheme = _isDark;

                if (_isDark)
                    _settingsManager.ThemeName = nameof(DarkTheme);
                else
                    _settingsManager.ThemeName = string.Empty;
            }

            if (args.PropertyName == nameof(IsUkrainian))
            {
                UncheckCulture(_isUkrainian, false);
                _settingsManager.IsUkrainianCulture = _isUkrainian;

                if (_isUkrainian)
                    _settingsManager.CultureName = "uk";
                else
                    _settingsManager.CultureName = string.Empty;
                
                _cultureActivator.AplyCulture();
            }

            if (args.PropertyName == nameof(IsRussian))
            {
                UncheckCulture(false, _isRussian);
                _settingsManager.IsRussianCulture = _isRussian;

                if (_isRussian)
                    _settingsManager.CultureName = "ru";
                else
                    _settingsManager.CultureName = string.Empty;

                _cultureActivator.AplyCulture();
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            ActivateControlsSorting();
            ActivateControlsCulture();
            ActivateControlsTheme();
        }

        #endregion
    }
}

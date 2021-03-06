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
                if (_isName)
                {
                    UncheckSorting(_isName, false, false);
                    _settingsManager.IsSortingByName = _isName;
                    _settingsManager.SortingName = "Name";
                }
                else
                {
                    _settingsManager.IsSortingByName = false;
                    _settingsManager.SortingName = string.Empty;
                }
            }

            if (args.PropertyName == nameof(IsNickName))
            {
                if (_isNickName)
                {
                    UncheckSorting(false, _isNickName, false);
                    _settingsManager.IsSortingByNickName = _isNickName;
                    _settingsManager.SortingName = "NickName";
                }
                else
                {
                    _settingsManager.IsSortingByNickName = false;
                    _settingsManager.SortingName = string.Empty;
                }
            }

            if (args.PropertyName == nameof(IsTime))
            {
                if (_isTime)
                {
                    UncheckSorting(false, false, _isTime);
                    _settingsManager.IsSortingByTime = _isTime;
                    _settingsManager.SortingName = "CreationTime";
                }
                else
                {
                    _settingsManager.IsSortingByTime = false;
                    _settingsManager.SortingName = string.Empty;
                }
            }

            if (args.PropertyName == nameof(IsDark))
            {
                if (_isDark)
                {
                    _settingsManager.IsDarkTheme = _isDark;
                    _settingsManager.ThemeName = nameof(DarkTheme);
                }
                else
                {
                    _settingsManager.IsDarkTheme = false;
                    _settingsManager.ThemeName = string.Empty;
                }
            }

            if (args.PropertyName == nameof(IsUkrainian))
            {
                if (_isUkrainian)
                {
                    IsRussian = false;
                    _settingsManager.IsUkrainianCulture = _isUkrainian;
                    _settingsManager.CultureName = "uk";
                }
                else
                {
                    _settingsManager.IsUkrainianCulture = false;
                    _settingsManager.CultureName = string.Empty;
                }
                _cultureActivator.AplyCulture();
            }

            if (args.PropertyName == nameof(IsRussian))
            {
                if (_isRussian)
                {
                    IsUkrainian = false;
                    _settingsManager.IsRussianCulture = _isRussian;
                    _settingsManager.CultureName = "ru";
                }
                else
                {
                    _settingsManager.IsRussianCulture = false;
                    _settingsManager.CultureName = string.Empty;
                }
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

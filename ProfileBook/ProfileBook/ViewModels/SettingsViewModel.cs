using Prism.Navigation;
using ProfileBook.Resources.Themes;
using ProfileBook.Services.Localization;
using ProfileBook.Servises.Settings;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private ISettingsManager _settingsManager;
        private ILocalizationService _localizationService;
        public SettingsViewModel(INavigationService navigationService, ISettingsManager settingsManager,
            ILocalizationService localizationService) :
            base(navigationService)
        {
            _settingsManager = settingsManager;
            _localizationService = localizationService;
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

        private void UpdateSortingName(bool isChecked, string sortingName)
        {
            if(isChecked)
                _settingsManager.SortingName = sortingName;          
            else
                _settingsManager.SortingName = string.Empty;
        }

        private void UpdateThemeName(bool isChecked, string themeName)
        {
            if (isChecked)
                _settingsManager.ThemeName = themeName;
            else
                _settingsManager.ThemeName = string.Empty;
        }

        private void UpdateCultureName(bool isChecked, string cultureName)
        {
            if (isChecked)
                _settingsManager.CultureName = cultureName;
            else
                _settingsManager.CultureName = string.Empty;
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
                UpdateSortingName(_isName, "Name");           
            }

            if (args.PropertyName == nameof(IsNickName))
            {
                UncheckSorting(false, _isNickName, false);
                _settingsManager.IsSortingByNickName = _isNickName;
                UpdateSortingName(_isNickName, "NickName");
            }

            if (args.PropertyName == nameof(IsTime))
            {
                UncheckSorting(false, false, _isTime);
                _settingsManager.IsSortingByTime = _isTime;
                UpdateSortingName(_isTime, "CreationTime");
            }

            if (args.PropertyName == nameof(IsDark))
            {
                _settingsManager.IsDarkTheme = _isDark;
                UpdateThemeName(_isDark, nameof(DarkTheme));
            }

            if (args.PropertyName == nameof(IsUkrainian))
            {
                UncheckCulture(_isUkrainian, false);
                _settingsManager.IsUkrainianCulture = _isUkrainian;
                UpdateCultureName(_isUkrainian, "uk");
                _localizationService.AplyCulture();
            }

            if (args.PropertyName == nameof(IsRussian))
            {
                UncheckCulture(false, _isRussian);
                _settingsManager.IsRussianCulture = _isRussian;
                UpdateCultureName(_isRussian, "ru");
                _localizationService.AplyCulture();
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

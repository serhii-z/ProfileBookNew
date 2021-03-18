using ProfileBook.Resources.Themes;
using Xamarin.Essentials;

namespace ProfileBook.Servises.Settings
{
    public class SettingsManager : ISettingsManager
    {
        public bool IsSortingByName
        {
            get => Preferences.Get(nameof(IsSortingByName), false);
            set => Preferences.Set(nameof(IsSortingByName), value);
        }

        public bool IsSortingByNickName
        {
            get => Preferences.Get(nameof(IsSortingByNickName), false);
            set => Preferences.Set(nameof(IsSortingByNickName), value);
        }

        public bool IsSortingByTime
        {
            get => Preferences.Get(nameof(IsSortingByTime), false);
            set => Preferences.Set(nameof(IsSortingByTime), value);
        }

        public bool IsDarkTheme
        {
            get => Preferences.Get(nameof(IsDarkTheme), false);
            set => Preferences.Set(nameof(IsDarkTheme), value);
        }

        public bool IsUkrainianCulture
        {
            get => Preferences.Get(nameof(IsUkrainianCulture), false);
            set => Preferences.Set(nameof(IsUkrainianCulture), value);
        }

        public bool IsRussianCulture
        {
            get => Preferences.Get(nameof(IsRussianCulture), false);
            set => Preferences.Set(nameof(IsRussianCulture), value);
        }

        public string SortingName
        {
            get => Preferences.Get(nameof(SortingName), string.Empty);
            set => Preferences.Set(nameof(SortingName), value);
        }

        public string ThemeName
        {
            get => Preferences.Get(nameof(ThemeName), nameof(LightTheme));
            set => Preferences.Set(nameof(ThemeName), value);
        }

        public string CultureName
        {
            get => Preferences.Get(nameof(CultureName), "en");
            set => Preferences.Set(nameof(CultureName), value);
        }
    }
}

using ProfileBook.Resources.Themes;
using ProfileBook.Servises.Settings;

namespace ProfileBook.ResourceActivator
{
    public class ThemeActivator : IThemeActivator
    {
        private ISettingsManager _settingsManager;

        public ThemeActivator(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }
        public void AplyTheme()
        {
            switch (_settingsManager.ThemeName)
            {
                case nameof(DarkTheme):
                    App.Current.Resources.Add(new DarkTheme());
                    break;
                default:
                    App.Current.Resources.Add(new LightTheme());
                    break;
            }
        }
    }
}

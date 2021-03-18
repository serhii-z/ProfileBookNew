using ProfileBook.Resources.Themes;
using ProfileBook.Servises.Settings;

namespace ProfileBook.Services.Resource
{
    public class ResourceService : IResourceService
    {
        private ISettingsManager _settingsManager;

        public ResourceService(ISettingsManager settingsManager)
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

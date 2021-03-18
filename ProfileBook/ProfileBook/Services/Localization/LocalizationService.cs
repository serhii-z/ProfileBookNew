using ProfileBook.Properties;
using ProfileBook.Servises.Settings;
using System.Globalization;
using Xamarin.Essentials;

namespace ProfileBook.Services.Localization
{
    public class LocalizationService : ILocalizationService
    {
        private ISettingsManager _settingsManager;

        public LocalizationService(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }
        public void AplyCulture()
        {
            var cultureName = Preferences.Get(nameof(_settingsManager.CultureName), "en");

            if (string.IsNullOrEmpty(cultureName))
            {
                cultureName = "en";
            }

            Resource.Culture = new CultureInfo(cultureName, false);
        }
    }
}

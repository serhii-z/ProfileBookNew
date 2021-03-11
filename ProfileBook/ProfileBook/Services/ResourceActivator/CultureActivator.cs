using ProfileBook.Properties;
using ProfileBook.Servises.Settings;
using System.Globalization;
using Xamarin.Essentials;

namespace ProfileBook.ResourceActivator
{
    public class CultureActivator : ICultureActivator
    {
        private ISettingsManager _settingsManager;

        public CultureActivator(ISettingsManager settingsManager)
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

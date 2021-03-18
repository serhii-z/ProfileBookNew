namespace ProfileBook.Servises.Settings
{
    public interface ISettingsManager
    {
        bool IsSortingByName { get; set; }
        bool IsSortingByNickName { get; set; }
        bool IsSortingByTime { get; set; }
        bool IsDarkTheme { get; set; }
        bool IsUkrainianCulture { get; set; }
        bool IsRussianCulture { get; set; }
        string SortingName { get; set; }
        string ThemeName { get; set; }
        string CultureName { get; set; }
    }
}

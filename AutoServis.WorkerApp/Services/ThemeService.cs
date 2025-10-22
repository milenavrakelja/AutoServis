using System;
using System.Windows;

namespace AutoServis.WorkerApp.Services
{
    public static class ThemeService
    {
        
        public static string CurrentTheme { get; private set; }

        public static void ApplyTheme(string themeName)
        {
            CurrentTheme = themeName;

            var app = Application.Current;
            var dictionaries = app.Resources.MergedDictionaries;

           
            for (int i = dictionaries.Count - 1; i >= 0; i--)
            {
                if (dictionaries[i].Source?.OriginalString.Contains("/Themes/") == true)
                {
                    dictionaries.RemoveAt(i);
                }
            }

          
            string uri = themeName switch
            {
                "Blue" => "pack://application:,,,/AutoServis.Core;component/Themes/BlueTheme.xaml",
                "Green" => "pack://application:,,,/AutoServis.Core;component/Themes/GreenTheme.xaml",
                "Red" => "pack://application:,,,/AutoServis.Core;component/Themes/RedTheme.xaml",
                _ => "pack://application:,,,/AutoServis.Core;component/Themes/BlueTheme.xaml"
            };

            try
            {
                dictionaries.Add(new ResourceDictionary { Source = new Uri(uri) });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Greška: {ex.Message}");
            }
        }
    }
}
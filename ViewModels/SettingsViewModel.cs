using System.Collections.Generic;

namespace Coursework.ViewModels
{
    public class SettingsViewModel
    {
        public IEnumerable<string> Themes{ get; set; }
        public string TargetTheme { get; set; }
        public string TargetCulture { get; set; }
    }
}

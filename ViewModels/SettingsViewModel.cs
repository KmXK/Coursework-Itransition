using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Coursework.ViewModels
{
    public class SettingsViewModel
    {
        public IEnumerable<SelectListItem> Themes{ get; set; }
        public IEnumerable<SelectListItem> Cultures { get; set; }
        public string TargetTheme { get; set; }
        public string TargetCulture { get; set; }
    }
}

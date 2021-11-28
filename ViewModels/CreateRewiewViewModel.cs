using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Coursework.ViewModels
{
    public class CreateRewiewViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public int Rating { get; set; }
        public IEnumerable<string> Groups { get; set; }
        [Required]
        public string SelectedGroup { get; set; }
    }
}

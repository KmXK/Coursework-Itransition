using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Coursework.ViewModels
{
    public class CreateRewiewViewModel
    {
        [Required(ErrorMessage = "TitleRequired")]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "TextRequired")]
        [Display(Name = "Text")]
        public string Text { get; set; }
        [Range(0, 10, ErrorMessage = "RatingRange")]
        [Display(Name = "Rating")]
        public int Rating { get; set; }
        public IEnumerable<string> Groups { get; set; }
        [Display(Name = "SelectedGroup")]
        public string SelectedGroup { get; set; }
    }
}

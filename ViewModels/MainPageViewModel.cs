using System.Collections.Generic;
using Coursework.Models;

namespace Coursework.ViewModels
{
    public class MainPageViewModel
    {
        public IEnumerable<Review> LastReview { get; set; }
        public IEnumerable<Review> PopularReviews { get; set; }
    }
}

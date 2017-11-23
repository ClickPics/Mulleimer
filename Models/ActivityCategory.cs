using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZenithWebsite.Models
{
    public class ActivityCategory
    {
        [Display(Name = "Activity Category")]
        public int ActivityCategoryId { get; set; }

        [Display(Name = "Activity Description")]
        public string ActivityDescription { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}

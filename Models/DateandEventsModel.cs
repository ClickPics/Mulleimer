using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZenithWebsite.Models
{
    public class DateandEventsModel
    {
        public string DayOfWeek { get; set; }
        public List<ApiEventModel> Events {get;set;}
    }
}

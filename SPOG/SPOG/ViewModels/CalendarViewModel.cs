using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOG.ViewModels
{
    public class CalenderViewModel
    {
        public static List<Event> CalenderEventsList { get; set; }
        public static async Task GetCalendarEvents()
        {
            var CalenderEvents = await App.GraphClient.Me.Events.Request()
                .Select("subject,organizer,start,end")
                .OrderBy("createdDateTime DESC")
                .GetAsync();
            CalenderEventsList = CalenderEvents.CurrentPage.ToList();
        }

    }
}

namespace SampleApi.Models
{
    public class CalendarItem
    {
        public string Id { get; set; }
        public Event Event { get; set; }
        public Person Person { get; set; }
    }
}
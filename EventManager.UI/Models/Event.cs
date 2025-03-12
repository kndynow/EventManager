namespace EventManager.UI.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int MaxAttendees { get; set; }
    }
}

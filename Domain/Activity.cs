using System;

namespace Domain
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Discription { get; set; }
        public string Category { get; set; }
        public string Sity { get; set; }
        public string Venue { get; set; }
    }
}
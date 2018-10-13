namespace Teeleh.Models
{
    public class Location
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string  Name { get; set; }
        public double XCordination { get; set; }
        public double YCordination { get; set; }

    }
}

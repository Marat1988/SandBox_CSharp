namespace DapperBeginnerCourse.Models
{
    public class VideoGame
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Publisher { get; set; }
        public required string Developer { get; set; }
        public required DateTime ReleaseDate { get; set; }
    }
}

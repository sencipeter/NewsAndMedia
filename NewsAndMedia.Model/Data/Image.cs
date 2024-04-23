namespace NewsAndMedia.Model.Data
{
    public class Image
    {
        public long Id { get; set; } // Primary key
        public string Description { get; set; }
        public virtual Author? Author { get; set; } // One-To-One
    }
}

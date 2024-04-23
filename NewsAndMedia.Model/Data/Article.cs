namespace NewsAndMedia.Model.Data
{
    public class Article
    {
        public long Id { get; set; } // Primary key
        public string Title { get; set; } // Index
        public virtual ICollection<Author> Author { get; set; } = new List<Author>(); // Many-To-Many
        public long SiteId { get; set; }
        public virtual Site? Site { get; set; } // One-To-Many
    }
}

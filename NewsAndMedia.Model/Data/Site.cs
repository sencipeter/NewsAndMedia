namespace NewsAndMedia.Model.Data
{
    public class Site
    {
        public long Id { get; set; } // Primary key
        public DateTimeOffset CreatedAt { get; set; }

        public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}
namespace NewsAndMedia.Model.Data
{
    public class Author
    {
        public long Id { get; set; } // Primary key
        public string Name { get; set; } // Unique index

        public long? ImageId { get; set; }
        public virtual Image? Image { get; set; } // One-To-One
        public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}

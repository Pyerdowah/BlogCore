using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlogCore.DAL.Model;

namespace Blog.DAL.Model
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Author { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}

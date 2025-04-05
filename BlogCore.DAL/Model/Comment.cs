using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.DAL.Model;

namespace BlogCore.DAL.Model
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public long PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Comment comment &&
                   Id == comment.Id &&
                   Content == comment.Content &&
                   PostId == comment.PostId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Content, PostId);
        }
    }
}

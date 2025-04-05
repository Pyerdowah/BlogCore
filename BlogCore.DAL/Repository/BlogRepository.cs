using System.Collections.Generic;
using Blog.DAL.Infrastructure;
using Blog.DAL.Model;
using System;
using BlogCore.DAL.Model;

namespace Blog.DAL.Repository
{
    public class BlogRepository
    {
        private readonly BlogContext _context;

        public BlogRepository(string connectionString)
        {
            _context = new BlogContext(connectionString);
        }

        public void ClearDb()
        {
            _context.Posts.ToList().ForEach(x => _context.Posts.Remove(x));
            _context.SaveChanges();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Posts;
        }

        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return _context.Comments;
        }

        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public IEnumerable<Comment> GetCommentsByPost(Post post)
        {
            return GetAllPosts().FirstOrDefault(p => p.Id == post.Id).Comments;
        }
    }
}

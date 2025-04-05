using Blog.DAL.Infrastructure;
using Blog.DAL.Repository;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using Blog.DAL.Model;
using TDD.DbTestHelpers.Core;
using BlogCore.DAL.Tests;
using Microsoft.EntityFrameworkCore;
using BlogCore.DAL.Model;

namespace Blog.DAL.Tests
{

    [TestClass]
    public class RepositoryTests : DbBaseTest<BlogFixtures>
    {
        private static string _connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("BloggingDatabase");

        public static string GetConnectionString(string name)
        {
            Configuration config =
            ConfigurationManager.OpenExeConfiguration(
            ConfigurationUserLevel.None);
            ConnectionStringsSection csSection =
            config.ConnectionStrings;
            for (int i = 0; i <
            ConfigurationManager.ConnectionStrings.Count; i++)
            {
                ConnectionStringSettings cs =
                csSection.ConnectionStrings[i];
                if (cs.Name == name)
                {
                    return cs.ConnectionString;
                }
            }
            return "";
        }

        [TestMethod]
        public void GetAllPost_OnePostInDb_ReturnOnePost()
        {
            // arrange
            var context = new BlogContext(_connectionString);
            context.Database.EnsureCreated();
            var repository = new BlogRepository(_connectionString);
            repository.ClearDb();

            // -- prepare data in db
            context.Posts.ToList().ForEach(x => context.Posts.Remove(x));
            context.Posts.Add(new Post
            {
                Author = "test",
                Content = "test, test, test..."
            });
            context.SaveChanges();
            // act
            var result = repository.GetAllPosts();
            // assert
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void GetAllPost_TwoPostInDb_ReturnTwoPosts()
        {
            // arrange
            var repository = new BlogRepository(_connectionString);
            repository.ClearDb();

            BaseFixtureSetUp();
            BaseSetUp();
            // act
            var result = repository.GetAllPosts();
            // assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void AddPost_ReturnOneMorePost()
        {
            // arrange
            var repository = new BlogRepository(_connectionString);
            repository.ClearDb();

            BaseFixtureSetUp();
            BaseSetUp();
         
            // act
            var resultBeforeAdd = repository.GetAllPosts().Count();
            repository.AddPost(new Post { Id = 3, Author = "ja", Content = "fajny content" });
            var resultAfterAdd = repository.GetAllPosts().Count();
            // assert
            Assert.AreEqual(resultBeforeAdd + 1, resultAfterAdd);
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void CanAddEmptyPost()
        {
            // arrange
            var repository = new BlogRepository(_connectionString);
            repository.ClearDb();

            BaseFixtureSetUp();
            BaseSetUp();

            // act
            repository.AddPost(new Post { Id = 3, Author = null , Content = null });
        }

        [TestMethod]
        public void GetAllComments_OneCommentInDb_ReturnOneComment()
        {
            // arrange
            var repository = new BlogRepository(_connectionString);
            repository.ClearDb();

            BaseFixtureSetUp();
            BaseSetUp();
            // act
            var result = repository.GetAllComments();
            // assert
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void AddComment_ReturnOneMoreComment()
        {
            // arrange
            var repository = new BlogRepository(_connectionString);
            repository.ClearDb();

            BaseFixtureSetUp();
            BaseSetUp();

            // act
            var resultBeforeAdd = repository.GetAllComments().Count();
            repository.AddComment(new Comment { Id = 2, PostId = 1, Content = "fajny komentarz" });
            var resultAfterAdd = repository.GetAllComments().Count();
            // assert
            Assert.AreEqual(resultBeforeAdd + 1, resultAfterAdd);
        }

        [TestMethod]
        public void GetSelectedPostCommentsList()
        {
            // arrange
            var repository = new BlogRepository(_connectionString);
            repository.ClearDb();

            BaseFixtureSetUp();
            BaseSetUp();

            var post3 = new Post { Id = 3, Author = "author", Content = "content" };
            var com2 = new Comment { Id = 2, PostId = 1, Content = "komentarz 2" };
            var com3 = new Comment { Id = 3, PostId = 3, Content = "komentarz 3" };
            var com4 = new Comment { Id = 4, PostId = 3, Content = "komentarz 4" };
            repository.AddPost(post3);
            repository.AddComment(com2);
            repository.AddComment(com3);
            repository.AddComment(com4);

            List<Comment> commentsPost3 = new List<Comment>();
            commentsPost3.Add(com3);
            commentsPost3.Add(com4);

            // act          
            List<Comment> retirevedComments = (List<Comment>) repository.GetCommentsByPost(post3);

            // assert
            CollectionAssert.AreEqual(commentsPost3, retirevedComments);
        }

        [TestMethod]
        public void GetSelectedPostCommentsList2()
        {
            // arrange
            var repository = new BlogRepository(_connectionString);
            repository.ClearDb();
            BaseFixtureSetUp();
            BaseSetUp();

            var post3 = new Post { Id = 3, Author = "author", Content = "content" };
            var com2 = new Comment { Id = 2, PostId = 1, Content = "komentarz 2" };
            var com3 = new Comment { Id = 3, PostId = 3, Content = "komentarz 3" };
            var com4 = new Comment { Id = 4, PostId = 3, Content = "komentarz 4" };
            repository.AddPost(post3);
            repository.AddComment(com2);
            repository.AddComment(com3);
            repository.AddComment(com4);

            List<Comment> commentsPost3 = new List<Comment>();
            commentsPost3.Add(com3);
            commentsPost3.Add(com4);

            // act          
            List<Comment> retirevedComments = (List<Comment>)repository.GetCommentsByPost(post3);

            // assert
            CollectionAssert.AreEqual(commentsPost3, retirevedComments);
        }
    }
}

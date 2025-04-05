using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.DAL.Model;
using BlogCore.DAL.Model;
using TDD.DbTestHelpers.Yaml;

namespace BlogCore.DAL.Tests
{
    public class BlogFixturesModel
    {
        public FixtureTable<Post> Posts { get; set; }
        public FixtureTable<Comment> Comments { get; set; }
    }
}

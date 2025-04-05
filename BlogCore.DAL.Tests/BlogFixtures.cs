using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.DAL.Infrastructure;
using TDD.DbTestHelpers.Yaml;

namespace BlogCore.DAL.Tests
{
    public class BlogFixtures : YamlDbFixture<BlogContext, BlogFixturesModel>
    {
        public BlogFixtures()
        {
            SetYamlFolderName("C:\\_TMP\\BlogCore\\BlogCore.DAL.Tests\\Fixtures\\");
            SetYamlFiles("posts.yml");
        }
    }
}

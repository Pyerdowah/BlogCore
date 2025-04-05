using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using TDD.DbTestHelpers.Core;
using TDD.DbTestHelpers.EF;
using TDD.DbTestHelpers.Helpers;
using YamlDotNet.Serialization;

namespace TDD.DbTestHelpers.Yaml
{
    public class YamlDbFixture<TContext, TFixtureType> : DbFixture<TContext> where TContext : DbContext, new()
    {
        private readonly FileHelper _fileHelper;
        private string _yamlFolderName = "";
        private string[] _yamlFilesNames = new[] {"fixtures.yaml"};

        public YamlDbFixture()
            : this(new FileHelper())
        {
            this._yamlFolderName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Fixtures");
        }

        public YamlDbFixture(FileHelper fileHelper)
        {
            _fileHelper = fileHelper;
        }

        public override void PrepareDatabase()
        {
            _fileHelper.ClearTables<TFixtureType>(Context);
        }

        public override void FillFixtures()
        {
            _fileHelper.FillFixturesFileFiles<TFixtureType>(Context, _yamlFolderName, _yamlFilesNames);
        }


        protected void SetYamlFolderName(string yamlFolderName)
        {
            _yamlFolderName = yamlFolderName;
        }

        protected void SetYamlFiles(params string[] yamlFiles)
        {
            _yamlFilesNames = yamlFiles;
        }
    }
}

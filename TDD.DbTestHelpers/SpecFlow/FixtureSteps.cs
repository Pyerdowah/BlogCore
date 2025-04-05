using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using TDD.DbTestHelpers.Helpers;
using TechTalk.SpecFlow;

namespace TDD.DbTestHelpers.SpecFlow
{
    [Binding]
    public class FixtureSteps
    {
        public static FileHelper Helper = new FileHelper();
        public static string YamlFileName = "fixtures.yaml";
        public static string YamlFolderName = "Fixtures";
        public static DbContext Context = null;
        public static Type FixtureModel = null;

        [BeforeFeature("fixture")]
        static public void BeforeFixtureFeature()
        {
            if (FixtureModel == null)
                throw new ArgumentException("FixtureSteps.FixtureModel cannot be null. Please specify fixture model");
            if (Context == null)
                throw new ArgumentException("FixtureSteps.Context cannot be null. Please specify db context.");
            Trace.WriteLine("Prepare DB and load fixtures");
            Helper.ClearTables(FixtureModel, Context);
            Helper.FillFixturesFileFiles(FixtureModel, Context, YamlFolderName, new[] {YamlFileName});
        }

        [AfterFeature("fixture")]
        static public void AfterFixtureFeature()
        {
            Trace.WriteLine("Clear DB");
        }
    }
}
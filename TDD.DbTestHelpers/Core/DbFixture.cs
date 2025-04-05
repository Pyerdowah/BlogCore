using Microsoft.EntityFrameworkCore;

namespace TDD.DbTestHelpers.Core
{
    public abstract class DbFixture<TContext> : IDbFixture where TContext : DbContext, new()
    {
        protected readonly TContext Context;

        public DbContext GetContext
        {
            get { return Context; }
        }

        protected DbFixture()
        {
            RefillBeforeEachTest = false;
            UseTransactionScope = false;
            Context = new TContext();
        }

        public bool UseTransactionScope { get; protected set; }
        public bool RefillBeforeEachTest { get; protected set; }
        public abstract void PrepareDatabase();
        public abstract void FillFixtures();
    }
}
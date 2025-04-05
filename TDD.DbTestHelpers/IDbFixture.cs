using Microsoft.EntityFrameworkCore;

namespace TDD.DbTestHelpers
{
    public interface IDbFixture
    {
        void PrepareDatabase();
        void FillFixtures();
        bool RefillBeforeEachTest { get; }
        bool UseTransactionScope { get; }
        DbContext GetContext { get; }
    }
}
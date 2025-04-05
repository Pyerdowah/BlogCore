using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDD.DbTestHelpers.EF
{
    public static class Extensions
    {
        public static void ClearTable<TEntity>(this DbSet<TEntity> table) where TEntity : class
        {
            foreach (var entity in table)
            {
                table.Remove(entity);
            }
        }
    }
}

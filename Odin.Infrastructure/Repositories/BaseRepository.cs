using System;

namespace Odin.Infrastructure.Repositories
{
    public abstract class BaseSqlRepository
    {
        protected string connectionString;

        public BaseSqlRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}

﻿using Odin.Core.Interfaces;
using Odin.Core.Model;
using System;
using System.Collections.Generic;
using Dapper;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;
using Odin.Core.Interfaces.Repositories;

namespace Odin.Infrastructure.Repositories
{
    public class UserRepository : BaseSqlRepository, IUserRepository
    {
        private const string SqlSelect = "SELECT [Id],[Name],[Email],[Password],[Activated],[CreatedOn],[Company] FROM [dbo].[User]";

        private const string SqlUpdate = "UPDATE [dbo].[User] SET [Name] =  @Name,[Email] = @Email,[Password] = @Password,[Activated] = @Activated,[Deleted] = @Deleted,[Company] = @Company";

        private const string SqlInsert = "INSERT INTO[dbo].[User]([Name],[Email],[Password],[CreatedOn],[Activated],[Deleted],[Company]) VALUES (@Name,@Email,@Password,@CreatedOn,@Activated,@Deleted,@Company) SELECT SCOPE_IDENTITY()";

        public UserRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task Delete(User entity)
        {
            string sql = SqlUpdate + " WHERE [Id]=@Id";
            entity.Deleted = true;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                await db.ExecuteAsync(sql, entity);
            }

        }

        public async Task<IEnumerable<User>> GetAll()
        {
            string sql = SqlSelect;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return await db.QueryAsync<User>(sql);
            }
        }

        public async Task<User> GetById(int id)
        {
            string sql = SqlSelect + " WHERE [Id]=@Id";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var result = await db.QueryAsync<User>(sql, new { Id = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<User> GetByNameOrEmail(string input)
        {
            string sql = SqlSelect + " WHERE ([Name]=@Input OR [Email]=@Input) AND [Deleted] = 0";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var result = await db.QueryAsync<User>(sql, new { Input = input });
                return result.FirstOrDefault();
            }
        }


        public async Task Update(User entity)
        {
            string sql = SqlUpdate + " WHERE [Id]=@Id";

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                await db.ExecuteAsync(sql, entity);
            }
        }

        async Task<int> IRepository<User>.Create(User entity)
        {
            string sql = SqlInsert;
            entity.CreatedOn = DateTime.Now;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return await db.ExecuteScalarAsync<int>(sql, entity);
            }
        }
    }
}

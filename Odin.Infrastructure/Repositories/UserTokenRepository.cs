﻿using Dapper;
using Odin.Core.Interfaces.Repositories;
using Odin.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odin.Infrastructure.Repositories
{
    public class UserTokenRepository : BaseSqlRepository, IUserTokenRepository
    {
        public UserTokenRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<int> Create(UserToken entity)
        {
            string sql = @"INSERT INTO [dbo].[UserToken]
                                   ([UserId]
                                   ,[Type]
                                   ,[Token]
                                   ,[ExpirationDate]
                                   ,[Used])
                             VALUES
                                   (@UserId
                                   ,@Type
                                   ,@Token
                                   ,@ExpirationDate
                                   ,@Used)
                                   SELECT SCOPE_IDENTITY()";

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return await db.ExecuteScalarAsync<int>(sql, entity);                
            }
        }

        public Task Delete(UserToken entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserToken>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UserToken> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserToken>> GetValidTokensByUserId(int userId)
        {
            string sql = @"SELECT [Id]
                              ,[UserId]
                              ,[Type]
                              ,[Token]
                              ,[ExpirationDate]
                              ,[Used]
                          FROM [dbo].[UserToken] WHERE [UserId]=@UserId";

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var userTokens = await db.QueryAsync<UserToken>(sql, new { UserId = userId });
                return userTokens.Where(x => x.ExpirationDate >= DateTime.Today);
            }           
        }

        public Task Update(UserToken entity)
        {
            throw new NotImplementedException();
        }
    }
}

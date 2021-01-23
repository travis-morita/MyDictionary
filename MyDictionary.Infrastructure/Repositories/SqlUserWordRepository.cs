using Dapper;
using MyDictionary.Core.Domain;
using MyDictionary.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace MyDictionary.Infrastructure.Repositories
{
    public class SqlUserWordRepository : IUserWordRepository
    {
        private string _connectionString { get; set; }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public SqlUserWordRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int GetUserWordId(string userId, string word)
        {
            using (IDbConnection conn = Connection)
            {
                var procedure = "GetUserWordId";
                var values = new { UserId = userId, Word = word };
                var userWordId = conn.Query<int>(procedure, values, commandType: CommandType.StoredProcedure);
                return userWordId.FirstOrDefault();
            }
        }
        public void Delete(UserWord userWord)
        {
            using (IDbConnection conn = Connection)
            {
                var procedure = "DeleteUserWord";
                var values = new { UserId = userWord.UserId, Word = userWord.Spelling };
                conn.Execute(procedure, values, commandType: CommandType.StoredProcedure);
            }

        }

        public int Create(UserWord userWord)
        {
            using (var connection = Connection)
            {
                var sql = "InsertUserWord";

                using (IDbConnection conn = Connection)
                {

                    DynamicParameters ps = new DynamicParameters();
                    ps.Add("@UserId", userWord.UserId, DbType.String, direction: ParameterDirection.Input);
                    ps.Add("@Word", userWord.Spelling, DbType.String, direction: ParameterDirection.Input);
                    ps.Add("@UserWordId", DbType.Int32, direction: ParameterDirection.Output);

                    conn.Open();
                    
                    using (var transaction = conn.BeginTransaction())
                    {

                        var result = conn.Execute(sql, ps, commandType: CommandType.StoredProcedure, transaction: transaction);
                        var userWordId = ps.Get<int>("UserWordId");

                        transaction.Commit();
                        return userWordId;
                    }

                }
            }
        }

        void IRepository<UserWord>.Update(UserWord entity)
        {
            throw new NotImplementedException();
        }

        public UserWord GetById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserWord> GetWordsByUserId(string userId)
        {
            using (IDbConnection conn = Connection)
            {
                var procedure = "GetWordsByUserId";
                var values = new { UserId = userId };
                var userWords = conn.Query<UserWord>(procedure, values, commandType: CommandType.StoredProcedure);
                return userWords;
            }
        }
    }
}

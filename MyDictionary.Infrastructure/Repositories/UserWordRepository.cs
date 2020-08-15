using Dapper;
using MyDictionary.Core.Domain;
using MyDictionary.Infrastructure.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace MyDictionary.Infrastructure.Repositories
{
    public class UserWordRepository : IUserWordRepository
    {
        private string _connectionString { get; set; }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public UserWordRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public UserWord GetUserWord(UserWord userWord)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = @"SELECT WD.Spelling, WD.Definition
                                FROM UserWords UW
                                INNER JOIN WordDetails WD ON WD.Spelling = UW.Word
                                WHERE UW.UserId = @UserId AND UW.Word = @Word";
                conn.Open();
                var result = conn.Query<UserWord>(sQuery, new { userWord.UserId, Word = userWord.Spelling });
                return result.FirstOrDefault();
            }
        }
        public void Delete(UserWord userWord)
        {

            using (IDbConnection conn = Connection)
            {
                string sQuery = @"DELETE FROM UserWord = @UserId";
                conn.Open();
                var result = conn.Execute(sQuery, new { userWord.UserId });
            }

        }


        void Update(UserWord userWord)
        {
            throw new NotImplementedException();
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

                        userWord.WordDetails.ForEach(detail =>
                        {

                            result = conn.Execute("InsertUserWordDetails",
                                new
                                {
                                    userWordId,
                                    detail.Definition,
                                    detail.PartOfSpeech,
                                    Synonyms = String.Join(", ", detail.Synonyms),
                                    detail.SimilarTo,
                                    detail.Derivation,
                                    detail.Examples
                                },
                                commandType: CommandType.StoredProcedure,
                                transaction: transaction);
                            
                        });

                        transaction.Commit();
                    }



                    return 0; // userWordId;

                    //DynamicParameters _params = new DynamicParameters();
                    //_params.Add("@newId", DbType.Int32, direction: ParameterDirection.Output);
                    //var result = connection.Execute("[dbo].YourProc", _params, null, null, CommandType.StoredProcedure);
                    //var retVal = _params.Get<int>("newId");
                }
            }
        }



        void IRepository<UserWord>.Create(UserWord entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<UserWord>.Update(UserWord entity)
        {
            throw new NotImplementedException();
        }

        public UserWord GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}

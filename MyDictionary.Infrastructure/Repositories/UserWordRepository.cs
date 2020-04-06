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
                var result = conn.Query<UserWord>(sQuery, new { UserId = userWord.UserId, Word = userWord.Spelling });
                return result.FirstOrDefault();
            }
        }
        public void Delete(UserWord userWord)
        {

            using (IDbConnection conn = Connection)
            {
                string sQuery = @"DELETE FROM UserWord = @UserId";
                conn.Open();
                var result = conn.Execute(sQuery, new { UserId = userWord.UserId });
            }

        }


        void Update(UserWord userWord)
        {
            throw new NotImplementedException();
        }

        void Create(UserWord userWord)
        {
            using (var connection = Connection)
            {
                var sql = "InsertUserWord";

                using (IDbConnection conn = Connection)
                {
                    connection.Open();

                    var affectedRows = connection.Execute(sql,
                        new 
                        { 
                            UserId = userWord.UserId, 
                            Word = userWord.Spelling,
                            Definition = userWord.WordDetails[0].Definition,
                            PartOfSpeech = "",
                            Synonyms = "",
                            SimilarTo = "",
                            Derivation = "",
                            Examples = ""
                        },
                        commandType: CommandType.StoredProcedure);
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

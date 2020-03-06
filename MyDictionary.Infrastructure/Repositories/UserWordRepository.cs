using MyDictionary.Core.Domain;
using MyDictionary.Infrastructure.Interfaces;
using System.Data;
using System.Threading.Tasks;

namespace MyDictionary.Infrastructure.Repositories
{
    public class UserWordRepository : IUserWordRepository
    {
        private string _connectionString { get; set; }
        public UserWordRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public Task<UserWord> GetUserWord(string word)
        {
            using(IDbConnection connection = 
                    new System.Data.SqlClient.SqlConnection()
            throw new System.NotImplementedException();
        }
    }
}

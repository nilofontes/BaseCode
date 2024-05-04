using Microsoft.Data.SqlClient;
using System.Data;

namespace BaseCode.Factory
{
    public class SqlFactory
    {
        public IDbConnection GetSqlConnection() 
        {
            return new SqlConnection("Server=localhost;DataBase=BaseCode; User=sa;Password=Senhaforte@89; Trusted_Connection=False;TrustServerCertificate=True");
        }
    }
}

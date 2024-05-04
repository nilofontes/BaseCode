using BaseCode.Factory;
using BaseCode.Interface;
using BaseCode.Models;
using System.Data;
using Dapper;
namespace BaseCode.Repository
{
    public class LoginRequestRepository : ILoginRequest
    {
        private readonly IDbConnection _connection;
        public LoginRequestRepository()
        {
            _connection = new SqlFactory().GetSqlConnection();
        }
        public LoginRequest GetUserByPassword(string username, string password)
        {
            var query = $"Select * from UserLogin where Username = '{username}' and PassWordHash = '{password}'";
            using (_connection) 
            {             
                return _connection.QueryFirstOrDefault<LoginRequest>(query);             
            }
        }

        public LoginRequest GetUserbyRefreshToken(string refreshToken)
        {
            var query = $"select * from UserLogin where Id in (select UserId from UserToken where RefreshToken = '{refreshToken}')";
            using (_connection)
            {
                return _connection.QueryFirstOrDefault<LoginRequest>(query);
            }
        }

        public void UpdateRefreshToken(LoginRequest loginrequest,string refreshToken)
        {
            var sql = @"update UserToken set RefreshToken = @RefreshToken where UserID = @UserID";

            var parameters = new
            {
                RefreshToken = refreshToken,
                UserID = loginrequest.UserId
            };

            using (_connection)
            {
               _connection.Execute(sql, parameters);
            }
        }
    }
}

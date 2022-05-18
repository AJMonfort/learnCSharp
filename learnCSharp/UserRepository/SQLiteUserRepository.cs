using learnCSharp.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnCSharp.UserRepository
{
    internal class SQLiteUserRepository : IUserRepository
    {
        private SQLiteDatabaseHelper _dbHelper;
        public SQLiteUserRepository()
        {
            _dbHelper = new SQLiteDatabaseHelper();
        }
        public async Task Initialize()
        {
            if (File.Exists(_dbHelper.DatabaseName))
            {
                return;
            }

            await _dbHelper.CreateDatabase();

        }

        public async Task DeleteUser(int ID)
        {
            using var connection = new SqliteConnection(_dbHelper.ConnectionString);
            await connection.ExecuteAsync(@"DELETE FROM User WHERE ID = @ID", new {ID});
        }

        public async Task<User> GetUser(int ID)
        {
            var connection = new SqliteConnection(_dbHelper.ConnectionString);
            var user = await connection.QuerySingleAsync<User>(@"SELECT * FROM User WHERE ID = @ID", new { ID });
            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var connection = new SqliteConnection(_dbHelper.ConnectionString);
            var userList = await connection.QueryAsync<User>(@"SELECT * FROM User");
            return userList.ToList();
        }

        public async Task<User> GetUserByName(String name)
        {
            var connection = new SqliteConnection(_dbHelper.ConnectionString);
            var users = await connection.QueryAsync<User>(@"SELECT * FROM User WHERE Name = @Name", new {Name = name});
            var user = users.FirstOrDefault();
            return user;
        }

        public async Task<User> NewUser(User user)
        {
            var connection = new SqliteConnection(_dbHelper.ConnectionString);
            var insertedID = await connection.QuerySingleAsync<int>(
@"INSERT INTO User(Name, Password)
VALUES (@Name, @Password);
SELECT last_insert_rowid() as ID;",
                new {Name = user.Name, Password = user.Password});
            user.ID = insertedID;
            return user;
        }

        public async Task UpdateUser(User user)
        {
            var connection = new SqliteConnection(_dbHelper.ConnectionString);
            await connection.ExecuteAsync(
@"UPDATE Todo
SET Name = @Name, Password = @Password
WHERE ID = @ID",
                new {Name = user.Name, Password = user.Password, ID = user.ID});
        }
    }
}

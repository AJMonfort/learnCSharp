using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnCSharp
{
    internal class SQLiteDatabaseHelper
    {
        public String DatabaseName { get; } = "tododatabase.db";
        public String ConnectionString { get; } = "Data Source = tododatabase.db";
        public async Task CreateDatabase()
        {
            using var connection = new SqliteConnection(ConnectionString);

            await connection.ExecuteAsync(
@"CREATE TABLE[User](
    ID INTEGER PRIMARY KEY NOT NULL,
    Name TEXT NOT NULL,
    Password TEXT NOT NULL
);

CREATE TABLE[Todo](
    ID INTEGER PRIMARY KEY NOT NULL,
    UserID INTEGER NOT NULL,
    Description TEXT NOT NULL,
    Completed INTEGER NOT NULL,
    FOREIGN KEY (UserID)
		REFERENCES [User]
			ON UPDATE NO ACTION
			ON DELETE CASCADE
);");
        }

    }
}

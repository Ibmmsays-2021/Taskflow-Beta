using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infrastracture.Persistence.Dapper;

public class SqlConnectionFactory(string connectionString)
{
    public IDbConnection CreateConnection() => new SqlConnection(connectionString);
}

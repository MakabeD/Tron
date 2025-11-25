using Oracle.ManagedDataAccess.Client;
using System.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}

public class OracleConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;
    public OracleConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    public IDbConnection CreateConnection()
    {
        return new OracleConnection(_connectionString);
    }
}


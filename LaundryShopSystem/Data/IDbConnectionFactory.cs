using System.Data;

namespace LaundryShopSystem.Data
{
    // Interface for dependency injection
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
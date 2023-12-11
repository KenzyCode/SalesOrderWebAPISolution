namespace SalesOrderWebAPI.IRepository

{
    public interface IRefreshRepository
    {
        Task<string> GenerateToken(string username);
    }
}

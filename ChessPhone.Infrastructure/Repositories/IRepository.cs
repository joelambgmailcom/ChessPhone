namespace ChessPhone.Infrastructure.Repositories
{
    public  interface IRepository<T>
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetAsync(int id);
    }
}

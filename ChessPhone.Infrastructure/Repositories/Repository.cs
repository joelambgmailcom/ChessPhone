using ChessPhone.Domain;
using System.Text.Json;

namespace ChessPhone.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : IRepositoryEntity
    {
        public async Task<T> GetAsync(int id)
        {
            var data = await FetchDataAsync();
            return data.FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await FetchDataAsync();
        }

        private static async Task<List<T>> FetchDataAsync()
        {
            var json = await GetJSonFromFileAsync($"{typeof(T).Name}.json");
            return JsonSerializer.Deserialize<List<T>>(json) ?? [];
        }

        private static async Task<string> GetJSonFromFileAsync(string fileLocation)
        {
            using StreamReader reader = new(fileLocation);
            return await reader.ReadToEndAsync();
        }
    }
}

using RestApi.Models;

namespace RestApi.Interfaces
{
    public interface ICategoryRepo
    {
        Task<List<Category>> GetAllAsync();

        Task<Category?> GetByIdAsync(int id);

    }
}
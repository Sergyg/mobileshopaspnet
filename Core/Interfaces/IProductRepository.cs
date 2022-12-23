using Core.Entities;

namespace Core.Interfaces;

public interface IProducteRpository
{
    Task<Product> GetProductByIdAsync(int id);
    Task<IReadOnlyList<Product>> GetProductAsync();
    
}
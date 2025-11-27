namespace RestApi.Interfaces
{
    public interface IProductRepo
    {
        Task<bool> ProductExists(int productId);
    }
}
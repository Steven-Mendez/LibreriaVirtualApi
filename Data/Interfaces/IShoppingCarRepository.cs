using LibreriaVirtualApi.Models;

namespace LibreriaVirtualApi.Data.Interfaces
{
    public interface IShoppingCarRepository
    {
        Task<IEnumerable<ShoppingCar>> GetByIdAsync(int userId);
        Task<ShoppingCar> AddEntry(ShoppingCar shoppingCar);
        void UpdateEntry(ShoppingCar shoppingCar);
        void DeleteEntry(ShoppingCar shoppingCar);
        void ClearCar(User id);
        void BuySingleBook(ShoppingCar shoppingCarEntry, int quantity);
        Task BuyAllCar(int userId);
        Task<bool> SaveAll();
    }
}

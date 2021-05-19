using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Models
{
    public interface ICartRepository
    {
        Task<Cart> GetCartAsync(string cartId);
        Task<Cart> UpdatecartAsync(Cart basket);

        Task<bool> DeleteCartAsync(string CartId);

        IEnumerable<string> GetUsers();
    }
}

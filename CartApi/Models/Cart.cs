using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Cart
    {
        public string BuyerId { get; set; }
        public List<CartItem> Items { get; set; }

        public Cart()
        { }
        public Cart(string cartId)  //buyer id or user id is cart id
        {
            BuyerId = cartId;
            Items = new List<CartItem>();
        }
    }
}

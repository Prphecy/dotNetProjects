using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookWeb.Models;

namespace BookWeb.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart> 
    {
        void Remove(ShoppingCart cartFromDb);
		void Update(ShoppingCart obj);
    }
}

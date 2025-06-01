using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductOK_App
{
    public class ProductInCart
    {
        List<InCart> _productInCart = new List<InCart>();
        private readonly ProductRepository _productRepository;
        public ProductInCart()
        {
            _productRepository = new ProductRepository();
        }
        public void ClearCart()
        {
            _productInCart.Clear();
        }
        public void AddProduct(string productName)
        {
            InCart existingItem = null;
            foreach (var cartItem in _productInCart)
            {
                if (cartItem._product.Equals(productName, StringComparison.OrdinalIgnoreCase))
                {
                    existingItem = cartItem;
                    break;
                }
            }
            if (existingItem != null)
            {
                existingItem._count += 1;
            }
            else
            {
                var allProducts = _productRepository.GetAllProducts();
                bool productExistsInDatabase = false;

                foreach (var product in allProducts)
                {
                    if (product.ProductName.Equals(productName, StringComparison.OrdinalIgnoreCase))
                    {
                        _productInCart.Add(new InCart(product.ProductName, product.Price, 1));
                        productExistsInDatabase = true;
                        break;
                    }
                }
                if (!productExistsInDatabase)
                {
                    MessageBox.Show("Товар не найден!");
                }
            }
        }

        public void RemoveProduct(string productName)
        {
            var allProducts = _productRepository.GetAllProducts();
            for (int i = 0; i < _productInCart.Count; i++)
            {
                if (_productInCart[i]._product.Equals(productName, StringComparison.OrdinalIgnoreCase))
                {
                    _productInCart.RemoveAt(i);
                    break;
                }
            }
        }

        public List<InCart> GetProductsInCart()
        {
            return _productInCart;
        }
    }
}

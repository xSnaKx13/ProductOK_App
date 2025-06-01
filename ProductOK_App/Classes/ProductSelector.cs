using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductOK_App
{
    public class ProductSelector
    {
        private readonly ProductRepository _productRepository;
        private readonly DataGridView _dataGridView;

        public ProductSelector(DataGridView dataGridView)
        {
            _productRepository = new ProductRepository();
            _dataGridView = dataGridView;
        }

        public void ShowProductsByCategory(string category)
        {
            var allProducts = _productRepository.GetAllProducts();
            var filteredProducts = allProducts.Where(p => p.Category == category).ToList();

            if (_dataGridView.InvokeRequired)
            {
                _dataGridView.Invoke(new Action(() => _dataGridView.DataSource = filteredProducts));
            }
            else
            {
                _dataGridView.DataSource = filteredProducts;
            }
        }
    }
}

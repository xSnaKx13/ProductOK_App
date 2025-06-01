using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductOK_App
{
    public class DataGridViewManager
    {
        public void LoadProductsToGrid(DataGridView dataGridView, List<Product> products)
        {
            dataGridView.DataSource = products;
        }
    }
}

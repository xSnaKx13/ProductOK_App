using ProductOK_App.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductOK_App
{
    public partial class Form1 : Form
    {
        private readonly ProductRepository _productRepository;
        private readonly ProductSelector _productSelector;
        private readonly DataGridViewManager _GridManager;
        private readonly ProductInCart _productInCart;
        private readonly TabPageHide _tabPageHide;
        private readonly AdminControlPanel _adminControlPanel;
        private readonly Wallet _wallet;
        private List<Product> _allProducts;
        public Form1()
        {
            InitializeComponent();
            _productRepository = new ProductRepository();
            _GridManager = new DataGridViewManager();
            _productSelector = new ProductSelector(dataGridView2);
            _productInCart = new ProductInCart();
            _tabPageHide = new TabPageHide(adminEnter);
            _adminControlPanel = new AdminControlPanel();
            _wallet = new Wallet();
            button7.Visible = false;
            label3.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            label4.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            textBox6.Visible = false;
            button8.Visible = false;
            button11.Visible = false;
            LoadProducts();
            _tabPageHide.HideTabControl();
            this.AcceptButton = button3;
        }
        private void LoadProducts()
        {
            _allProducts = _productRepository.GetAllProducts().ToList();
        }

        private void PrintProductButton_Click(object sender, EventArgs e)
        {

            adminEnter.SelectedIndex = 1;
        }


        private void Fruit_Click(object sender, EventArgs e)
        {
            _productSelector.ShowProductsByCategory("Фрукты");
            adminEnter.SelectedIndex = 2;
        }
        private void Vegetables_Click(object sender, EventArgs e)
        {
            _productSelector.ShowProductsByCategory("Овощи");
            adminEnter.SelectedIndex = 2;
        }
        private void Sweets_Click(object sender, EventArgs e)
        {
            _productSelector.ShowProductsByCategory("Сладости");
            adminEnter.SelectedIndex = 2;
        }
        private void DairyProducts_Click(object sender, EventArgs e)
        {
            _productSelector.ShowProductsByCategory("Молочные продукты");
            adminEnter.SelectedIndex = 2;
        }
        private void HouseholdGoods_Click(object sender, EventArgs e)
        {
            _productSelector.ShowProductsByCategory("Бытовые товары");
            adminEnter.SelectedIndex = 2;
        }
        private void meatProducts_Click(object sender, EventArgs e)
        {
            _productSelector.ShowProductsByCategory("Мясные продукты");
            adminEnter.SelectedIndex = 2;
        }
        private void drinks_Click(object sender, EventArgs e)
        {
            _productSelector.ShowProductsByCategory("Напитки");
            adminEnter.SelectedIndex = 2;
        }
        private void choiceProduct_Click(object sender, EventArgs e)
        {
            string addProductName = textBox1.Text.Trim();
            var productInCart = _productInCart.GetProductsInCart();
            _productInCart.AddProduct(addProductName);
            dataGridView3.DataSource = productInCart;
            textBox1.Clear();
            label3.Visible = true;

        }

        private void removeInCartButton_Click(object sender, EventArgs e)
        {
            string removeProductName = textBox2.Text.Trim();
            var productInCart = _productInCart.GetProductsInCart();
            _productInCart.RemoveProduct(removeProductName);
            dataGridView3.DataSource = productInCart;
            label4.Visible = true;
        }

        private void backToMenu_Click(object sender, EventArgs e)
        {
            adminEnter.SelectedIndex = 1;
            label3.Visible = false;
        }

        private void backToMenu2_Click(object sender, EventArgs e)
        {
            adminEnter.SelectedIndex = 1;
            label3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView3.DataSource = null;
            var productInCart = _productInCart.GetProductsInCart();
            dataGridView3.DataSource = productInCart;
            adminEnter.SelectedIndex = 4;
            label4.Visible = false;
            label3.Visible = false;
            decimal totalSum = 0;
            var products = _productInCart.GetProductsInCart();
            foreach (var product in products)
            {
                totalSum += product.TotalPrice;
            }
            label6.Text = $"Текущая сумма: {totalSum}";
            label5.Text = $"Баланс: {_wallet._balance}";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView3.DataSource = null;
            var productInCart = _productInCart.GetProductsInCart();
            dataGridView3.DataSource = productInCart;
            adminEnter.SelectedIndex = 4;
            label4.Visible = false;
            decimal totalSum = 0;
            var products = _productInCart.GetProductsInCart();
            foreach (var product in products)
            {
                totalSum += product.TotalPrice;
            }
            label6.Text = $"Текущая сумма: {totalSum}";
            label5.Text = $"Баланс: {_wallet._balance}";
        }
        
        public void UpdateBalance()
        {
            label5.Text = $"Баланс: {_wallet._balance}";
        }

        private void payButton_Click(object sender, EventArgs e)
        {
            decimal _totalCoast = 0;
            var products = _productInCart.GetProductsInCart();
            foreach ( var product in products )
            {
                _totalCoast += product.TotalPrice;
            }

            if(_wallet._balance >= _totalCoast)
            {
                MessageBox.Show($"Покупка совершена успешно. Списано {_totalCoast}");
                _wallet._balance -= _totalCoast;
                UpdateBalance();
            }
            _totalCoast = 0;
            _productInCart.ClearCart();
            dataGridView3.DataSource = null;
            dataGridView3.DataSource = _productInCart;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            adminEnter.SelectedIndex = 5;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _adminControlPanel.UserEnter(textLogin, textPassword, adminEnter);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            adminEnter.SelectedIndex = 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            adminEnter.SelectedIndex = 0;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            AdminControlPanel adminControlPanel = new AdminControlPanel();
            adminControlPanel.PrintAllProduct(dataGridView4);
        }

        private void addProductButton_Click(object sender, EventArgs e)
        {
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            label9.Visible = true;
            addProductButton.Visible = false;
            removeProductButton.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            button7.Visible = true;
            button11.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            _adminControlPanel.AddProductinDB(textBox3, textBox4, textBox5);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            adminEnter.SelectedIndex = 0;
        }

        private void removeProductButton_Click(object sender, EventArgs e)
        {

            addProductButton.Visible = false;
            removeProductButton.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            label10.Visible = true;
            textBox6.Visible = true;
            button8.Visible = true;
            button11.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
            // Получаем ID товара для удаления (например, из TextBox или выделенной строки DataGridView)
            if (!int.TryParse(textBox6.Text, out int productId))
            {
                MessageBox.Show("Введите корректный ID товара!");
                return;
            }

            _adminControlPanel.DeleteProductById(productId);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            label4.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            textBox6.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
            button11.Visible = false;
            addProductButton.Visible = true;
            removeProductButton.Visible = true;
            button9.Visible = true;
            button10.Visible = true;
        }
    }
}

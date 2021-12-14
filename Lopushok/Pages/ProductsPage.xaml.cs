using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lopushok.Models;
using System.Data.Entity;

namespace Lopushok.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        private List<Product> products;

        public ProductsPage()
        {
            InitializeComponent();

            products = DB.db.Product.ToList();

            btnEdit.Visibility = Visibility.Hidden;

            lbProducts.ItemsSource = products;
            lbProducts.SelectedValuePath = "ID";
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            FindProduct();
        }

        private void FindProduct()
        {
            if (tbSearch.Text != "")
            {

                products = DB.db.Product.Where(p => (p.Title.ToString() + p.ProductType.Title.ToString()).ToLower().Contains(tbSearch.Text.ToLower())).ToList();


                lbProducts.ItemsSource = products;
            }
            else
            {
                products = DB.db.Product.ToList();

                lbProducts.ItemsSource = products;
            }
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void radioButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProductPage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditProductPage(lbProducts.SelectedItem as Product));
        }

        private void lbProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lbProducts.SelectedItems.Count > 0)
            {
                btnEdit.Visibility = Visibility.Visible;
            }
        }
    }
}

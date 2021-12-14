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

            List<ProductType> filter = DB.db.ProductType.ToList();
            filter.Insert(0, new ProductType { Title = "Все типы" });

            cbFilter.ItemsSource = filter;
            cbFilter.SelectedValuePath = "ID";
            cbFilter.DisplayMemberPath = "Title";
            cbFilter.SelectedIndex = 0;

            List<string> sort = new List<string>();
            sort.Add("Сортировака");
            sort.Add("По названию");
            sort.Add("По типу продукции");
            sort.Add("По мин. стоимости");

            cbSort.ItemsSource = sort;
            cbSort.SelectedIndex = 0;


            rbAsc.IsEnabled = false;
            rbDesc.IsEnabled = false;
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
            }
            else
            {
                products = DB.db.Product.ToList();
            }

            if(cbFilter.SelectedIndex != 0)
            {
                products = products.Where(p => p.ProductType == cbFilter.SelectedItem).ToList();
            }

            if(cbSort.SelectedIndex != 0)
            {
                rbAsc.IsEnabled = true;
                rbDesc.IsEnabled = true;

                switch (cbSort.SelectedIndex)
                {
                    case 1:
                        if (rbAsc.IsChecked == true)
                            products = products.OrderBy(p => p.Title).ToList();
                        else products = products.OrderByDescending(p => p.Title).ToList();
                        break;
                    case 2:
                        if (rbAsc.IsChecked == true)
                            products = products.OrderBy(p => p.ProductType.Title).ToList();
                        else products = products.OrderByDescending(p => p.ProductType.Title).ToList();
                        break;
                    case 3:
                        if (rbAsc.IsChecked == true)
                            products = products.OrderBy(p => p.MinCostForAgent).ToList();
                        else products = products.OrderByDescending(p => p.MinCostForAgent).ToList();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                rbAsc.IsEnabled = false;
                rbDesc.IsEnabled = false;
            }

            lbProducts.ItemsSource = products;
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FindProduct();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FindProduct();
        }

        private void radioButton_Click(object sender, RoutedEventArgs e)
        {
            FindProduct();
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

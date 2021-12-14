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

namespace Lopushok.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditProductPage.xaml
    /// </summary>
    public partial class EditProductPage : Page
    {
        private Product product;
        public EditProductPage(Product product)
        {
            InitializeComponent();

            this.product = product;

            cbType.ItemsSource = DB.db.ProductType.ToList();
            cbType.DisplayMemberPath = ("Title");
            cbType.SelectedValuePath = ("ID");

            LoadData();
        }

        private void LoadData()
        {
            tbArticle.Text = product.ArticleNumber;
            tbDescription.Text = product.Description;
            tbMinCost.Text = product.MinCostForAgent.ToString();
            tbPersonCount.Text = product.ProductionPersonCount.ToString();
            tbTitle.Text = product.Title;
            tbWorckshopNum.Text = product.ProductionWorkshopNumber.ToString();
            cbType.SelectedValue = product.ProductTypeID;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить этот продукт?", "Внимание", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DB.db.Product.Remove(product);
                NavigationService.Navigate(new ProductsPage());
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(tbTitle.Text))
            {
                if (!String.IsNullOrEmpty(cbType.Text))
                {
                    if (!String.IsNullOrEmpty(tbArticle.Text))
                    {
                        product.Title = tbTitle.Text;

                        if (int.TryParse(tbMinCost.Text, out int i))
                        {
                            product.MinCostForAgent = i;
                        }

                        if (int.TryParse(tbWorckshopNum.Text, out i))
                        {
                            product.ProductionWorkshopNumber = i;
                        }

                        if (int.TryParse(cbType.SelectedValue.ToString(), out i))
                        {
                            product.ProductTypeID = i;
                        }

                        if (int.TryParse(tbPersonCount.Text, out i))
                            product.ProductionPersonCount = i;

                        product.ArticleNumber = tbArticle.Text;

                        product.Description = tbDescription.Text;
                        DB.db.SaveChanges();
                        NavigationService.Navigate(new ProductsPage());
                    }
                    else MessageBox.Show("Введите артикуль продукта");
                }
                else MessageBox.Show("Выберите тип продукта");
            }
            else MessageBox.Show("Введите название продукта");
        }
    }
}

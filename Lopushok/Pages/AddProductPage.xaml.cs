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
using Lopushok.Models;
using System.Windows.Shapes;

namespace Lopushok.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddProductPage.xaml
    /// </summary>
    public partial class AddProductPage : Page
    {
        private Product newProduct = new Product();

        public AddProductPage()
        {
            InitializeComponent();

            cbType.ItemsSource = DB.db.ProductType.ToList();
            cbType.DisplayMemberPath = ("Title");
            cbType.SelectedValuePath = ("ID");
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(tbTitle.Text))
            {
                if (!String.IsNullOrEmpty(cbType.Text))
                {
                    if (!String.IsNullOrEmpty(tbArticle.Text))
                    {
                        newProduct.Title = tbTitle.Text;

                        if (int.TryParse(tbMinCost.Text, out int i))
                        {
                            newProduct.MinCostForAgent = i;
                        }
                        else
                            MessageBox.Show("Введите минимальное количество для агентов");

                        if (int.TryParse(tbWorckshopNum.Text, out i))
                        {
                            newProduct.ProductionWorkshopNumber = i;
                        }
                        else
                            MessageBox.Show("Введите номер цеха");

                        if (int.TryParse(cbType.SelectedValue.ToString(), out i))
                        {
                            newProduct.ProductTypeID = i;
                        }

                        if (int.TryParse(tbPersonCount.Text, out i))
                            newProduct.ProductionPersonCount = i;

                        newProduct.ArticleNumber = tbArticle.Text;

                        newProduct.Description = tbDescription.Text;

                        DB.db.Product.Add(newProduct);
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

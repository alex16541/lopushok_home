using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lopushok.Models
{
    partial class Product
    {
        public string ValidImage => (String.IsNullOrWhiteSpace(Image) || String.IsNullOrEmpty(Image)) ? "/Resources/products/picture.png" : Image.ToString();

        public string ValidMaterials
        {
            get
            {
                string result = "";

                List<Material> materials = Material.ToList();


                if(materials.Count > 0 && materials != null)
                {
                    for (int i = 0; i < materials.Count; i++)
                    {
                        if (i == materials.Count - 1)
                        {
                            result += materials[i].Title + ".";
                        }
                        else
                        {
                            result += materials[i].Title + ", ";
                        }
                    }
                }
                else
                {
                    return "Отсутствуют";
                }
                return result;
            }
        }
    }
}

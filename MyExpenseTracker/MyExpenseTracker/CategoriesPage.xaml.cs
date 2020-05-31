using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyExpenseTracker.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenseTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]


    public partial class CategoriesPage : ContentPage
    {
        public ObservableCollection<Categories> categories_oc { get; set; }
        public CategoriesPage()
        {
            InitializeComponent();
            categories_oc = new ObservableCollection<Categories>();
            CategoryManager.GetAllCategories(categories_oc);
            listView.ItemsSource = categories_oc;
        }  
    }
}
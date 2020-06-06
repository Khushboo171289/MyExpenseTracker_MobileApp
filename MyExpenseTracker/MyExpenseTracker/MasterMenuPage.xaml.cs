using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenseTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterMenuPage : ContentPage
    {
        public MasterMenuPage()
        {
            InitializeComponent();

            this.BindingContext = new
            {
                Header = "My Expense Tracker",
                Image = "https://g.foolcdn.com/editorial/images/457855/jar-full-of-hundred-dollar-bills-money-savings.jpg",

            };
        }

        private void MainMenuListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}
    
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MyExpenseTracker.Model
{
    public class CategoryManager
    {
        
        public static void GetAllCategories(ObservableCollection<Categories> categories_oc)
        {
            var categories = new List<Categories>();
            categories.Add(new Categories("Auto", 0, 0, 0,"Auto.png"));
            categories.Add(new Categories("Education", 0, 0, 0,"Education.png"));
            categories.Add(new Categories("Entertainment",0, 0, 0,"Entertainment.png"));
            categories.Add(new Categories("Food", 0, 0, 0,"Food.png"));
            categories.Add(new Categories("Health", 0, 0, 0,"Health.png"));
            categories.Add(new Categories("Home", 0, 0, 0,"Home.png"));
            categories_oc.Clear();
            categories.ForEach(item => categories_oc.Add(new Categories(item.Name, item.Spent, item.Balance, item.Budget, item.IconSource)));

        }


    }

}

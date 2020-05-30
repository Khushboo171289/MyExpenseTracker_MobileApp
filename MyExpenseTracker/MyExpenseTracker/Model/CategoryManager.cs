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
            categories.Add(new Categories("auto", 0, 0, 0,"auto.png"));
            categories.Add(new Categories("education", 0, 0, 0,"education.png"));
            categories.Add(new Categories("entertainment",0, 0, 0,"entertainment.png"));
            categories.Add(new Categories("food", 0, 0, 0,"food.png"));
            categories.Add(new Categories("health", 0, 0, 0,"health.png"));
            categories.Add(new Categories("home", 0, 0, 0,"home.png"));
            categories_oc.Clear();
            categories.ForEach(item => categories_oc.Add(new Categories(item.Name, item.Spent, item.Balance, item.Budget, item.IconSource)));

        }


    }

}

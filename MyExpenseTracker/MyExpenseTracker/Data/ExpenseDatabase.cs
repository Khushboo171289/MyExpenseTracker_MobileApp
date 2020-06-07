using MyExpenseTracker.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenseTracker.Data
{
    public class ExpenseDatabase
    {

        readonly SQLiteAsyncConnection _database;

        public ExpenseDatabase(string dbPath)
        {
            //Creating sqlite Db Connection
            _database = new SQLiteAsyncConnection(dbPath);

            // Creating Tables for Budget, Categories and Expenses
            _database.CreateTableAsync<Expense>().Wait();
            _database.CreateTableAsync<Categories>().Wait();
            _database.CreateTableAsync<Budget>().Wait();
        }

        //To get all Expenses
        public Task<List<Expense>> GetExpensesAsync()
        {
            return _database.Table<Expense>().ToListAsync();

        }

        //To Get all Categories
        public Task<List<Categories>> GetCategoriesAsync()
        {
            return _database.Table<Categories>().ToListAsync();

        }


        //Retrive category by name
        public Categories GetSpecificCategory(string name) //retrieves single element
        {
            return _database.FindWithQueryAsync<Categories>("Select * from Categories where Name=?", name).Result;
        }

        //Update categories Budget amount
        public Task<int> UpdateCategoriesBudgetAmount(string name, double spent)
        {
            return _database.ExecuteAsync("Update Categories Set Budget = ? where Name = ?", spent, name);
        }

        //retrive all budgets
        public Task<List<Budget>> GetBudgetsAsync()
        {
            return _database.Table<Budget>().ToListAsync();

        }

        //Retrive Expense by ID
        public Task<Expense> GetExpenseAsync(int id)
        {
            return _database.Table<Expense>()
                            .Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        //Retrive all expenses in a category
        public Task<List<Expense>> GetExpenseByCategory(string category)
        {
            return _database.Table<Expense>()
                    .Where(i => i.Category == category)
                    .ToListAsync();
        }

        //Retrive category by name
        public Task<List<Categories>> GetCategoryByName(string cname)
        {
            return _database.Table<Categories>()
                    .Where(i => i.Name == cname)
                    .ToListAsync();
        }

        //Retrive category by Month
        public Task<List<Categories>> GetCategoryByMonth(string month)
        {
            return _database.Table<Categories>()
                    .Where(i => i.Month == month)
                    .ToListAsync();
        }

        //Retrive Budget by name
        public Task<List<Budget>> GetBudgetByMonth(string month)
        {
            return _database.Table<Budget>()
                    .Where(i => i.Month == month)
                    .ToListAsync();
        }

        //Save or Update Expense
        public Task<int> SaveExpenseAsync(Expense expense)
        {
            if (expense.ID != 0)
            {
                return _database.UpdateAsync(expense);
            }
            else
            {
                return _database.InsertAsync(expense);
            }
        }

        //Save or Update Category
        public Task<int> SaveCategoryAsync(Categories categories)
        {
            if (categories.ID != 0)
            {
                return _database.UpdateAsync(categories);
            }
            else
            {
                return _database.InsertAsync(categories);
            }
        }

        //Save or Update Budget
        public Task<int> SaveBudgetAsync(Budget budget)
        {
            if (budget.ID != 0)
            {
                return _database.UpdateAsync(budget);
            }
            else
            {
                return _database.InsertAsync(budget);
            }
        }


        //Delete Expense
        public Task<int> DeleteExpenseAsync(Expense expense)
        {
            return _database.DeleteAsync(expense);
        }

        //Delete Category
        public Task<int> DeleteCategoryAsync(Categories  categories)
        {
            return _database.DeleteAsync(categories);
        }

        //Delete Budget
        public Task<int> DeleteBudgetAsync(Budget budget)
        {
            return _database.DeleteAsync(budget);
        }

        //Retrives Sum of all expense spent amount
        public double SumExpenseAsync()
        {
           return _database.ExecuteScalarAsync<double>("select Sum(Spent) FROM Expense").Result; 
        }

        //Retrives Sum of all expense spent amount by categories
        public double SumOfExpenseByCategoriesAsync(string cname)
        {
            return _database.ExecuteScalarAsync<double>("select Sum(Spent) FROM Expense Where Category = ?", cname).Result;
        }

        //Retrives Sum of all expense for month budget
        public double SumExpenseByBudgetAsync(string cname)
        {
            return _database.ExecuteScalarAsync<double>("select Sum(Spent) FROM Categories Where Name = ?", cname).Result;
        }

        //Update Budget Spent and balance amount
        public Task<int> UpdateBudgetSpentAndBalance(string month, double spent, double balance)
        {
            return _database.ExecuteAsync("Update Budget Set Expense = ? , Balance = ? where Month = ?", spent, balance, month);
        }

        //Update Categories Spent and balance amount
        public Task<int> UpdateCategoriesSpentAndBalance(string name, double spent, double balance)
        {
            return _database.ExecuteAsync("Update Categories Set Spent = ? , Balance = ? where Name = ?", spent, balance, name);
        }

        //Update Budget amount
        public Task<int> UpdateBudgetAmount(string month, double spent)
        {
            return _database.ExecuteAsync("Update Budget Set BudgetAmount = ?where Month = ?", spent, month);
        }

        //Update Categories Budget amount
        public Task<int> UpdateCategoriesAmount(string name, double spent)
        {
            return _database.ExecuteAsync("Update Categories Set Budget = ? , Balance = ? where Name = ?", spent, name);
        }

    }
}

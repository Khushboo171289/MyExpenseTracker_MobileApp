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
            _database = new SQLiteAsyncConnection(dbPath);

            _database.CreateTableAsync<Expense>().Wait();
            _database.CreateTableAsync<Categories>().Wait();
            _database.CreateTableAsync<Budget>().Wait();
        }
        public Task<List<Expense>> GetExpensesAsync()
        {
            return _database.Table<Expense>().ToListAsync();

        }

        public Task<List<Categories>> GetCategoriesAsync()
        {
            return _database.Table<Categories>().ToListAsync();

        }

        public Task<List<Budget>> GetBudgetsAsync()
        {
            return _database.Table<Budget>().ToListAsync();

        }

        public Task<Expense> GetExpenseAsync(int id)
        {
            return _database.Table<Expense>()
                            .Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<List<Expense>> GetExpenseByCategory(string category)
        {
            return _database.Table<Expense>()
                    .Where(i => i.Category == category)
                    .ToListAsync();
        }

        public Task<List<Categories>> GetCategoryByName(string cname)
        {
            return _database.Table<Categories>()
                    .Where(i => i.Name == cname)
                    .ToListAsync();
        }

        public Task<List<Categories>> GetCategoryByMonth(string month)
        {
            return _database.Table<Categories>()
                    .Where(i => i.Month == month)
                    .ToListAsync();
        }

        public Task<List<Budget>> GetBudgetByMonth(string month)
        {
            return _database.Table<Budget>()
                    .Where(i => i.Month == month)
                    .ToListAsync();
        }

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



        public Task<int> DeleteExpenseAsync(Expense expense)
        {
            return _database.DeleteAsync(expense);
        }

        public Task<int> DeleteCategoryAsync(Categories  categories)
        {
            return _database.DeleteAsync(categories);
        }

        public Task<int> DeleteBudgetAsync(Budget budget)
        {
            return _database.DeleteAsync(budget);
        }

        public double SumExpenseAsync()
        {
           return _database.ExecuteScalarAsync<double>("select Sum(Spent) FROM Expense").Result; 
        }

        public double SumOfExpenseByCategoriesAsync(string cname)
        {
            return _database.ExecuteScalarAsync<double>("select Sum(Spent) FROM Expense Where Category = ?", cname).Result;
        }

        public double SumExpenseByBudgetAsync(string cname)
        {
            return _database.ExecuteScalarAsync<double>("select Sum(Spent) FROM Categories Where Name = ?", cname).Result;
        }

        public Task<int> UpdateBudgetSpentAndBalance(string month, double spent, double balance)
        {
            return _database.ExecuteAsync("Update Budget Set Expense = ? , Balance = ? where Month = ?", spent, balance, month);
        }

        public Task<int> UpdateCategoriesSpentAndBalance(string name, double spent, double balance)
        {
            return _database.ExecuteAsync("Update Categories Set Spent = ? , Balance = ? where Name = ?", spent, balance, name);
        }

        public Task<int> UpdateBudgetAmount(string month, double spent)
        {
            return _database.ExecuteAsync("Update Budget Set BudgetAmount = ?where Month = ?", spent, month);
        }

        public Task<int> UpdateCategoriesAmount(string name, double spent)
        {
            return _database.ExecuteAsync("Update Categories Set Budget = ? , Balance = ? where Name = ?", spent, name);
        }
    }
}

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
        }
        public Task<List<Expense>> GetExpensesAsync()
        {
            return _database.Table<Expense>().ToListAsync();

        }

        public Task<Expense> GetExpenseAsync(int id)
        {
            return _database.Table<Expense>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
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

        public Task<int> DeleteExpenseAsync(Expense expense)
        {
            return _database.DeleteAsync(expense);
        }
    }
}

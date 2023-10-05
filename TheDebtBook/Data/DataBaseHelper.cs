using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;
using Microsoft.Maui;
using TheDebtBook.Models;

namespace TheDebtBook.Data
{

    public static class DataBaseHelper
    {
        static SQLiteAsyncConnection _database;

        public static SQLiteAsyncConnection Database
        {
            get
            {
                if (_database == null)
                {
                    var dbPath = Path.Combine(FileSystem.AppDataDirectory, "TheDebtBook.db");
                    _database = new SQLiteAsyncConnection(dbPath);
                }
                return _database;
            }
        }
        public static async Task InitializeDatabaseAsync()
        {
            await Database.CreateTableAsync<Debtor>();
            await Database.CreateTableAsync<DebtTransaction>();
        }
        //Clean Up Database
        public static async Task ClearDatabaseAsync()
        {
            await Database.DeleteAllAsync<Debtor>();
            await Database.DeleteAllAsync<DebtTransaction>();
        }
        //Debtor CRUD
        public static async Task<Debtor> GetDebtorByNameAsync(string debtorName)
        {
            return await Database.Table<Debtor>().Where(d => d.Name == debtorName).FirstOrDefaultAsync();
        }

        public static Task<int> AddDebtorAsync(Debtor debtor)
        {
            return Database.InsertAsync(debtor);
        }

        public static Task<List<Debtor>> GetAllDebtorsAsync()
        {
            return Database.Table<Debtor>().ToListAsync();
        }
        public static Task<Debtor> GetDebtorByIdAsync(int id)
        {
            return Database.Table<Debtor>().Where(d => d.Id == id).FirstOrDefaultAsync();
        }
        public static Task<int> UpdateDebtorAsync(Debtor debtor)
        {
            return Database.UpdateAsync(debtor);
        }
        public static Task<int> DeleteDebtorAsync(Debtor debtor)
        {
            return Database.DeleteAsync(debtor);
        }


        //DebtTransaction CRUD
        public static Task<int> AddDebtTransactionAsync(DebtTransaction transaction)
        {
            return Database.InsertAsync(transaction);
        }
        public static Task<List<DebtTransaction>> GetTransactionsForDebtorAsync(int debtorId)
        {
            return Database.Table<DebtTransaction>().Where(t => t.DebtorId == debtorId).ToListAsync();
        }
        public static Task<int> UpdateDebtTransactionAsync(DebtTransaction transaction)
        {
            return Database.UpdateAsync(transaction);
        }
        public static Task<int> DeleteDebtTransactionAsync(DebtTransaction transaction)
        {
            return Database.DeleteAsync(transaction);
        }


    }

}

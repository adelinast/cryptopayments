using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;

namespace CryptoPayments
{
    public class TransactionContext : DbContext
    {
        public TransactionContext()
        : base()
        {
			Database.SetInitializer(new DropCreateDatabaseAlways<TransactionContext>());
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
    }

    public class DbInteraction
    {
        public DbInteraction()
        {
            mDbContext = new TransactionContext();

            mDbContext.Database.Log = logInfo => FileLogger.Log(logInfo);
            mDbContext.Database.Connection.ConnectionString = scmConnectionString;

            //for App.config
            ConnectionStringSettings strSettings = ConfigurationManager.ConnectionStrings["ConnectionString"];
            if (strSettings != null)
            {
                mDbContext.Database.Connection.ConnectionString = strSettings.ConnectionString;
            }

            //Console.WriteLine(mDbContext.Database.Connection.ConnectionString);
        }
                
        public void AddTransaction(Transaction transaction)
        {
            mDbContext.Transactions.Add(transaction);
            mDbContext.SaveChanges();
            mDbContext.SaveChangesAsync().Wait();
        }

        public void AddDeposit(Deposit deposit)
        {
            mDbContext.Deposits.Add(deposit);
            mDbContext.SaveChanges();
        }

        public List<Transaction> GetTransactions()
        {
            var transactions = mDbContext.Transactions.ToList();
            return transactions;
        }

        public List<Deposit> GetDeposits()
        {
            var deposits = mDbContext.Deposits.ToList();
            return deposits;
        }

        public Deposit GetDepositByName(string Address)
        {
            var deposit = mDbContext.Deposits.Where((x) => x.Address == Address).First();
            return deposit;
        }

        public bool DepositExists(string Address)
        {
            return mDbContext.Deposits.Any(x => x.Address == Address);
        }

        public double GetMinDeposit()
        {
            return mDbContext.Deposits.Min(x => x.Sum);
        }

        public double GetMaxDeposit()
        {
            return mDbContext.Deposits.Max(x => x.Sum);
        }

        private string scmConnectionString = "Data Source=mysqldocker\\SQLSERVER,1433;Initial Catalog = CryptoPayments.TransactionContext; User Id=sa; Password=Password1; Persist Security Info = True; Connection Timeout=10";
        private TransactionContext mDbContext;
    }

    public class FileLogger
    {
        public static void Log(string logInfo)
        {
            File.AppendAllText(@".\Logger.txt", logInfo);
        }
    }

}

namespace Credigas.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;
    using Models;
    //using SQLite.Net;
    using Xamarin.Forms;
    //using SQLite.Extensions;
    //using SQLite;
    using SQLiteNetExtensions.Extensions;
    using SQLite;

    public class DataAccess : IDisposable
    {
        private SQLiteConnection connection;

        public DataAccess()
        {
            bool eraseDB = false;
            var config = DependencyService.Get<IConfig>();
            connection = new SQLiteConnection( System.IO.Path.Combine(config.DirectoryDB, "Credigas.db3"));

            if (eraseDB)
            {
                connection.DropTable<TokenResponse>();
                connection.DropTable<User>();
                connection.DropTable<Customer>();
                connection.DropTable<Order>();
                connection.DropTable<Payment>();
            }

            connection.CreateTable<TokenResponse>();


            connection.CreateTable<User>();



            connection.CreateTable<Customer>();
            connection.CreateTable<Order>();
            connection.CreateTable<Payment>();
        }



        public int Count(string table)
        {
            
            var list = connection.Query<Customer>("SELECT * FROM " + table);

            return list.Count;

        }


        public void Insert<T>(T model)
        {
            connection.Insert(model);
        }

        public void InsertAll<T>(List<T> list) where T : class
        {
            try
            {
                connection.BeginTransaction();
                foreach (var record in list)
                {
                    connection.Insert(record);
                }
                connection.Commit();
                //var total = Count("["+connection.Table<T>().Table.TableName+"]");
                //var total2 = SelectAll<T>(false);
                //total = total;
            }
            catch (Exception ex)
            {
                connection.Rollback();
                ex.ToString();
            }
        }

        public void Update<T>(T model)
        {
            connection.Update(model);
        }

        public void Delete<T>(T model)
        {
            connection.Delete(model);
        }

        public void DeleteAll<T>(List<T> list ) where T : class
        {
            try
            {
                connection.BeginTransaction();
                foreach (var record in list)
                {
                    connection.Delete(record);
                }
                connection.Commit();

            }
            catch (Exception ex)
            {
                connection.Rollback();
                ex.ToString();
            }
        }

        public Payment GetPaymentWithChildren(object pk)
        {
            return connection.GetWithChildren<Payment>(pk,true);
        }

        public List<T> GetAll<T>(bool withChildren) where T : class
        {
            List<T> res = new List<T>();

            return res;

        }

        public List<Payment> GetPaymentsByOrder(object pk)
        {

            var list = connection.Query<Payment>("SELECT * FROM [Payment] WHERE OrderId = ?", pk);
            return list;
        }

        public List<Payment> GetNewPayments(DateTime date)
        {

            var list = connection.Query<Payment>("SELECT * FROM [Payment] WHERE Date >= ?", date);
                                                
            return list;
        }

        public List<Payment> GetPendingPayments(DateTime date)
        {

            var list = connection.Query<Payment>("SELECT * FROM [Payment] WHERE Date >= ? AND IsSync = 0", date);

            return list;
        }


        public List<Order> GetOrdersByCustomer(object pk)
        {

            var list = connection.Query<Order>("SELECT * FROM [Order] WHERE CustomerId = ?", pk);
            return list;
        }

        public List<TokenResponse> GetAllTokenResponse()
        {

            var list = connection.Query<TokenResponse>("SELECT * FROM [TokenResponse]");
            return list;
        }

        public TokenResponse GetTokenResponse()
        {

            var list = connection.Query<TokenResponse>("SELECT * FROM [TokenResponse]");
            return list.FirstOrDefault();
        }

        public User GetUser()
        {

            var list = connection.Query<User>("SELECT * FROM [User]");
            return list.FirstOrDefault();
        }

        public long GetNextIdForPayment()
        {

            var list = connection.Query<Payment>("SELECT * FROM [Payment] ORDER BY PaymentId DESC");
            var payment = list.FirstOrDefault();
            return payment.PaymentId >= 0 ? payment.PaymentId  + 1: 1;
        }

        public Customer GetCustomer(object pk)
        {

            var list = connection.Query<Customer>("SELECT * FROM [Customer] WHERE CustomerId = ?", pk);
            return list.FirstOrDefault();
        }

        public List<User> GetAllUsers()
        {

            var list = connection.Query<User>("SELECT * FROM [User]");
            return list;
        }

        public List<Customer> GetAllCustomers()
        {

            var list = connection.Query<Customer>("SELECT * FROM [Customer]");
            return list;
        }

        public List<Order> GetAllOrders()
        {

            var list = connection.Query<Order>("SELECT * FROM [Order]");
            return list;
        }

        public List<Payment> GetAllPayments()
        {

            var list = connection.Query<Payment>("SELECT * FROM [Payment]");
            return list;
        }

        public double GetPortfolio()
        {
            double portfolio = 0.0;
            var list = connection.Query<Order>("SELECT * FROM [Order]");

            portfolio = list.AsEnumerable().Sum(o => o.Total);

            return portfolio;
        }

        public double GetCollected()
        {
            double collected = 0.0;
            var list = connection.Query<Payment>("SELECT * FROM [Payment]");

            collected = list.AsEnumerable().Sum(o => o.Total);

            return collected;
        }

        public double GetCollectedToday()
        {
            double collected = 0.0;
            var list = connection.Query<Payment>("SELECT * FROM [Payment] WHERE Date >= ?", DateTime.Today);

            collected = list.AsEnumerable().Sum(o => o.Total);

            return collected;
        }

        public int GetClosed()
        {
            int closed = 0;
            var list = connection.Query<Order>("SELECT * FROM [Order] WHERE Closed = ?", "S");

            closed = list.AsEnumerable().Count();

            return closed;
        }

        public int GetClosedToday()
        {
            int closed = 0;
            var list = connection.Query<Order>("SELECT * FROM [Order] WHERE Closed = ? AND DateModified >= ?", "S", DateTime.Today);

            closed = list.AsEnumerable().Count();

            return closed;
        }

        public void CloseOrder(object pk)
        {
            var list = connection.Query<Order>("UPDATE [Order] SET Closed = ?, DateModified = ? WHERE OrderId = ?", "S", DateTime.Today, pk);

            return;
        }


        /*
        public T First<T>(bool WithChildren) where T : class
        {
            if (WithChildren)
            {
                return null; //ReadOperations.GetAllWithChildren<T>(connection,);//.FirstOrDefault();
            }
            else
            {
                return connection.Table<T>().FirstOrDefault();
            }
        }
        */

        /*
        public List<T> GetList<T>(bool WithChildren) where T : class
        {
            
            if (WithChildren)
            {
                return connection.GetWithChildren<T>().ToList();
            }
            else
            {
                return connection.Table<T>().ToList();
            }
        }
        */
        /*
        public List<T> Get<T>(object pk) where T : class
        {
            TableQuery<T> table = connection.Table<T>();

            var list = connection.Query<T>("SELECT * FROM [" + table.Table.TableName + "] WHERE " + table.Table.PK.Name + " = ?", pk);
            return (List<T>)list;
        }
        */


        /*
        public List<T> SelectAll<T>(bool WithChildren) where T : class
        {
            TableQuery<T> table = connection.Table<T>();
            string query = "SELECT * FROM  " + table.Table.TableName;
            var list = connection.Query<T>(query);

            return (List<T>)list;
        }
        */
        /*
        public T Find<T>(int pk, bool WithChildren) where T : class
        {
            //if (WithChildren)
            //{
            //    return connection.GetAllWithChildren<T>()
            //                     .FirstOrDefault(m => m.GetHashCode() == pk);
            //}
            //else
            {
                return connection.Table<T>()
                                 .FirstOrDefault(m => m.GetHashCode() == pk);
            }
        }
        */

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}

namespace Credigas.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Credigas.Models;
    using Data;

    public class DataService
    {
        
        public bool DeleteAll<T>(List<T> list) where T : class
        {
            try
            {
                using (var da = new DataAccess())
                {
                    da.DeleteAll<T>(list);
                }

                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }


        /*
        public bool DeleteAll<T>(List<T> list) where T : class
        {
            try
            {
                using (var da = new DataAccess())
                {
                    da.DeleteAll<T>(list);

                }

                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }
        */


        public TokenResponse DeleteAllTokensAndInsert(TokenResponse model)
        {
            try
            {
                using (var da = new DataAccess())
                {
                    var oldRecords = da.GetAllTokenResponse();
                    foreach (var oldRecord in oldRecords)
                    {
                        da.Delete(oldRecord);
                    }

                    da.Insert(model);

                    return model;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return model;
            }
        }

        public User DeleteAllUsersAndInsert(User model)
        {
            try
            {
                using (var da = new DataAccess())
                {
                    var oldRecords = da.GetAllUsers();
                    foreach (var oldRecord in oldRecords)
                    {
                        da.Delete(oldRecord);
                    }

                    da.Insert(model);

                    return model;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return model;
            }
        }

        public Customer DeleteAllCustomersAndInsert(Customer model)
        {
            try
            {
                using (var da = new DataAccess())
                {
                    var oldRecords = da.GetAllCustomers();
                    foreach (var oldRecord in oldRecords)
                    {
                        da.Delete(oldRecord);
                    }

                    da.Insert(model);

                    return model;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return model;
            }
        }

        public void DeleteAllCustomers() 
        {
            try
            {
                using (var da = new DataAccess())
                {
                    var oldRecords = da.GetAllCustomers();
                    foreach (var oldRecord in oldRecords)
                    {
                        da.Delete(oldRecord);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public void DeleteAllOrders()
        {
            try
            {
                using (var da = new DataAccess())
                {
                    var oldRecords = da.GetAllOrders();
                    foreach (var oldRecord in oldRecords)
                    {
                        da.Delete(oldRecord);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public void DeleteAllPayments()
        {
            try
            {
                using (var da = new DataAccess())
                {
                    var oldRecords = da.GetAllPayments();
                    foreach (var oldRecord in oldRecords)
                    {
                        da.Delete(oldRecord);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public void DeleteAllVisits()
        {
            try
            {
                using (var da = new DataAccess())
                {
                    var oldRecords = da.GetAllVisits();
                    foreach (var oldRecord in oldRecords)
                    {
                        da.Delete(oldRecord);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        /*
        public T InsertOrUpdate<T>(T model) where T : class
        {
            try
            {
                using (var da = new DataAccess())
                {
                    var oldRecord = da.Find<T>(model.GetHashCode(), false);
                    if (oldRecord != null)
                    {
                        da.Update(model);
                    }
                    else
                    {
                        da.Insert(model);
                    }

                    return model;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return model;
            }
        }
        */

        public T Insert<T>(T model)
        {
            using (var da = new DataAccess())
            {
                da.Insert(model);
                return model;
            }
        }

        public bool InsertAll<T>(List<T> list) where T : class
        {
            try
            {
                using (var da = new DataAccess())
                {
                    da.InsertAll<T>(list);

                }

                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        /*
        public T Find<T>(int pk, bool withChildren) where T : class
        {
            using (var da = new DataAccess())
            {
                return da.Find<T>(pk, withChildren);
            }
        }

        public T First<T>(bool withChildren) where T : class
        {
            using (var da = new DataAccess())
            {
                return da.GetList<T>(withChildren).FirstOrDefault();
            }
        }

        public List<T> Get<T>(bool withChildren) where T : class
        {
            using (var da = new DataAccess())
            {
                return da.GetList<T>(withChildren).ToList();
                //return da.SelectAll<T>(withChildren);
            }
        }

        public List<T> Get<T>(object pk) where T : class
        {
            using (var da = new DataAccess())
            {
                return da.Get<T>(pk);

            }
        }
        */

        public List<Order> GetOrdersByCustomer(long pk)
        {
            using (var da = new DataAccess())
            {
                return da.GetOrdersByCustomer(pk);

            }
        }

        public List<Visit> GetVisits(object debCollectorPK, object customerPK, object orderPK)
        {
            using (var da = new DataAccess())
            {
                return da.GetVisits(debCollectorPK,customerPK,orderPK);

            }
        }

        public List<Visit> GetAllVisits()
        {
            using (var da = new DataAccess())
            {
                return da.GetAllVisits();

            }
        }

        public List<Visit> GetAllPendingVisits()
        {
            using (var da = new DataAccess())
            {
                return da.GetAllPendingVisits();

            }
        }

        public List<Payment> GetPaymentsByOrder(long pk)
        {
            using (var da = new DataAccess())
            {
                return da.GetPaymentsByOrder(pk);

            }
        }

        public Payment GetPaymentWithChildren(object pk)
        {
            using (var da = new DataAccess())
            {
                return da.GetPaymentWithChildren(pk);

            }
        }

        public Visit GetVisitWithChildren(object pk)
        {
            using (var da = new DataAccess())
            {
                return da.GetVisitWithChildren(pk);

            }
        }

        public List<Payment> GetNewPaymentsWithChildren(DateTime date)
        {
            List<Payment> result = new List<Payment>();
            using (var da = new DataAccess())
            {
                var payments = da.GetNewPayments(date);
                foreach (var item in payments)
                {
                    var payment = da.GetPaymentWithChildren(item.PaymentId);
                    var client = da.GetCustomer(payment.Order.CustomerId);
                    payment.Order.Customer = client;
                    result.Add(payment);
                }
            }

            return result;
        }

        public List<Payment> GetPendingPaymentsWithChildren(DateTime date)
        {
            List<Payment> result = new List<Payment>();
            using (var da = new DataAccess())
            {
                var payments = da.GetPendingPayments(date);
                foreach (var item in payments)
                {
                    var payment = da.GetPaymentWithChildren(item.PaymentId);
                    var client = da.GetCustomer(payment.Order.CustomerId);
                    payment.Order.Customer = client;
                    result.Add(payment);
                }
            }

            return result;
        }

        public void Update<T>(T model)
        {
            using (var da = new DataAccess())
            {
                da.Update(model);
            }
        }

        public void Delete<T>(T model)
        {
            using (var da = new DataAccess())
            {
                da.Delete(model);
            }
        }

        public TokenResponse GetTokenResponse()
        {
            using (var da = new DataAccess())
            {
                return da.GetTokenResponse();
            }
        }

        public User GetUser()
        {
            using (var da = new DataAccess())
            {
                return da.GetUser();
            }
        }

        public long GetNextIdForPayment()
        {
            using (var da = new DataAccess())
            {
                return da.GetNextIdForPayment();
            }
        }

        public long GetNextIdForVisit()
        {
            using (var da = new DataAccess())
            {
                return da.GetNextIdForVisit();
            }
        }

        public List<Customer> GetAllCustomers()
        {
            using (var da = new DataAccess())
            {
                return da.GetAllCustomers();
            }
        }

        public List<Order> GetAllOrders()
        {
            using (var da = new DataAccess())
            {
                return da.GetAllOrders();
            }
        }

        public Statistics LoadStatistics()
        {
            Statistics statistics = new Statistics();
            using (var da = new DataAccess())
            {
                statistics.Date = DateTime.Today;
                statistics.Portfolio = da.GetPortfolio();
                statistics.Collected = da.GetCollected();
                statistics.CollectedToday = da.GetCollectedToday();
                statistics.OutstandingBalance = statistics.Portfolio - statistics.Collected;
                statistics.ClosedCards = da.GetClosed();
                statistics.TotalOrders = da.GetTotalOrders();
                statistics.OrdersWithPayment = da.GetOrdersWithPaymentThoday();
                //statistics.CustomersWithoutPayment = da.GetCustomersWithoutPaymentThoday();

            }

            return statistics;
        }

        public void CloseOrder(object pk)
        {
            using (var da = new DataAccess())
            {
                da.CloseOrder(pk);
            }

            return;
        }


        /*
        public void Save<T>(List<T> list) where T : class
        {
            using (var da = new DataAccess())
            {
                foreach (var record in list)
                {
                    InsertOrUpdate(record);
                }
            }
        }
        */
    }
}

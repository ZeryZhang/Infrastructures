using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hk.Infrastructures.Logging;
using Hk.Infrastructures.Mongo.Repository;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Hk.Infrastructures.MongoTests
{
    ///// Business Entity for Customer
    ///// </summary>
    //[CollectionName("CustomersTest")]
    //public class Customer : Entity
    //{
    //    public Customer()
    //    {
    //    }

    //    [BsonElement("fname")]
    //    public string FirstName { get; set; }

    //    [BsonElement("lname")]
    //    public string LastName { get; set; }

    //    public string Email { get; set; }

    //    public string Phone { get; set; }

    //    public Address HomeAddress { get; set; }

    //    public IList<Order> Orders { get; set; }
    //}
    //public class Order
    //{
    //    public DateTime PurchaseDate { get; set; }

    //    public IList<OrderItem> Items;
    //}

    //public class OrderItem
    //{
    //    public Product Product
    //    {
    //        get;
    //        set;
    //    }

    //    public int Quantity { get; set; }
    //}

    //public class Address
    //{
    //    public string Address1 { get; set; }

    //    public string Address2 { get; set; }

    //    public string City { get; set; }

    //    public string PostCode { get; set; }

    //    [BsonIgnoreIfNull]
    //    public string Country { get; set; }
    //}

    //public class IntCustomer : IEntity<int>
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
    ///// <summary>
    ///// Business Entity for Product
    ///// </summary>
    //public class Product : Entity
    //{
    //    public Product()
    //    {
    //    }

    //    public string Name { get; set; }

    //    public string Description { get; set; }

    //    public decimal Price { get; set; }
    //}
    public class Customer : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            //try
            //{
            //    int i = 0;
            //    int b = 1/i;
            //}
            //catch (Exception ex)
            //{
            //    Hk.Infrastructures.Logging.LoggerClient.WriteLog().Debug(-1, "Hk.Infrastructures.MongoTests", "V1.0", ex, ex.Message); 
            //}
            //var url = new MongoUrl(ConfigurationManager.ConnectionStrings["MongoServerSettings"].ConnectionString);
            //var client = new MongoClient(url);
            ////  client.GetServer().DropDatabase(url.DatabaseName);

            //List<string> list = client.GetServer().GetDatabaseNames().ToList();
            //var repo = new MongoRepository<Customer>("InstantMessaging");

            //for (int i = 0; i < 2; i++)
            //{
            //    // adding new entity
            //    var newCustomer = new Customer
            //    {
            //        FirstName = "Steve" + i.ToString(),
            //        LastName = "Cornell"
            //    };

            //    repo.Add(newCustomer);
            //}
         
            //// searching
            //var result = repo.Where(c => c.FirstName == "Steve");

            // updating 
           // newCustomer.LastName = "Castle";
           // repo.Update(newCustomer);


            //  IMongoRepository<Customer> _customerRepo = new MongoRepository<Customer>("InstantMessaging");
            //  IMongoRepositoryManager<Customer> _customerMan = new MongoRepositoryManager<Customer>("InstantMessaging");



            //  var customer = new Customer();
            //  customer.FirstName = "Bob";
            //  customer.LastName = "Dillon";
            //  customer.Phone = "0900999899";
            //  customer.Email = "Bob.dil@snailmail.com";
            //  customer.HomeAddress = new Address
            //  {
            //      Address1 = "North kingdom 15 west",
            //      Address2 = "1 north way",
            //      PostCode = "40990",
            //      City = "George Town",
            //      Country = "Alaska"
            //  };

            //  _customerRepo.Add(customer);
            //  if (_customerMan.Exists)
            //  {
            //      var alreadyAddedCustomer = _customerRepo.Where(c => c.FirstName == "Bob").Single();
            //  }
        }
    }
}

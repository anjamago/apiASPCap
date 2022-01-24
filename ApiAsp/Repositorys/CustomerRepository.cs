using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ApiAsp.Models.Entitys;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApiAsp.Repositorys
{
    public class CustomerRepository
    {
        //private readonly NorthwindContext _context;

        //public CustomerRepository(NorthwindContext context) { 
        //        _context = context;
        //}
        private readonly DB.Models.NorthwindContext constext;
        public CustomerRepository() {
            constext = new DB.Models.NorthwindContext();
        }
      

        public List<Customer> AllCutomer()
        {
            List<Customer> customers = new List<Customer>();
            using (var context =  new ApiAsp.DB.Models.NorthwindContext()){
                var all = context.Customers.Select(c => new Customer
                {
                    CustomerId = c.CustomerId,
                    CompanyName = c.CompanyName,
                    ContactName = c.ContactName,
                    ContactTitle = c.ContactTitle,
                    Address = c.Address,
                    City = c.City,
                    Region = c.Region,
                    Country = c.Country,
                    Phone = c.Phone,
                    Fax = c.Fax,
                    PostalCode = c.PostalCode,
                });
                customers.AddRange(all);
            }
            return customers;
        }


        public async Task<Customer> GetFind(string id)
        {
            Customer customer =  new Customer();
            using (var context = new ApiAsp.DB.Models.NorthwindContext())
            {
                var find = context.Customers.Find(id);
                if(find != null)
                {
                    customer = new Customer
                    {
                        CustomerId = find.CustomerId,
                        CompanyName = find.CompanyName,
                        ContactName = find.ContactName,
                        ContactTitle = find.ContactTitle,
                        Address = find.Address,
                        City = find.City,
                        Region = find.Region,
                        Country = find.Country,
                        Phone = find.Phone,
                        Fax = find.Fax,
                        PostalCode = find.PostalCode,
                    };
                }
                
            }

            return await Task.FromResult(customer);
        }

        public async Task<bool> CreateCustomer(Customer customer)
        {
            DB.Models.Customer customerAdd = new DB.Models.Customer
            {
                CustomerId = customer.CustomerId,
                CompanyName = customer.CompanyName,
                ContactName = customer.ContactName,
                ContactTitle = customer.ContactTitle,
                Address = customer.Address,
                City = customer.City,
                Region = customer.Region,
                Country = customer.Country,
                Phone = customer.Phone,
                Fax = customer.Fax,
                PostalCode = customer.PostalCode,
            };
            var add = await constext.Customers.AddAsync(customerAdd);
            constext.SaveChanges();
            return add.State == EntityState.Added || add.State == EntityState.Modified;
        }

        public async Task  UpdateCustomer(Customer customer)
        {
            DB.Models.Customer register = new DB.Models.Customer
            {
                CustomerId = customer.CustomerId,
                CompanyName = customer.CompanyName,
                ContactName = customer.ContactName,
                ContactTitle = customer.ContactTitle,
                Address = customer.Address,
                City = customer.City,
                Region = customer.Region,
                Country = customer.Country,
                Phone = customer.Phone,
                Fax = customer.Fax,
                PostalCode = customer.PostalCode,
            };

            constext.Entry(register).State = EntityState.Modified;
            await constext.SaveChangesAsync();
        }

        public async Task DeleteCustomer(string customerId)
        {
            DB.Models.Customer customer = new DB.Models.Customer
            {
                CustomerId = customerId,
            };

            constext.Remove(customer);
            await constext.SaveChangesAsync();
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ApiAsp.Models.Entitys;
using System.Threading.Tasks;


namespace ApiAsp.Repositorys
{
    public class CustomerRepository
    {
        //private readonly NorthwindContext _context;

        //public CustomerRepository(NorthwindContext context) { 
        //        _context = context;
        //}

        public List<ApiAsp.DB.Models.Customer> AllCutomer()
        {
            List<ApiAsp.DB.Models.Customer> customer;
            using (var context =  new ApiAsp.DB.Models.NorthwindContext()){
                customer = context.Customers.ToList();
                
                }

                return customer;
        }


        public async Task<ApiAsp.DB.Models.Customer> GetFind(string id)
        {
            ApiAsp.DB.Models.Customer customer;
            using (var context = new ApiAsp.DB.Models.NorthwindContext())
            {
                customer = context.Customers.Find(id);
            }

            return await Task.FromResult(customer);
        }

    }
}

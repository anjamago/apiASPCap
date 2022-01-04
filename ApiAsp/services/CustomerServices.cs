using ApiAsp.Repositorys;
using System;
using System.Threading.Tasks;

namespace ApiAsp.services
{
    public class CustomerServices
    {

        private readonly  CustomerRepository  Repositorys;
            
        public CustomerServices() { 
            Repositorys = new CustomerRepository();
        }

        public dynamic AllCutomer()
        {
            var all = Repositorys.AllCutomer();
            if(all.Count > 0)   
            {
                return all;
            }

            return null;
        }

        public async Task<dynamic> Find(string id)
        {
            return await Repositorys.GetFind(id);
        }
    }
}

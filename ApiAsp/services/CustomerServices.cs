using ApiAsp.Models.Entitys;
using ApiAsp.Repositorys;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ApiAsp.Models.Response;

namespace ApiAsp.services
{
    public class CustomerServices
    {

        private readonly  CustomerRepository  Repositorys;
            
        public CustomerServices() { 
            Repositorys = new CustomerRepository();
        }

        public Response<List<Customer>> AllCutomer()
        {
            try
            {

                var customers = Repositorys.AllCutomer();

                if (customers.Count > 0)
                {
                    return new Response<List<Customer>>(
                            message:"Solicitud OK",
                            count: customers.Count,
                            data: customers);
                }
                return new Response<List<Customer>>(
                               message: "Nose encontraron registros ",
                               count: 0,
                               data: new List<Customer>()
                           );
            }
            catch (Exception ex)
            {

                return new Response<List<Customer>>(
                               status: System.Net.HttpStatusCode.InternalServerError,
                               message: ex.Message,
                               count: 0,
                               data: new List<Customer>()
                           );
            }
        }

        public async Task<Response<Customer>> Find(string id)
        {
            try
            {
                var customer = await Repositorys.GetFind(id);
                if (String.IsNullOrEmpty(customer.CustomerId))
                {
                    return new Response<Customer>(data: null, message: "Nose encontro el registro");

                }
                return new Response<Customer>(data: customer, message: "Solicitud OK");
            }
            catch (Exception ex)
            {
                return new Response<Customer>(
                    data: null, 
                    message: ex.Message, 
                    status: System.Net.HttpStatusCode.InternalServerError
                );
            }
        }

        public async Task<Response<Customer>> CreateCustomer(Customer customer)
        {
            // validar customer 

            try
            {
                var isCreate = await Repositorys.CreateCustomer(customer);

                if (isCreate)
                {
                    return new Response<Customer>(
                       data: new Customer(),
                       message: "Cliente Registrado ",
                       count: 1
                   );
                }

                return new Response<Customer>(
                       data: new Customer(),
                       message: "Nose pudo registrar el Cliente ",
                       count: 0,
                       status: System.Net.HttpStatusCode.BadRequest
                   );
            }catch (Exception ex)
            {
                return new Response<Customer>(
                    data: new Customer(),
                    message: ex.Message,
                    status: System.Net.HttpStatusCode.InternalServerError
                );
            }
        }

    }
}

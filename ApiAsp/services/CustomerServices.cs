using ApiAsp.Models.Entitys;
using ApiAsp.Repositorys;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ApiAsp.Models.Response;
using ApiAsp.services.Validations;
using FluentValidation.Results;

namespace ApiAsp.services
{
    public class CustomerServices
    {

        private readonly  CustomerRepository  Repositorys;
        private readonly CustomerValidator customerValidator;
        public CustomerServices() { 
            Repositorys = new CustomerRepository();
            customerValidator = new  CustomerValidator();
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

        public async Task<Response<Customer>> UpdateCustomer (Customer customer)
        {

            try
            {
                ValidationResult valid = customerValidator.Validate(customer);
                if (valid.IsValid)
                {
                    var findCustomer = await Repositorys.GetFind(customer.CustomerId);
                    if (string.IsNullOrEmpty(findCustomer.CustomerId))
                    {
                        return new Response<Customer>(
                            count: 0,
                            status: System.Net.HttpStatusCode.NotFound,
                            message: " Nose encontro el registro",
                            data: null
                        );
                    }

                    await Repositorys.UpdateCustomer(customer);

                    return new Response<Customer>(
                         count: 1,
                          message: "Cliente actualizado ",
                          data: null
                    );

                }

                return new Response<Customer>(
                        count: 0,
                         message: "Nose encontro el registro ",
                         data: null,
                         errors: valid.Errors
                   );
            }
            catch  {
                return new Response<Customer>(
                          count: 0,
                           message: "Algo a ocurrido Error en el servidor ",
                           data: null,
                           status: System.Net.HttpStatusCode.InternalServerError
                     );
            }

        }

        public async Task<Response<bool>> DeleteCustomer(string id)
        {
            try
            {
                 var customer = await Repositorys.GetFind(id);
                if (string.IsNullOrEmpty(customer.CustomerId))
                {
                    return new Response<bool>(
                           count: 0,
                           status: System.Net.HttpStatusCode.NotFound,
                           message: " Nose encontro el registro",
                           data: false
                       );
                }

                await Repositorys.DeleteCustomer(id);

                return new Response<bool>(
                            count: 0,
                            message: " Usuario Eliminado",
                            data: true
                        );
            }
            catch (Exception ex) {

                return new Response<bool>(
                       count: 0,
                        message: ex.Message,
                        data: false,
                        status: System.Net.HttpStatusCode.InternalServerError
                  );
            }

        }


    }
}

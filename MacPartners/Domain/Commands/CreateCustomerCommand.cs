using MacPartners.Controllers;
using MacPartners.Domain.Models;
using MacPartners.Domain.Models.Entities;
using MacPartners.Domain.Models.Enums;
using MacPartners.Domain.Models.ValueObjects;
using MacPartners.Domain.Queries;
using MacPartners.Domain.Repositories;
using System;

namespace MacPartners.Domain.Commands
{
    public class CreateCustomerCommand
    {
        private readonly ICustomerRepository _repository;
        private readonly CustomerQueries _queries;

        public CreateCustomerCommand(ICustomerRepository repository, CustomerQueries queries)
        {
            _repository = repository;
            _queries = queries;
        }

        public TransactionResponse<Customer> CreateCustomer(Person person, Company company, Address address, Partner partner)
        {
            try
            {
                //1 - Checking if the customer already registred
                if(_queries.IsCustomerAlreadRegistredByCnpj(company.Cnpj) || _queries.IsCustomerAlreadRegistredByCpf(person.Cpf))
                    return new Models.ValueObjects.TransactionResponse<Customer>(new Exception("Cliente já cadastrado"));

                //2 - Create a new Customer
                var customer = company.IsValid? new Customer(company, address, partner) : new Customer(person, address, partner);
                customer.Create(_repository);
                _repository.SaveChanges();

                return new Models.ValueObjects.TransactionResponse<Customer>(customer);
            }
            catch (Exception exception)
            {
                return new Models.ValueObjects.TransactionResponse<Customer>(exception);
            }
        }
    }
}

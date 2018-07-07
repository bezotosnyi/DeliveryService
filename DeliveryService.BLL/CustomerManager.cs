namespace DeliveryService.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading.Tasks;

    using DeliveryService.BLL.Contract;
    using DeliveryService.BLL.Transformer;
    using DeliveryService.BLL.Validator;
    using DeliveryService.DAL.Contract;
    using DeliveryService.Domain;
    using DeliveryService.DTO;

    public class CustomerManager : ICustomerManager
    {
        private readonly IRepository<Customer> customerRepository;

        public CustomerManager(IRepository<Customer> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        ~CustomerManager()
        {
            this.customerRepository?.Dispose();
        }

        public Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            return Task.Run(() => this.customerRepository.Entity.Select(SimpleAutoMapperTransformer.Transform<Customer, CustomerDto>));
        }

        public async Task AddCustomerAsync(CustomerDto customerDto)
        {
            var customer = SimpleAutoMapperTransformer.Transform<CustomerDto, Customer>(customerDto);

            this.Validate(customerDto);

            if (await this.customerRepository.Entity.AnyAsync(
                    x => x.LastName == customer.LastName && x.Name == customer.Name
                                                         && x.Patronymic == customer.Patronymic
                                                         && x.Address == customer.Address))
            {
                throw new ArgumentException("Такой клиент уже существует.");
            }

            this.customerRepository.Entity.Add(customer);
            await this.customerRepository.SaveChangesAsync();
        }

        public Task UpdateCustomerAsync(CustomerDto customerDto)
        {
            this.Validate(customerDto);
            this.customerRepository.Entity.AddOrUpdate(SimpleAutoMapperTransformer.Transform<CustomerDto, Customer>(customerDto));
            return this.customerRepository.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(CustomerDto customerDto)
        {
            var customer =
                await this.customerRepository.Entity.FirstOrDefaultAsync(x => x.CustomerId == customerDto.CustomerId);
            this.customerRepository.Entity.Remove(customer ?? throw new ArgumentException("Не найден клиент для удаления."));
            await this.customerRepository.SaveChangesAsync();
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(long customerId)
        {
            var customer = await this.customerRepository.Entity.FirstOrDefaultAsync(x => x.CustomerId == customerId)
                           ?? throw new ArgumentException("По заданному id клиент не найден.");
            return SimpleAutoMapperTransformer.Transform<Customer, CustomerDto>(customer);
        }

        private void Validate(CustomerDto customerDto)
        {
            var validate = DataAnnotationsValidator.Validate(customerDto);
            if (!validate.Success) throw new ArgumentException(validate.ErrorMessage);
        }
    }
}
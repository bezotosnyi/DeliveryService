namespace DeliveryService.BLL.Contract
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DeliveryService.DTO;

    public interface ICustomerManager
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();

        Task AddCustomerAsync(CustomerDto customerDto);

        Task UpdateCustomerAsync(CustomerDto customerDto);

        Task DeleteCustomerAsync(CustomerDto customerDto);

        Task<CustomerDto> GetCustomerByIdAsync(long customerId);
    }
}
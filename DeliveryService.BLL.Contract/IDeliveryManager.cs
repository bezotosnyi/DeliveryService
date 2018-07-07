namespace DeliveryService.BLL.Contract
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DeliveryService.Domain;
    using DeliveryService.DTO;

    public interface IDeliveryManager
    {
        ICustomerManager CustomerManager { get; }

        ICourierManager CourierManager { get; }

        ITypeOfCargoManager TypeOfCargoManager { get; }

        ITransportManager TransportManager { get; }

        IDepartureManager DepartureManager { get; }

        Task<IEnumerable<DeliveryDto>> GetAllDeliveriesDtoAsync();

        Task<IEnumerable<Delivery>> GetAllDeliveriesAsync();

        Task AddDeliveryAsync(Delivery delivery);

        Task UpdateDeliveryAsync(Delivery delivery);

        Task DeleteDeliveryAsync(Delivery delivery);

        Task<Delivery> GetDeliveryByIdAsync(long deliveryId);

        Task<DeliveryDto> GetDeliveryDtoByIdAsync(long deliveryId);
    }
}
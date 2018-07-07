namespace DeliveryService.BLL.Contract
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DeliveryService.Domain;
    using DeliveryService.DTO;

    public interface ISelectionManager
    {
        IDeliveryManager DeliveryManager { get; }

        Task<IEnumerable<DeliveriesOfCourier>> GetAllDeliveriesByCourierAsync(Courier courier);
    }
}
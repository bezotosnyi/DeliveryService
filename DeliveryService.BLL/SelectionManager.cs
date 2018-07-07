namespace DeliveryService.BLL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DeliveryService.BLL.Contract;
    using DeliveryService.BLL.Transformer;
    using DeliveryService.Domain;
    using DeliveryService.DTO;

    public class SelectionManager : ISelectionManager
    {
        public SelectionManager(IDeliveryManager deliveryManager)
        {
            this.DeliveryManager = deliveryManager;
        }

        public IDeliveryManager DeliveryManager { get; }

        public async Task<IEnumerable<DeliveriesOfCourier>> GetAllDeliveriesByCourierAsync(Courier courier)
        {
            var departureIds =
                (await this.DeliveryManager.DepartureManager.GetAllDeparturesAsync()).Where(
                    x => x.CourierId == courier.CourierId).Select(x => x.DepartureId);

            var deliveryIds = (await this.DeliveryManager.GetAllDeliveriesAsync())
                .Where(x => departureIds.Contains(x.DepartureId)).Select(x => x.DeliveryId);

            return (await this.DeliveryManager.GetAllDeliveriesDtoAsync())
                .Where(x => deliveryIds.Contains(x.DeliveryId)).Select(
                    SimpleAutoMapperTransformer.Transform<DeliveryDto, DeliveriesOfCourier>);
        }
    }
}
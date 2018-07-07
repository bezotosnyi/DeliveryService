namespace DeliveryService.BLL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DeliveryService.BLL.Contract;
    using DeliveryService.DTO;

    public class ReportManager : IReportManager
    {
        public ReportManager(IDeliveryManager deliveryManager)
        {
            this.DeliveryManager = deliveryManager;
        }

        public IDeliveryManager DeliveryManager { get; }

        public async Task<IEnumerable<FinanceReport>> GetFinanceReportAsync()
        {
            return (await this.DeliveryManager.GetAllDeliveriesDtoAsync())
                .Select(
                    x => new FinanceReport
                             {
                                 TypeOfCargo = x.TypeOfCargo,
                                 CostOfCargo = x.CostOfCargo,
                                 PaymentForMileage = x.PaymentForMileage
                             })
                .GroupBy(x => x.TypeOfCargo).Select(
                    x => new FinanceReport
                             {
                                 TypeOfCargo = x.Key,
                                 CostOfCargo = x.Sum(c => c.CostOfCargo),
                                 PaymentForMileage = x.Sum(p => p.PaymentForMileage)
                             });
        }
    }
}
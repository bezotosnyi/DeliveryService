namespace DeliveryService.BLL.Contract
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DeliveryService.DTO;

    public interface IReportManager
    {
        IDeliveryManager DeliveryManager { get; }

        Task<IEnumerable<FinanceReport>> GetFinanceReportAsync();
    }
}
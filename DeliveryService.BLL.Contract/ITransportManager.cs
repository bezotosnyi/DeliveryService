namespace DeliveryService.BLL.Contract
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DeliveryService.DTO;

    public interface ITransportManager
    {
        Task<IEnumerable<TransportDto>> GetAllTransportsAsync();

        Task AddTransportAsync(TransportDto transportDto);

        Task UpdateTransportAsync(TransportDto transportDto);

        Task DeleteTransportAsync(TransportDto transportDto);

        Task<TransportDto> GetTransportByIdAsync(long transportId);
    }
}
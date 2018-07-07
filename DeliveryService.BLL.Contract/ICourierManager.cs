namespace DeliveryService.BLL.Contract
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DeliveryService.Domain;
    using DeliveryService.DTO;

    public interface ICourierManager
    {
        Task<IEnumerable<CourierDto>> GetAllCouriersAsync();

        Task AddCourierAsync(CourierDto courierDto);

        Task UpdateCourierAsync(CourierDto courierDto);

        Task DeleteCourierAsync(CourierDto courierDto);

        Task<CourierDto> GetCourierDtoByIdAsync(long courierId);

        Task<Courier> GetCourierByIdAsync(long courierId);
    }
}
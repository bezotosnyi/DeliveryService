namespace DeliveryService.BLL.Contract
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DeliveryService.Domain;

    public interface IDepartureManager
    {
        Task<IEnumerable<Departure>> GetAllDeparturesAsync();
        
        Task<Departure> AddDepartureAsync(Departure departure);

        Task UpdateDepartureAsync(Departure departure);

        Task DeleteDepartureAsync(Departure departure);

        Task<Departure> GetDepartureByIdAsync(long departureId);
    }
}
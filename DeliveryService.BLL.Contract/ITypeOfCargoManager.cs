namespace DeliveryService.BLL.Contract
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DeliveryService.DTO;

    public interface ITypeOfCargoManager
    {
        Task<IEnumerable<TypeOfCargoDto>> GetAllTypeOfCargoesAsync();

        Task AddTypeOfCargoAsync(TypeOfCargoDto typeOfCargoDto);

        Task UpdateTypeOfCargoAsync(TypeOfCargoDto typeOfCargoDto);

        Task DeleteTypeOfCargoAsync(TypeOfCargoDto typeOfCargoDto);

        Task<TypeOfCargoDto> GetTypeOfCargoByIdAsync(long typeOfCargoId);
    }
}
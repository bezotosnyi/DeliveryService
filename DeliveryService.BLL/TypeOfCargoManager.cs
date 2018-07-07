namespace DeliveryService.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading.Tasks;

    using DeliveryService.BLL.Contract;
    using DeliveryService.BLL.Transformer;
    using DeliveryService.BLL.Validator;
    using DeliveryService.DAL.Contract;
    using DeliveryService.Domain;
    using DeliveryService.DTO;

    public class TypeOfCargoManager : ITypeOfCargoManager
    {
        private readonly IRepository<TypeOfCargo> typeOfCargoRepository;

        public TypeOfCargoManager(IRepository<TypeOfCargo> typeOfCargoRepository)
        {
            this.typeOfCargoRepository = typeOfCargoRepository ?? throw new ArgumentNullException(nameof(typeOfCargoRepository));
        }

        ~TypeOfCargoManager()
        {
            this.typeOfCargoRepository?.Dispose();
        }

        public Task<IEnumerable<TypeOfCargoDto>> GetAllTypeOfCargoesAsync()
        {
            return Task.Run(() => this.typeOfCargoRepository.Entity.Select(SimpleAutoMapperTransformer.Transform<TypeOfCargo, TypeOfCargoDto>));
        }

        public async Task AddTypeOfCargoAsync(TypeOfCargoDto typeOfCargoDto)
        {
            var typeOfCargo = SimpleAutoMapperTransformer.Transform<TypeOfCargoDto, TypeOfCargo>(typeOfCargoDto);

            this.Validate(typeOfCargoDto);

            if (await this.typeOfCargoRepository.Entity.AnyAsync(
                    x => x.Name == typeOfCargo.Name && x.Description == typeOfCargo.Description))
            {
                throw new ArgumentException("Такой тип груза уже существует.");
            }

            this.typeOfCargoRepository.Entity.Add(typeOfCargo);
            await this.typeOfCargoRepository.SaveChangesAsync();
        }

        public Task UpdateTypeOfCargoAsync(TypeOfCargoDto typeOfCargoDto)
        {
            this.Validate(typeOfCargoDto);
            this.typeOfCargoRepository.Entity.AddOrUpdate(SimpleAutoMapperTransformer.Transform<TypeOfCargoDto, TypeOfCargo>(typeOfCargoDto));
            return this.typeOfCargoRepository.SaveChangesAsync();
        }

        public async Task DeleteTypeOfCargoAsync(TypeOfCargoDto typeOfCargoDto)
        {
            var typeOfCargo =
                await this.typeOfCargoRepository.Entity.FirstOrDefaultAsync(x => x.TypeOfCargoId == typeOfCargoDto.TypeOfCargoId);
            this.typeOfCargoRepository.Entity.Remove(typeOfCargo ?? throw new ArgumentException("Не найден тип груза для удаления."));
            await this.typeOfCargoRepository.SaveChangesAsync();
        }

        public async Task<TypeOfCargoDto> GetTypeOfCargoByIdAsync(long typeOfCargoId)
        {
            var typeOfCargo = await this.typeOfCargoRepository.Entity.FirstOrDefaultAsync(x => x.TypeOfCargoId == typeOfCargoId)
                            ?? throw new ArgumentException("По заданному id тип груза не найден.");
            return SimpleAutoMapperTransformer.Transform<TypeOfCargo, TypeOfCargoDto>(typeOfCargo);
        }

        private void Validate(TypeOfCargoDto courier)
        {
            var validate = DataAnnotationsValidator.Validate(courier);
            if (!validate.Success) throw new ArgumentException(validate.ErrorMessage);
        }
    }
}
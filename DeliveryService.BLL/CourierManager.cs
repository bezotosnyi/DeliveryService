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

    public class CourierManager : ICourierManager
    {
        private readonly IRepository<Courier> courierRepository;

        public CourierManager(IRepository<Courier> courierRepository)
        {
            this.courierRepository = courierRepository ?? throw new ArgumentNullException(nameof(courierRepository));
        }

        ~CourierManager()
        {
            this.courierRepository?.Dispose();
        }

        public Task<IEnumerable<CourierDto>> GetAllCouriersAsync()
        {
            return Task.Run(() => this.courierRepository.Entity.Select(SimpleAutoMapperTransformer.Transform<Courier, CourierDto>));
        }

        public async Task AddCourierAsync(CourierDto courierDto)
        {
            var courier = SimpleAutoMapperTransformer.Transform<CourierDto, Courier>(courierDto);

            this.Validate(courierDto);

            if (await this.courierRepository.Entity.AnyAsync(
                    x => x.LastName == courier.LastName && x.Name == courier.Name
                                                         && x.Patronymic == courier.Patronymic
                                                         && x.Address == courier.Address
                                                         && x.HireDate == courier.HireDate))
            {
                throw new ArgumentException("Такой курьер уже существует.");
            }

            this.courierRepository.Entity.Add(courier);
            await this.courierRepository.SaveChangesAsync();
        }

        public Task UpdateCourierAsync(CourierDto courierDto)
        {
            this.Validate(courierDto);
            this.courierRepository.Entity.AddOrUpdate(SimpleAutoMapperTransformer.Transform<CourierDto, Courier>(courierDto));
            return this.courierRepository.SaveChangesAsync();
        }

        public async Task DeleteCourierAsync(CourierDto courierDto)
        {
            var courier =
                await this.courierRepository.Entity.FirstOrDefaultAsync(x => x.CourierId == courierDto.CourierId);
            this.courierRepository.Entity.Remove(courier ?? throw new ArgumentException("Не найден курьер для удаления."));
            await this.courierRepository.SaveChangesAsync();
        }

        public async Task<CourierDto> GetCourierDtoByIdAsync(long courierId)
        {
            var courier = await this.courierRepository.Entity.FirstOrDefaultAsync(x => x.CourierId == courierId)
                           ?? throw new ArgumentException("По заданному id курьер не найден.");
            return SimpleAutoMapperTransformer.Transform<Courier, CourierDto>(courier);
        }

        public async Task<Courier> GetCourierByIdAsync(long courierId)
        {
            return await this.courierRepository.Entity.FirstOrDefaultAsync(x => x.CourierId == courierId)
                   ?? throw new ArgumentException("По заданному id курьер не найден.");
        }

        private void Validate(CourierDto courier)
        {
            var validate = DataAnnotationsValidator.Validate(courier);
            if (!validate.Success) throw new ArgumentException(validate.ErrorMessage);
        }
    }
}
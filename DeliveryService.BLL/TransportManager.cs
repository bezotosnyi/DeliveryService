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

    public class TransportManager : ITransportManager
    {
        private readonly IRepository<Transport> transportRepository;

        public TransportManager(IRepository<Transport> transportRepository)
        {
            this.transportRepository = transportRepository ?? throw new ArgumentNullException(nameof(transportRepository));
        }

        ~TransportManager()
        {
            this.transportRepository?.Dispose();
        }

        public Task<IEnumerable<TransportDto>> GetAllTransportsAsync()
        {
            return Task.Run(() => this.transportRepository.Entity.Select(SimpleAutoMapperTransformer.Transform<Transport, TransportDto>));
        }

        public async Task AddTransportAsync(TransportDto transportDto)
        {
            var transport = SimpleAutoMapperTransformer.Transform<TransportDto, Transport>(transportDto);

            this.Validate(transportDto);

            if (await this.transportRepository.Entity.AnyAsync(
                    x => x.NumberOfCar == transport.NumberOfCar && x.CarModel == transport.CarModel
                                                                && x.DateOfRegistration
                                                                == transport.DateOfRegistration))
            {
                throw new ArgumentException("Такой транспорт уже существует.");
            }

            this.transportRepository.Entity.Add(transport);
            await this.transportRepository.SaveChangesAsync();
        }

        public Task UpdateTransportAsync(TransportDto transportDto)
        {
            this.Validate(transportDto);
            this.transportRepository.Entity.AddOrUpdate(SimpleAutoMapperTransformer.Transform<TransportDto, Transport>(transportDto));
            return this.transportRepository.SaveChangesAsync();
        }

        public async Task DeleteTransportAsync(TransportDto transportDto)
        {
            var transport =
                await this.transportRepository.Entity.FirstOrDefaultAsync(x => x.TransportId == transportDto.TransportId);
            this.transportRepository.Entity.Remove(transport ?? throw new ArgumentException("Не найден транспорт для удаления."));
            await this.transportRepository.SaveChangesAsync();
        }

        public async Task<TransportDto> GetTransportByIdAsync(long transportId)
        {
            var transport = await this.transportRepository.Entity.FirstOrDefaultAsync(x => x.TransportId == transportId)
                          ?? throw new ArgumentException("По заданному id транспорт не найден.");
            return SimpleAutoMapperTransformer.Transform<Transport, TransportDto>(transport);
        }

        private void Validate(TransportDto transportDto)
        {
            var validate = DataAnnotationsValidator.Validate(transportDto);
            if (!validate.Success) throw new ArgumentException(validate.ErrorMessage);
        }
    }
}
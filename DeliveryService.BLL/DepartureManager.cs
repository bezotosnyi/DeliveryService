namespace DeliveryService.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading.Tasks;

    using DeliveryService.BLL.Contract;
    using DeliveryService.BLL.Validator;
    using DeliveryService.DAL.Contract;
    using DeliveryService.Domain;

    public class DepartureManager : IDepartureManager
    {
        private readonly IRepository<Departure> departureRepository;

        public DepartureManager(IRepository<Departure> departureRepository)
        {
            this.departureRepository = departureRepository;
        }

        ~DepartureManager()
        {
            this.departureRepository?.Dispose();
        }

        public Task<IEnumerable<Departure>> GetAllDeparturesAsync()
        {
            return Task.FromResult(this.departureRepository.Entity.AsEnumerable());
        }

        public async Task<Departure> AddDepartureAsync(Departure departure)
        {
            var validate = DataAnnotationsValidator.Validate(departure);
            if (!validate.Success) throw new ArgumentException(validate.ErrorMessage);

            var dep = this.departureRepository.Entity.Add(departure);
            await this.departureRepository.SaveChangesAsync();
            return dep;
        }

        public Task UpdateDepartureAsync(Departure departure)
        {
            this.Validate(departure);
            this.departureRepository.Entity.AddOrUpdate(departure);
            return this.departureRepository.SaveChangesAsync();
        }

        public async Task DeleteDepartureAsync(Departure departure)
        {
            var entity =
                await this.departureRepository.Entity.FirstOrDefaultAsync(x => x.DepartureId == departure.DepartureId);
            this.departureRepository.Entity.Remove(entity ?? throw new ArgumentException("Не найден выезд для удаления."));
            await this.departureRepository.SaveChangesAsync();
        }

        public async Task<Departure> GetDepartureByIdAsync(long departureId)
        {
            return await this.departureRepository.Entity.FirstOrDefaultAsync(x => x.DepartureId == departureId)
                   ?? throw new ArgumentException("По заданному id выезд не найден.");
        }

        private void Validate(Departure departure)
        {
            var validate = DataAnnotationsValidator.Validate(departure);
            if (!validate.Success) throw new ArgumentException(validate.ErrorMessage);
        }
    }
}
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

    public class DeliveryManager : IDeliveryManager
    {
        private readonly IRepository<Delivery> deliveryRepository;

        public ICustomerManager CustomerManager { get; }

        public ICourierManager CourierManager { get; }

        public ITypeOfCargoManager TypeOfCargoManager { get; }

        public ITransportManager TransportManager { get; }

        public IDepartureManager DepartureManager { get; }

        public DeliveryManager(
            IRepository<Delivery> deliveryRepository,
            ICustomerManager customerManager,
            ICourierManager courierManager,
            ITypeOfCargoManager typeOfCargoManager,
            ITransportManager transportManager,
            IDepartureManager departureManager)
        {
            this.deliveryRepository = deliveryRepository ?? throw new ArgumentNullException(nameof(deliveryRepository));
            this.CustomerManager = customerManager ?? throw new ArgumentNullException(nameof(customerManager));
            this.CourierManager = courierManager ?? throw new ArgumentNullException(nameof(courierManager));
            this.TypeOfCargoManager = typeOfCargoManager ?? throw new ArgumentNullException(nameof(typeOfCargoManager));
            this.TransportManager = transportManager ?? throw new ArgumentNullException(nameof(transportManager));
            this.DepartureManager = departureManager ?? throw new ArgumentNullException(nameof(departureManager));
        }

        ~DeliveryManager()
        {
            this.deliveryRepository?.Dispose();
        }

        public Task<IEnumerable<DeliveryDto>> GetAllDeliveriesDtoAsync()
        {
            return Task.Run(
                async () =>
                    {
                        var list = new List<DeliveryDto>();
                        foreach (var delivery in this.deliveryRepository.Entity.AsEnumerable())
                        {
                            var deliveryDto = SimpleAutoMapperTransformer.Transform<Delivery, DeliveryDto>(delivery);
                            deliveryDto.Customer = (await this.CustomerManager.GetCustomerByIdAsync(delivery.CustomerId)).ToString();
                            deliveryDto.TypeOfCargo = (await this.TypeOfCargoManager.GetTypeOfCargoByIdAsync(delivery.TypeOfCargoId)).ToString();
                            delivery.Departure = 
                                await this.DepartureManager.GetDepartureByIdAsync(delivery.DepartureId);
                            deliveryDto.Courier =
                                (await this.CourierManager.GetCourierDtoByIdAsync(delivery.Departure.CourierId))
                                .ToString();
                            deliveryDto.Transport =
                                (await this.TransportManager.GetTransportByIdAsync(delivery.Departure.TransportId))
                                .ToString();
                            deliveryDto.DateOfDeparture = delivery.Departure.DateOfDeparture;
                            deliveryDto.PaymentForMileage = delivery.Departure.PaymentForMileage;

                            list.Add(deliveryDto);
                        }

                        return list.AsEnumerable();
                    });
        }

        public Task<IEnumerable<Delivery>> GetAllDeliveriesAsync()
        {
            return Task.FromResult(this.deliveryRepository.Entity.AsEnumerable());
        }

        public Task AddDeliveryAsync(Delivery delivery)
        {
            this.Validate(delivery);
            this.deliveryRepository.Entity.Add(delivery);
            return this.deliveryRepository.SaveChangesAsync();
        }

        public Task UpdateDeliveryAsync(Delivery delivery)
        {
            this.Validate(delivery);
            this.deliveryRepository.Entity.AddOrUpdate(delivery);
            return this.deliveryRepository.SaveChangesAsync();
        }

        public async Task DeleteDeliveryAsync(Delivery delivery)
        {
            var entity =
                await this.deliveryRepository.Entity.FirstOrDefaultAsync(x => x.DeliveryId == delivery.DeliveryId);
            this.deliveryRepository.Entity.Remove(entity ?? throw new ArgumentException("Не найдена доставка для удаления."));
            await this.deliveryRepository.SaveChangesAsync();
        }

        public async Task<Delivery> GetDeliveryByIdAsync(long deliveryId)
        {
            return await this.deliveryRepository.Entity.FirstOrDefaultAsync(x => x.DeliveryId == deliveryId)
                           ?? throw new ArgumentException("По заданному id доставка не найдена.");
        }

        public async Task<DeliveryDto> GetDeliveryDtoByIdAsync(long deliveryId)
        {
            var delivery = await this.deliveryRepository.Entity.FirstOrDefaultAsync(x => x.DeliveryId == deliveryId)
                   ?? throw new ArgumentException("По заданному id доставка не найдена.");
            return await Task.Run(
                       async () =>
                           {
                               var deliveryDto = SimpleAutoMapperTransformer.Transform<Delivery, DeliveryDto>(delivery);
                               deliveryDto.Customer = (await this.CustomerManager.GetCustomerByIdAsync(delivery.CustomerId)).ToString();
                               deliveryDto.TypeOfCargo = (await this.TypeOfCargoManager.GetTypeOfCargoByIdAsync(delivery.TypeOfCargoId)).ToString();
                               delivery.Departure =
                                   await this.DepartureManager.GetDepartureByIdAsync(delivery.DepartureId);
                               deliveryDto.Courier =
                                   (await this.CourierManager.GetCourierDtoByIdAsync(delivery.Departure.CourierId))
                                   .ToString();
                               deliveryDto.Transport =
                                   (await this.TransportManager.GetTransportByIdAsync(delivery.Departure.TransportId))
                                   .ToString();
                               deliveryDto.DateOfDeparture = delivery.Departure.DateOfDeparture;
                               deliveryDto.PaymentForMileage = delivery.Departure.PaymentForMileage;

                               return deliveryDto;
                           });
        }

        private void Validate<T>(T obj) where T : class, new()
        {
            var validate = DataAnnotationsValidator.Validate(obj);
            if (!validate.Success) throw new ArgumentException(validate.ErrorMessage);
        }
    }
}
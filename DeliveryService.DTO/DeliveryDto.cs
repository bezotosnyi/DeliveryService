namespace DeliveryService.DTO
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class DeliveryDto
    {
        public DeliveryDto()
        {
        }

        public DeliveryDto(
            string courier,
            DateTime dateOfDeparture,
            string transport,
            decimal paymentForMileage,
            DateTime dateOfDelivery,
            string customer,
            int mileage,
            string cargeName,
            string typeOfCargo,
            decimal costOfCargo)
        {
            this.Courier = courier;
            this.DateOfDeparture = dateOfDeparture;
            this.Transport = transport;
            this.PaymentForMileage = paymentForMileage;
            this.DateOfDelivery = dateOfDelivery;
            this.Customer = customer;
            this.Mileage = mileage;
            this.CargeName = cargeName;
            this.TypeOfCargo = typeOfCargo;
            this.CostOfCargo = costOfCargo;
        }

        [DisplayName("Id доставки")]
        public long DeliveryId { get; set; }

        [DisplayName("Клиент")]
        [Required(AllowEmptyStrings = false)]
        public string Customer { get; set; }

        [DisplayName("Курьер")]
        [Required(AllowEmptyStrings = false)]
        public string Courier { get; set; }

        [DisplayName("Наименование груза")]
        [Required(AllowEmptyStrings = false)]
        public string CargeName { get; set; }

        [DisplayName("Тип груза")]
        [Required(AllowEmptyStrings = false)]
        public string TypeOfCargo { get; set; }

        [DisplayName("Стоимость груза")]
        [Required(AllowEmptyStrings = false)]
        public decimal CostOfCargo { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Дата выезда")]
        [Required(AllowEmptyStrings = false)]
        public DateTime DateOfDeparture { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Дата доставки")]
        [Required(AllowEmptyStrings = false)]
        public DateTime DateOfDelivery { get; set; }

        [DisplayName("Транспорт")]
        [Required(AllowEmptyStrings = false)]
        public string Transport { get; set; }

        [DisplayName("Расстояние в км")]
        [Required(AllowEmptyStrings = false)]
        public int Mileage { get; set; }

        [DisplayName("Оплата за километраж")]
        [Required(AllowEmptyStrings = false)]
        public decimal PaymentForMileage { get; set; }        
    }
}
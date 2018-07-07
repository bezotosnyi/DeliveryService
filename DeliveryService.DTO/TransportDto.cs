namespace DeliveryService.DTO
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class TransportDto
    {
        public TransportDto()
        {
        }

        public TransportDto(string numberOfCar, string carModel, DateTime dateOfRegistration)
        {
            this.NumberOfCar = numberOfCar;
            this.CarModel = carModel;
            this.DateOfRegistration = dateOfRegistration;
        }

        [DisplayName("Id транспорта")]
        public long TransportId { get; set; }

        [DisplayName("Номер автомобиля")]
        [StringLength(10)]
        [Required(AllowEmptyStrings = false)]
        public string NumberOfCar { get; set; }

        [DisplayName("Модель автомобиля")]
        [StringLength(20, MinimumLength = 3)]
        [Required(AllowEmptyStrings = false)]
        public string CarModel { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Дата регистрации")]
        [Required(AllowEmptyStrings = false)]
        public DateTime DateOfRegistration { get; set; }

        public override string ToString()
        {
            return $"Автомобиль:{this.CarModel}, номер: {this.NumberOfCar}";
        }
    }
}
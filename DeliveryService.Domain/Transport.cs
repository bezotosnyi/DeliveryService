namespace DeliveryService.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Transport : DomainObject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
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

        public ICollection<Departure> Departures { get; set; }

        public override string ToString()
        {
            return $"Автомобиль:{this.CarModel}, номер: {this.NumberOfCar}";
        }
    }
}
namespace DeliveryService.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Departure : DomainObject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        [DisplayName("Id выезда")]
        public long DepartureId { get; set; }

        [DisplayName("Курьер id")]
        [Required(AllowEmptyStrings = false)]
        public long CourierId { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Дата выезда")]
        [Required(AllowEmptyStrings = false)]
        public DateTime DateOfDeparture { get; set; }

        [DisplayName("Транспорт id")]
        [Required(AllowEmptyStrings = false)]
        public long TransportId { get; set; }

        [DisplayName("Оплата за километраж")]
        [Required(AllowEmptyStrings = false)]
        public decimal PaymentForMileage { get; set; }

        public Courier Courier { get; set; }

        public Transport Transport { get; set; }

        public ICollection<Delivery> Deliveries { get; set; }

        public override string ToString()
        {
            return $"Курьер: {this.Courier}, дата выезда: {this.DateOfDeparture}, транспорт: {this.Transport}, оплата за километраж: {this.PaymentForMileage}";
        }
    }
}
namespace DeliveryService.Domain
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Delivery : DomainObject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        [DisplayName("Id доставки")]
        public long DeliveryId { get; set; }

        [DisplayName("Выезд id")]
        [Required(AllowEmptyStrings = false)]
        public long DepartureId { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Дата доставки")]
        [Required(AllowEmptyStrings = false)]
        public DateTime DateOfDelivery { get; set; }

        [DisplayName("Клиент id")]
        [Required(AllowEmptyStrings = false)]
        public long CustomerId { get; set; }

        [DisplayName("Расстояние в км")]
        [Required(AllowEmptyStrings = false)]
        public int Mileage { get; set; }

        [DisplayName("Наименование груза")]
        [Required(AllowEmptyStrings = false)]
        public string CargeName { get; set; }

        [DisplayName("Тип груза")]
        [Required(AllowEmptyStrings = false)]
        public long TypeOfCargoId { get; set; }

        [DisplayName("Стоимость груза")]
        [Required(AllowEmptyStrings = false)]
        public decimal CostOfCargo { get; set; }

        public Departure Departure { get; set; }

        public Customer Customer { get; set; }

        public TypeOfCargo TypeOfCargo { get; set; }

        public override string ToString()
        {
            return
                $"DepartureId: {this.DepartureId}, DateOfDelivery: {this.DateOfDelivery}, CustomerId: {this.CustomerId}, "
                + $"Mileage: {this.Mileage}, CargeName: {this.CargeName}, TypeOfCargoId: {this.TypeOfCargoId}, "
                + $"CostOfCargo: {this.CostOfCargo}";
        }
    }
}
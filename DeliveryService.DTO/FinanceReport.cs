namespace DeliveryService.DTO
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class FinanceReport
    {
        [DisplayName("Тип груза")]
        [Required(AllowEmptyStrings = false)]
        public string TypeOfCargo { get; set; }

        [DisplayName("Общая стоимость груза")]
        [Required(AllowEmptyStrings = false)]
        public decimal CostOfCargo { get; set; }

        [DisplayName("Общая оплата за километраж")]
        [Required(AllowEmptyStrings = false)]
        public decimal PaymentForMileage { get; set; }
    }
}
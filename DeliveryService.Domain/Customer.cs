namespace DeliveryService.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Customer : DomainObject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        [DisplayName("Id клиента")]
        public long CustomerId { get; set; }

        [DisplayName("Фамилия")]
        [StringLength(50, MinimumLength = 4)]
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [DisplayName("Имя")]
        [StringLength(50, MinimumLength = 4)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [DisplayName("Отчество")]
        [StringLength(50, MinimumLength = 4)]
        [Required(AllowEmptyStrings = false)]
        public string Patronymic { get; set; }

        [DisplayName("Адрес")]
        [StringLength(100, MinimumLength = 4)]
        [Required(AllowEmptyStrings = false)]
        public string Address { get; set; }

        [DisplayName("Контактный телефон")]
        [Phone]
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^\+?(38|8)?\(?0\d{2}\)?\-?\d{3}\-?\d{2}\-?\d{2}$")]
        public string ContactPhone { get; set; }

        [DisplayName("Контактный телефон 2")]
        [Phone]
        [RegularExpression(@"^\+?(38|8)?\(?0\d{2}\)?\-?\d{3}\-?\d{2}\-?\d{2}$")]
        public string ContactPhone2 { get; set; }

        public ICollection<Delivery> Deliveries { get; set; }

        public override string ToString()
        {
            return $"{this.LastName} {this.Name} {this.Patronymic}";
        }
    }
}
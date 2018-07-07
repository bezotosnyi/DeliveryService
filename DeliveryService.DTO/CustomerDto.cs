namespace DeliveryService.DTO
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class CustomerDto
    {
        public CustomerDto()
        {
        }

        public CustomerDto(
            string lastName,
            string name,
            string patronymic,
            string address,
            string contactPhone,
            string contactPhone2)
        {
            this.LastName = lastName;
            this.Name = name;
            this.Patronymic = patronymic;
            this.Address = address;
            this.ContactPhone = contactPhone;
            this.ContactPhone2 = contactPhone2;
        }

        [DisplayName("Id клиента")]
        public long CustomerId { get; set; }

        [DisplayName("Фамилия")]
        [StringLength(50, MinimumLength = 4)]
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; }

        [DisplayName("Имя")]
        [StringLength(50, MinimumLength = 4)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; }

        [DisplayName("Отчество")]
        [StringLength(50, MinimumLength = 4)]
        [Required(AllowEmptyStrings = false)]
        public string Patronymic { get; }

        [DisplayName("Адрес")]
        [StringLength(100, MinimumLength = 4)]
        [Required(AllowEmptyStrings = false)]
        public string Address { get; }

        [DisplayName("Контактный телефон")]
        [Phone]
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^\+?(38|8)?\(?0\d{2}\)?\-?\d{3}\-?\d{2}\-?\d{2}$")]
        public string ContactPhone { get; }

        [DisplayName("Контактный телефон 2")]
        [Phone]
        [RegularExpression(@"^\+?(38|8)?\(?0\d{2}\)?\-?\d{3}\-?\d{2}\-?\d{2}$")]
        public string ContactPhone2 { get; }

        public override string ToString()
        {
            return $"{this.LastName} {this.Name} {this.Patronymic}";
        }
    }
}
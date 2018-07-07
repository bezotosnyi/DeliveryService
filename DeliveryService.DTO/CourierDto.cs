namespace DeliveryService.DTO
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class CourierDto
    {
        public CourierDto()
        {
        }

        public CourierDto(
            string lastName,
            string name,
            string patronymic,
            string passport,
            string address,
            string contactPhone,
            string contactPhone2,
            DateTime hireDate)
        {
            this.LastName = lastName;
            this.Name = name;
            this.Patronymic = patronymic;
            this.Passport = passport;
            this.Address = address;
            this.ContactPhone = contactPhone;
            this.ContactPhone2 = contactPhone2;
            this.HireDate = hireDate;
        }

        [DisplayName("Id курьера")]
        public long CourierId { get; set; }

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

        [DisplayName("Паспорт")]
        [StringLength(8)]
        [Required(AllowEmptyStrings = false)]
        public string Passport { get; set; }

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

        [DataType(DataType.Date)]
        [DisplayName("Дата приема на работу")]
        [Required(AllowEmptyStrings = false)]
        public DateTime HireDate { get; set; }

        public override string ToString()
        {
            return
                $"{this.LastName} {this.Name} {this.Patronymic}";
        }
    }
}
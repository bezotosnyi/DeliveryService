namespace DeliveryService.Domain
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User : DomainObject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        [DisplayName("Id пользователя")]
        public long UserId { get; set; }

        [DisplayName("Имя")]
        [StringLength(50, MinimumLength = 4)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [DisplayName("Фамилия")]
        [StringLength(50, MinimumLength = 4)]
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [DisplayName("Логин")]
        [StringLength(50, MinimumLength = 4)]
        [Required(AllowEmptyStrings = false)]
        public string Login { get; set; }

        [DisplayName("Пароль")]
        [StringLength(50, MinimumLength = 4)]
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }

        public override string ToString()
        {
            return $"UserId: {this.UserId}, Name: {this.Name}, LastName: {this.LastName}, Login: {this.Login}, Password: {this.Password}";
        }
    }
}
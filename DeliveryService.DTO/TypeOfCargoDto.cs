namespace DeliveryService.DTO
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class TypeOfCargoDto
    {
        public TypeOfCargoDto()
        {
        }

        public TypeOfCargoDto(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        [DisplayName("Id типа груза")]
        public long TypeOfCargoId { get; set; }

        [DisplayName("Название")]
        [StringLength(50, MinimumLength = 4)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [DisplayName("Описание")]
        [StringLength(100, MinimumLength = 4)]
        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{this.Name}";
        }
    }
}
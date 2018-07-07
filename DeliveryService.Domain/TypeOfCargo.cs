namespace DeliveryService.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TypeOfCargo : DomainObject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
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

        public ICollection<Delivery> Deliveries { get; set; }

        public override string ToString()
        {
            return $"{this.Name}";
        }
    }
}
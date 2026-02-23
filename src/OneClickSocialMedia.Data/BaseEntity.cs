using System.ComponentModel.DataAnnotations;

namespace OneClickSocialMedia.Data
{
    public abstract class BaseEntity
    {
        [Key]
        /// <summary>
        /// Id of the entity
        /// </summary>
        public Guid Id { get; set; }
    }
}

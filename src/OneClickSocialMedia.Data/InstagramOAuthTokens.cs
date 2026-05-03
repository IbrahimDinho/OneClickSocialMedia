using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneClickSocialMedia.Data
{
    public class InstagramOAuthTokens : BaseEntity
    {
        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual Users User { get; set; }

        /// <summary>
        /// Instagram Access Token
        /// </summary>
        [Required]
        public string AccessToken { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}

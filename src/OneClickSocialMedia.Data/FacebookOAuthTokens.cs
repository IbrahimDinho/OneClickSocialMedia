using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneClickSocialMedia.Data
{
    public class FacebookOAuthTokens : BaseEntity
    {
        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual Users User { get; set; }

        /// <summary>
        ///  Access Token
        /// </summary>
        [Required]
        public string UserAccessToken { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneClickSocialMedia.Data
{
    public class TwitterOAuthTokens : BaseEntity
    {
        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual Users User { get; set; }

        /// <summary>
        /// Twitter Api Key
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string TwitterApiKey { get; set; }

        /// <summary>
        /// Twitter Api Secret
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string TwitterApiSecret { get; set; }

        /// <summary>
        /// Twitter Access Token
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string TwitterAccessToken { get; set; }

        /// <summary>
        /// Twitter Token Secret
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string TwitterAccessTokenSecret { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}

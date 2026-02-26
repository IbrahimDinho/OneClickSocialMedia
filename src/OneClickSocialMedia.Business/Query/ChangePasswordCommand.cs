using MediatR;
using OneClickSocialMedia.Business.Query.Response;

namespace OneClickSocialMedia.Business.Query
{
    public class ChangePasswordCommand : IRequest<ChangePasswordCommandResponse>
    {

        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The old password of the user
        /// </summary>
        public string CurrentPassword { get; set; }

        /// <summary>
        /// The new password of the user
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// The confirmed new password of the user
        /// </summary>
        public string ConfirmNewPassword { get; set; }
    }
}


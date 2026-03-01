using MediatR;
using OneClickSocialMedia.Business.Query.Response;

namespace OneClickSocialMedia.Business.Query
{
    public class Enable2FactorCommand : IRequest<Enable2FactorCommandResponse>
    {

        /// <summary>
        /// Enable 2 factor 
        /// </summary>
        public bool Enable { get; set; }

    }
}


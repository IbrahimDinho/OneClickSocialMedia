using System;
using System.Collections.Generic;
using System.Text;

namespace OneClickSocialMedia.Business.Query.Response
{
    public class ResetPasswordResponse : Response
    {
        public IList<string> ErrorMessages { get; set; }
    }
}

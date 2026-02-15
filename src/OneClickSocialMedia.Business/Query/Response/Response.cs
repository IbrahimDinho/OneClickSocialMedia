using System;
using System.Collections.Generic;
using System.Text;

namespace OneClickSocialMedia.Business.Query.Response
{
    public abstract class Response
    {
        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }


    }
}

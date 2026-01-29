using System;
using System.Collections.Generic;
using System.Text;

namespace OneClickSocialMedia.Business.Query.Response
{
    public class RegisterResponse
    {
        public bool IsSuccess { get; set; }

        public List<string> ErrorMessage { get; set; }


    }
}

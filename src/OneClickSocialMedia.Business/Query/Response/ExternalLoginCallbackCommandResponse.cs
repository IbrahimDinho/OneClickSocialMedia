namespace OneClickSocialMedia.Business.Query.Response
{
    public class ExternalLoginCallbackCommandResponse : Response
    {
        public IList<string> ErrorMessages { get; set; } = new List<string>();

        public string ReturnUrl { get; set; }
    }
}

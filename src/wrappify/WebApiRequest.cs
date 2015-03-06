using System;

namespace wrappify
{
    public class WebApiRequest  
    {
        private readonly IRequestConfiguration _requestConfiguration;

        public WebApiRequest(IRequestConfiguration requestConfiguration)
        {
            _requestConfiguration = requestConfiguration;
            Build();
        }

        public Uri Uri { get; set; }

        private void Build()
        {
            //todo: creae new uri with params
        }
    }
}
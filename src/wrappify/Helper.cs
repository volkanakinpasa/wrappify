﻿using wrappify.Interfaces;

namespace wrappify
{
    public class Helper
    {
        public static string BuildUrl(IRequestConfiguration _requestConfiguration, string path)
        {
            return string.Format("{0}://{1}:{2}/{3}", _requestConfiguration.Scheme, _requestConfiguration.Host, _requestConfiguration.Port, path);
        }
    }
}
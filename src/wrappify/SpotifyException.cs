using System;
using System.Net;

namespace wrappify
{
    internal class SpotifyException : Exception
    {
        public SpotifyException(HttpStatusCode statusCode, string responseJson)
            : base()
        {

        }

        public SpotifyException(Exception ex):base(ex.Message)
        {

        }
    }
}
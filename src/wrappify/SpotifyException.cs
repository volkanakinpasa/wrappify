using System;
using System.Net;

namespace wrappify
{
    internal class SpotifyException : Exception
    {
        public SpotifyException(HttpStatusCode statusCode, string responseJson)
            : base(string.Format("Error occurred. Status : {0}. Message:{1}", statusCode, responseJson))
        {

        }

        public SpotifyException(Exception ex)
            : base(ex.Message)
        {

        }
    }
}
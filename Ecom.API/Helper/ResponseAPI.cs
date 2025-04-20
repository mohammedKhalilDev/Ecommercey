﻿namespace Ecom.API.Helper
{
    public class ResponseAPI
    {

        public ResponseAPI(int statusCode, string message = null)
        {
            statusCode = statusCode;
            StatusCode = statusCode;
            Message = message ?? GetMessageFromStatusCode(statusCode);
        }

        private string GetMessageFromStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Done",
                400 => "Bad Request",
                401 => "Un Authorized",
                500 => "Server Error",
                _ => null,

            };
        }

        public int StatusCode { get; }
        public string Message { get; }
    }
}

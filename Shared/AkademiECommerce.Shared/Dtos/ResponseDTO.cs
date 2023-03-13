using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkademiECommerce.Shared.Dtos
{
    public class ResponseDTO<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public bool IsSuccessful { get; set; }
        public List<string> Errors { get; set; }
        public static ResponseDTO<T> Success(T data, int statusCode)
        {
            return new ResponseDTO<T>
            {
                Data = data,
                StatusCode = statusCode,
                IsSuccessful = true
            };
        }
        public static ResponseDTO<T> Success(int statusCode)
        {
            return new ResponseDTO<T>
            {
                IsSuccessful = true,
                StatusCode = statusCode,
                Data = default(T)
            };
        }
        public static ResponseDTO<T> Fail(List<string> errors, int statusCode)
        {
            return new ResponseDTO<T>
            {
                Errors = errors,
                StatusCode = statusCode,
                IsSuccessful = false
            };
        }
        public static ResponseDTO<T> Fail(string error, int statusCode)
        {
            return new ResponseDTO<T>
            {
                StatusCode = statusCode,
                IsSuccessful = false,
                Errors = new List<string>() { error}
            };
        }
    }
}

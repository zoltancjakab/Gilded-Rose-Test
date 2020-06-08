using System;
using Microsoft.VisualStudio.Web.CodeGeneration;

namespace GildedRose.Web.Services
{

    public class ServiceResult
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public static ServiceResult Failure(Exception e)
        {
            return new ServiceResult()
            {
                ErrorMessage = e.Message,
                IsSuccessful = false,
            };
        }

        public static ServiceResult Failure(string message)
        {
            return new ServiceResult()
            {
                ErrorMessage = message,
                IsSuccessful = false,
            };
        }

        public static ServiceResult<T> Failure<T>(string message) where T : new()
        {
            return new ServiceResult<T>()
            {
                ErrorMessage = message,
                IsSuccessful = false,
            };
        }
    }

    public class ServiceResult<T> : ServiceResult where T : new()
    {
        public T EntityResults { get; set; }
        
        public static ServiceResult<T> Success(T entity)
        {
            return new ServiceResult<T>()
            {
                ErrorMessage = null,
                IsSuccessful = true,
                EntityResults = entity
            };
        }

    }
}
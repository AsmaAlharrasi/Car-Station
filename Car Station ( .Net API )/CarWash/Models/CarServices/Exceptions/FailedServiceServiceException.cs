using System;
using Xeptions;

namespace CarWash.Models.ServicesModels.Exceptions
{
    public class FailedServiceServiceException : Xeption
    {
        public FailedServiceServiceException(System.Exception innerException)
            : base(message: "Failed assignment service error occured, contact support.", innerException){ }
    }
}

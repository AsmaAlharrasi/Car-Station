
using Xeptions;

namespace CarWash.Models.ServicesModels.Exceptions
{
    public class InvalidServiceException : Xeption
    {
        public InvalidServiceException(string parameterName, object parameterValue)
                  : base(message: $"Invalid service, " +
                  $"parameter name: {parameterName}, " +
                  $"parameter value: {parameterValue}.")
        { }

        public InvalidServiceException()
            : base(message: "Invalid service. Please fix the errors and try again.") { }

    }
}

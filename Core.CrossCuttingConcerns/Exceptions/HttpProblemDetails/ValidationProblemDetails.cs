using Core.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails
{
    public class ValidationProblemDetails : ProblemDetails
    {
        public IEnumerable<ValidationExceptionModel> Errors { get; init; }

        public ValidationProblemDetails(IEnumerable<ValidationExceptionModel> errors)
        {
            Title = "Validation Error(s)";
            Detail = "One or more validation errors occurred.";
            Errors = errors;
            Status = StatusCodes.Status400BadRequest;
            Type = "https://example.com/probs/validation";
        }

        public string WriteJsonResponse()
        {
            var combinedDetails = new Dictionary<string, object>
            {
                { "Title", Title },
                { "Detail", Detail },
                { "Status", Status },
                { "Type", Type },
                { "Errors", Errors }
            };
            return JsonSerializer.Serialize(combinedDetails);
        }
    }
}

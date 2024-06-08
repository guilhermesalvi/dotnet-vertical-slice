using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace VerticalSlice.Api.Shared.SeedWork.Models;

public class ValidationProblemResult : ProblemDetails
{
    [JsonPropertyName("errors")]
    public IEnumerable<string> Errors { get; set; }

    public ValidationProblemResult(IEnumerable<string> errors)
    {
        Title = "One or more validation errors occurred.";
        Errors = errors;
    }
}

using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using VerticalSlice.Api.Shared.Notifications;

namespace VerticalSlice.Api.Shared.SeedWork.Models;

public class ValidationProblemResult : ProblemDetails
{
    [JsonPropertyName("errors")]
    public IEnumerable<string> Errors { get; set; }

    public ValidationProblemResult(IEnumerable<string> errors) => Errors = errors;

    public ValidationProblemResult(IEnumerable<Notification> notifications) =>
        Errors = notifications.Select(n => n.Value);
}

namespace WebReview.API.Contracts.Errors;

public record ErrorResponse(string Message);

public record ValidationErrorResponse(string Message, IEnumerable<string> Errors);

public record SuccessResponse<T>(string Message, T Data);

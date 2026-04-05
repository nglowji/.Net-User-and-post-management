using FluentValidation;
using WebReview.API.Contracts.Errors;
using WebReview.Application.Exceptions;

namespace WebReview.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new ValidationErrorResponse("Dữ liệu không hợp lệ.", ex.Errors.Select(x => x.ErrorMessage)));
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(new ErrorResponse(ex.Message));
        }
        catch (ForbiddenException ex)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(new ErrorResponse(ex.Message));
        }
        catch (InvalidOperationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new ErrorResponse(ex.Message));
        }
        catch
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new ErrorResponse("Có lỗi hệ thống, vui lòng thử lại."));
        }
    }
}

using FluentValidation;
using WebReview.Application.Features.Users.Commands;

namespace WebReview.Application.Features.Users.Validators;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Mã người dùng không hợp lệ.");
    }
}

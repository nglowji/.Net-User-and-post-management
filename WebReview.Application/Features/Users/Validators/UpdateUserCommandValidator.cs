using FluentValidation;
using WebReview.Application.Features.Users.Commands;

namespace WebReview.Application.Features.Users.Validators;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Mã người dùng không hợp lệ.");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Tên đăng nhập không được để trống.")
            .MinimumLength(3).WithMessage("Tên đăng nhập tối thiểu 3 ký tự.")
            .MaximumLength(50).WithMessage("Tên đăng nhập tối đa 50 ký tự.");
    }
}

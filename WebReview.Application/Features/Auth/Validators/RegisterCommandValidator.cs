using FluentValidation;
using WebReview.Application.Features.Auth.Commands;

namespace WebReview.Application.Features.Auth.Validators;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Tên đăng nhập không được để trống.")
            .MinimumLength(3).WithMessage("Tên đăng nhập tối thiểu 3 ký tự.")
            .MaximumLength(50).WithMessage("Tên đăng nhập tối đa 50 ký tự.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Mật khẩu không được để trống.")
            .MinimumLength(6).WithMessage("Mật khẩu tối thiểu 6 ký tự.")
            .MaximumLength(100).WithMessage("Mật khẩu tối đa 100 ký tự.");
    }
}

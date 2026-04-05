using FluentValidation;
using WebReview.Application.Features.Posts.Commands;

namespace WebReview.Application.Features.Posts.Validators;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("Mã người dùng không hợp lệ.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Tiêu đề không được để trống.")
            .MaximumLength(200).WithMessage("Tiêu đề tối đa 200 ký tự.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Nội dung không được để trống.");
    }
}

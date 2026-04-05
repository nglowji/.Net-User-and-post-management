using FluentValidation;
using WebReview.Application.Features.Posts.Commands;

namespace WebReview.Application.Features.Posts.Validators;

public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
{
    public DeletePostCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Mã bài viết không hợp lệ.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("Mã người dùng không hợp lệ.");
    }
}

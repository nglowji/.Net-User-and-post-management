using FluentValidation;
using WebReview.Application.Features.Posts.Queries;

namespace WebReview.Application.Features.Posts.Validators;

public class GetPostByIdQueryValidator : AbstractValidator<GetPostByIdQuery>
{
    public GetPostByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Mã bài viết không hợp lệ.");
    }
}

using FluentValidation;
using WebReview.Application.Features.Users.Queries;

namespace WebReview.Application.Features.Users.Validators;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Mã người dùng không hợp lệ.");
    }
}

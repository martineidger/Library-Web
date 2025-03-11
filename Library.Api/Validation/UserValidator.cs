using FluentValidation;
using Library.Api.Contracts;

namespace Library.Api.Validation
{
    public class UserValidator : AbstractValidator<UserContract>
    {
        public UserValidator()
        {
                
        }
    }
}

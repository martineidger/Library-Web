using FluentValidation;
using Library.Api.Contracts;

namespace Library.Api.Validation
{
    public class BookValidator : AbstractValidator<BookContract>
    {
        public BookValidator()
        {
            RuleFor(x => x.ISBN)
            .NotEmpty().WithMessage("ISBN is required.")
            .Length(10, 13).WithMessage("ISBN must be between 10 and 13 characters long.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

            RuleFor(x => x.Genre)
                .NotEmpty().WithMessage("Genre is required.")
                .MaximumLength(50).WithMessage("Genre must not exceed 50 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

            RuleFor(x => x.AuthorID)
                .NotEqual(Guid.Empty).WithMessage("Author ID is required.");

            RuleFor(x => x.ImgFile)
                .Must(file => file == null || (file.Length > 0 && file.ContentType.StartsWith("image/")))
                .WithMessage("ImgFile must be a valid image file.");

            RuleFor(x => x.PickDate)
                .LessThan(x => x.ReturnDate).WithMessage("Pick date must be earlier than return date.")
                .When(x => x.ReturnDate.HasValue); 

            RuleFor(x => x.ReturnDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("Return date must be in the future.")
                .When(x => x.ReturnDate.HasValue); 
        }
    }
}

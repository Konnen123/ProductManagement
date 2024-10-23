using Infrastructure.Persistance;


namespace Application.Use_Cases.Commands
{
    public class CreateProductCommandValidator : BaseProductCommandValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator(ApplicationDbContext context) : base(context)
        {
        }
    }
}

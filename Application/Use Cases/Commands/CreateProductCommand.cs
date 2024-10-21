using MediatR;

namespace Application.Use_Cases.Commands
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal TVA { get; set; }
    }
}

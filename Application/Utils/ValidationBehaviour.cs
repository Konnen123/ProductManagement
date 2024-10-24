using FluentValidation;
using MediatR;

namespace Application
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            Console.WriteLine(typeof(TResponse));
            if (validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    //Singura posibilitate in care am reusit sa returnez erorile de validare ca result, dar asta implica sa am un constructor public pentru clasa Result, ceea ce nu e ok, codul e f complicat de asemenea. 
                    
                    // var error = new Error("Validation failed", string.Join(", ", failures.Select(f => f.ErrorMessage)));
                    // var resultType = typeof(TResponse).GetGenericTypeDefinition().MakeGenericType(typeof(TResponse).GetGenericArguments());
                    // return (TResponse)Activator.CreateInstance(resultType, error);
                    
                    throw new ValidationException(failures);
                }
            }

            return await next();
        }
    }
}
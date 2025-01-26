using FluentResults;
using Domain.SharedKernel;
using Resources;

namespace Domain.Features.Transactions.ValueObjects
{
    public class Amount : ValueObject
    {
        public decimal Value { get; }

        private Amount(decimal value)
        {
            Value = value;
        }

        public static Result<Amount> Create(decimal value)
        {
            var result = new Result<Amount>();

            if (value < 0)
            {
                string message = string.Format(Messages.Null, DataDictionary.Amount);

                result.WithError(message);

                return result;
            }

            if (value > 1_000_000) 
                return Result.Fail<Amount>("Amount exceeds the maximum limit.");

            return Result.Ok(new Amount(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
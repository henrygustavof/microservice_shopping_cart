namespace Product.Domain.ValueObject
{
    using Enum;

    public class Money
    {
        public decimal Amount { get; }
        public Currency Currency { get; }

        public Money()
        {
        }

        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public static Money Dollars(decimal amount)
        {
            return new Money(amount, Currency.USD);
        }

        public static Money Soles(decimal amount)
        {
            return new Money(amount, Currency.PEN);
        }

        public static Money Euros(decimal amount)
        {
            return new Money(amount, Currency.EUR);
        }
    }
}

using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new Contract()
                .Requires()
                .IsNullOrEmpty(FirstName, "Name.FirstName", "Nome inválido")
                .IsNullOrEmpty(LastName, "Name.LastName", "Sobrenome inválido")
                .HasMinLen(FirstName, 3, "Name.FirstName", "O Nome deve ter pelo menos 3 caracteres")
                .HasMinLen(LastName, 3, "Name.LastName", "O Sobrenome deve ter pelo menos 3 caracteres")
            );

            // if (string.IsNullOrEmpty(FirstName))
            //     AddNotification("Name.FirstName", "Nome inválido");

            // if (string.IsNullOrEmpty(LastName))
            //     AddNotification("Name.LastName", "Sobrenome inválido");
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
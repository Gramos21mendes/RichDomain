using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscriptions;
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();

            AddNotifications(name, document, email);
        }

        public Name Name { get; set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Adress Adress { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions => _subscriptions.ToArray();


        public void AddSubscripton(Subscription subscription)
        {
            // //Cancela todas as outras assinaturas e coloca esta como principal.
            // foreach (var sub in Subscriptions)
            //     sub.DeactivateSubscription();

            // _subscriptions.Add(subscription);

            var hasSubscriptionActive = false;

            foreach (var sub in _subscriptions)
            {
                if (sub.Active)
                    hasSubscriptionActive = true;
            }

            AddNotifications(new Contract()
            .Requires()
            .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Você ja tem uma assinatura ativa."));

            //Alternativa
            // if(hasSubscriptionActive)
            // AddNotification("Student.Subscriptions", "Você ja tem uma assinatura ativa.");

            if (Valid)
                _subscriptions.Add(subscription);
        }

    }
}
using System.Collections.Generic;
using System.Linq;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class Student
    {

        private IList<Subscription> _subscriptions;
        public Student(Name name, string document, string email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();
        }

        public Name Name { get; set; }
        public string Document { get; private set; }
        public string Email { get; private set; }
        public string Adress { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions => _subscriptions.ToArray();


        public void AddSubscripton(Subscription subscription)
        {
            //Cancela todas as outras assinaturas e coloca esta como principal.
            foreach (var sub in Subscriptions)
                sub.DeactivateSubscription();

            _subscriptions.Add(subscription);
        }

    }
}
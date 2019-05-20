using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{

    [TestClass]
    public class StudentTest
    {

        private readonly Name _name;
        private readonly Document _document;
        private readonly Student _student;
        private readonly Adress _address;
        private readonly Email _email;
        private readonly Subscription _subscription;

        public StudentTest()
        {
            _name = new Name("Guilherme", "Ramos Mendes");
            _document = new Document("11950265609", EDocumentType.CPF);
            _email = new Email("guilherme.mendes@interplayer.com.br");
            _address = new Adress("Rua Ricardo Zacharias", "50", "Parque América", "São Paulo", "SP", "Brasil", 04841020);
            _student = new Student(_name, _document, _email);
            _subscription = new Subscription(null);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            //Cria pagamento
            var payment = new PayPalPayment("123456789", DateTime.Now, DateTime.Now.AddDays(5), 100, 100, "Guilherme", _document, _address, _email);
            //Adiciona Pagamento
            _subscription.AddPayment(payment);
            //Adiciona inscrição
            _student.AddSubscripton(_subscription);
            _student.AddSubscripton(_subscription);
            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
        {
            //Adiciona inscrição.
            _student.AddSubscripton(_subscription);
            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSucessWhenAddSubscription()
        {
            //Cria Pagamento.
            var payment = new PayPalPayment("123456789", DateTime.Now, DateTime.Now.AddDays(5), 100, 100, "Guilherme", _document, _address, _email);
            //Adiciona Pagamento.
            _subscription.AddPayment(payment);
            //Adiciona inscrição.
            _student.AddSubscripton(_subscription);
            Assert.IsTrue(_student.Invalid);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;
using PaymentContext.Domain.ValueObjects;
using System;
using PaymentContext.Domain.Commands;

namespace PaymentContext.Tests
{
    [TestClass]
    public class SubscriptionHandlerTest
    {
        // Red, Green, Refactor
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();

            command.FirstName = "Guilherme";
            command.LastName = "Ramos Mendes";
            command.Document = "99999999999";
            command.Email = "guilherme.mendes@interplayers2.com.br";

            command.BarCode = "1516512313";
            command.BoletoNumber = "181561";

            command.PaymentNumber = "5189165";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddDays(5);
            command.Total = 200;
            command.TotalPaid = 200;
            command.Payer = "Guilherme";
            command.PayerDocument = "12345678945";
            command.PayerDocumentType = EDocumentType.CPF;
            command.Street = "Rua Ricardo Zacharias";
            command.Number = "50";
            command.Neighborhood = "Parque América";
            command.City = "São Paulo";
            command.State = "SP";
            command.Country = "Brasil";
            command.ZipCode = 042241020;

            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);
        }

    }
}

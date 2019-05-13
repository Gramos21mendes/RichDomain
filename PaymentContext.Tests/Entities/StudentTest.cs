using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTest
    {
        [TestMethod]
        public void TestMethod1()
        {

            var name = new Name("Guilherme", "Ramos Mendes");
            var document = new Document("11950265609", EDocumentType.CPF);
            var email = new Email("guilherme.rmendes95gmail.com");
            var student = new Student(name, document, email);
        }
    }
}

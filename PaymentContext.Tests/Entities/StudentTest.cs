using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
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
            var student = new Student(name, "11950265609", "guilherme.rmendes95@gmail.com");
        }
    }
}

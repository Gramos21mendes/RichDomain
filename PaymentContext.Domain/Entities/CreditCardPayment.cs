using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class CreditCardPayment : Payment
    {
        public CreditCardPayment(string cardHolderName
        , string cardNumber
        , string lastTransactionNumber
        , DateTime paidDate
        , DateTime expireDate
        , decimal total
        , decimal totalPaid
        , string payer
        , Document document
        , Adress adress
        , Email email) : base(paidDate, expireDate, total, totalPaid, payer, document, adress, email)
        {
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            LastTransactionNumber = lastTransactionNumber;
        }

        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string LastTransactionNumber { get; set; }
    }
}

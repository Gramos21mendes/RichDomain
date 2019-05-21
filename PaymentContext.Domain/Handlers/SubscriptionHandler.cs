using System;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable, IHandler<CreateBoletoSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        //Boleto
        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Fail Fast Validationss
            command.Validate();

            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar seu cadastro");
            }

            //Verificar se o documento já está cadastrado.
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este documento já está em uso.");

            //Verificar se o email já está cadastrado.
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este email já está em uso.");

            //Gerar os Vos
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            //Gerar Entidades 
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.BarCode, command.BoletoNumber, command.PaidDate, command.ExpireDate
            , command.Total, command.TotalPaid, command.Payer, new Document(command.PayerDocument, command.PayerDocumentType)
            , address, email);


            //Relacionametos
            subscription.AddPayment(payment);
            student.AddSubscripton(subscription);

            //Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Salvar informações
            if (Valid)
            {


                _repository.CreateSubscription(student);

                //Enviar E-mail de boas-vindas
                _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo !", "Sua Assinatura foi criada.");

                //retornar informações

                return new CommandResult(true, "Assinatura realizada com sucesso.");

            }
            else
            {
                return new CommandResult(false, "Não foi possível realizar a assinatura.");
            }
        }

        //PayPal
        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            //Fail Fast Validationss
            command.Validate();

            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar seu cadastro");
            }

            //Verificar se o documento já está cadastrado.
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este documento já está em uso.");

            //Verificar se o email já está cadastrado.
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este email já está em uso.");

            //Gerar os Vos
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            //Gerar Entidades 
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(command.TransactionCode, command.PaidDate, command.ExpireDate
            , command.Total, command.TotalPaid, command.Payer, new Document(command.PayerDocument, command.PayerDocumentType)
            , address, email);


            //Relacionametos
            subscription.AddPayment(payment);
            student.AddSubscripton(subscription);

            //Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Salvar informações
            if (Valid)
            {
                _repository.CreateSubscription(student);

                //Enviar E-mail de boas-vindas
                _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo !", "Sua Assinatura foi criada.");

                //retornar informações
                return new CommandResult(true, "Assinatura realizada com sucesso.");

            }
            else
            {
                return new CommandResult(false, "Não foi possível realizar a assinatura.");
            }
        }

        //CreditCard
        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
            //Fail Fast Validationss
            command.Validate();

            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar seu cadastro");
            }

            //Verificar se o documento já está cadastrado.
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este documento já está em uso.");

            //Verificar se o email já está cadastrado.
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este email já está em uso.");

            //Gerar os Vos
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            //Gerar Entidades 
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new CreditCardPayment(command.CardHolderName, command.CardNumber, command.LastTransactionNumber, command.PaidDate, command.ExpireDate
            , command.Total, command.TotalPaid, command.Payer, new Document(command.PayerDocument, command.PayerDocumentType)
            , address, email);


            //Relacionametos
            subscription.AddPayment(payment);
            student.AddSubscripton(subscription);

            //Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            //Salvar informações
            if (Valid)
            {


                _repository.CreateSubscription(student);

                //Enviar E-mail de boas-vindas
                _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo !", "Sua Assinatura foi criada.");

                //retornar informações

                return new CommandResult(true, "Assinatura realizada com sucesso.");

            }
            else
            {
                return new CommandResult(false, "Não foi possível realizar a assinatura.");
            }
        }
    }
}
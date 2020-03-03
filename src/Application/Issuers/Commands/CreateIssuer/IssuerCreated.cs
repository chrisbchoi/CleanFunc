using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using CleanFunc.Application.Common.Interfaces;
using MediatR;

namespace CleanFunc.Application.Issuers.Commands.CreateIssuer
{
    public class IssuerCreated : INotification
    {
        public Guid IssuerId {get;set;}
        
        public class IssuerCreatedHandler : INotificationHandler<IssuerCreated>
        {
            private readonly IEmailService _emailService;
            private readonly IBusFactory messageSenderFactory;

            public IssuerCreatedHandler(IEmailService emailService, 
                                            IBusFactory messageSenderFactory)
            {
                Guard.Against.Null(emailService, nameof(emailService));
                Guard.Against.Null(messageSenderFactory, nameof(messageSenderFactory));

                _emailService = emailService;
                this.messageSenderFactory = messageSenderFactory;
            }

            public async Task Handle(IssuerCreated notification, CancellationToken cancellationToken)
            {
                // send event to the service bus
                var sender = messageSenderFactory.Create<IssuerCreated>();
                //await sender.SendAsync(notification);

                // TODO use details from notification to fill in email dto
                await _emailService.SendAsync(new EmailMessageDto());
            }
        }
    }
}
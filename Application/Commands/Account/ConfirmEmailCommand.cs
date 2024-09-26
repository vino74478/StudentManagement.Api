using Application.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Account
{
    public class ConfirmEmailCommand : IRequest<bool>
    {
        public string userId { get; set; }
        public string token { get; set; }
    }

    public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailCommand, bool>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<RegisterHandler> _logger;

        public ConfirmEmailHandler(ILogger<RegisterHandler> logger,
                                   IAccountRepository accountRepository)
        {
            _logger = logger;
            _accountRepository = accountRepository;
        }
        public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            return await _accountRepository.ConfirmEmail(request.userId, request.token);
        }
    }
}

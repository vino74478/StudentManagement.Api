using Application.Commands.Students;
using Application.DTO.Account;
using Application.Interface;
using Domain.Entities.Account;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Account
{
    public class RegisterCommand : IRequest<bool>
    {
        public RegisterDto Register { get; set; }
    }

    public class RegisterHandler : IRequestHandler<RegisterCommand, bool>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<RegisterHandler> _logger;

        public RegisterHandler(ILogger<RegisterHandler> logger,
                               IAccountRepository accountRepository)
        {
            _logger = logger;
            _accountRepository = accountRepository;
        }
        public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var register = new Register()
            {
                Email = request.Register.Email,
                FirstName = request.Register.FirstName,
                LastName = request.Register.LastName,
                PhoneNumber = request.Register.PhoneNumber,
                Password = request.Register.Password,
                ConfirmPassword = request.Register.ConfirmPassword

            };
            return await _accountRepository.Register(register);
        }
    }
}

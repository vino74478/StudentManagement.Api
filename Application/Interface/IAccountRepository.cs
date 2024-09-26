using Domain.Entities.Account;

namespace Application.Interface
{
    public interface IAccountRepository
    {
        Task<bool> Register(Register userDTO);
        Task<bool> Login(Login loginDTO);

        Task<bool> ConfirmEmail(string userId, string token);
    }
}

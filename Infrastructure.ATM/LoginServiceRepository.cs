using Application.ATM.port.In;
using Domain.ATM;

namespace Infrastructure.ATM
{
    public class LoginServiceRepository : ILoginServiceRepository
    {
        public bool? Login(Account account)
        {
            return true;
        }
    }
}
using Application.ATM.port.In;
using Domain.ATM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ATM
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginService: ILoginService
    {
        private readonly ILoginServiceRepository _loginServiceRepository;

        public LoginService(ILoginServiceRepository loginServiceRepository)
        {
            _loginServiceRepository = loginServiceRepository;
        }
        public bool? Login(Account account)
        {

            return _loginServiceRepository.Login(account);
        }
    }
}

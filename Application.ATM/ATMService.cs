using Domain.ATM;

namespace Application.ATM
{
    public class ATMService
    {
        private readonly ILoginService _loginService;

        public ATMService(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public bool? Login(AccountRequest accountRequest)
        {
            bool? result = false;

            // TODO: Login 作業
            Account account = new Account()
            {
                Aid = accountRequest.UserId,
                Password = accountRequest.Password
            };

            result = _loginService.Login(account);

            // TODO: CheckIsCustomer 作業


            return result;
        }
    }
}
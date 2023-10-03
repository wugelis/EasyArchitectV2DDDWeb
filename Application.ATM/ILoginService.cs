using Domain.ATM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Application.ATM
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILoginService: IService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        bool? Login(Account account);
    }
}

using Domain.ATM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ATM.port.In
{
    public interface ILoginServiceRepository
    {
        bool? Login(Account account);
    }
}

using Application.ATM.port.In;
using Domain.ATM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ATMTests.Fake
{
    public class FakeLoginServiceRepository : ILoginServiceRepository
    {
        public bool? Login(Account account)
        {
            return true;
        }
    }
}

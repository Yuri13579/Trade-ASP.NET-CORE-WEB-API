using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1U_ASP.Security.Model
{
    public interface ILogActionServeProcess
    {
        Task<int> CreateAppAction(LogActionDto logActionDto);
    }
}

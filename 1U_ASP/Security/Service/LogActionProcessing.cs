using System;
using System.Threading.Tasks;
using _1U_ASP.Models;
using _1U_ASP.Repositorys.Interface;
using _1U_ASP.Security.Model;
using Microsoft.EntityFrameworkCore;

namespace _1U_ASP.Security.Service
{
    public class LogActionProcessing : ILogActionServeProcess
    {
        #region DI
        private readonly IRepository<SysCode> _sysCodeRepository;
        private readonly IRepository<Person> _person;
        private readonly IRepository<UserAction> _logAction;

        public LogActionProcessing(
            IRepository<SysCode> sysCodeRepository,
            IRepository<Person> person,
            IRepository<UserAction> logAction)
        {
            _sysCodeRepository = sysCodeRepository;
            _person = person;
            _logAction = logAction;
        }
        #endregion

        public async Task<int> CreateAppAction(LogActionDto logActionDto)
        {
            var sysCode = await _sysCodeRepository.List(x => x.Deleted== false                 
                                                             && x.ShortDesc == logActionDto.Method).FirstOrDefaultAsync();
                //.GetBySpecAsync(new SysCodeSpec(false, logActionDto.Method, true));

                var person = await _person.List(x => x.Deleted == false
                                                     && x.PersonId == logActionDto.PersonId).FirstOrDefaultAsync();
                //.GetBySpecAsync(new PersonSpec(false, logActionDto.PersonId));
                    
            var logAction = await _logAction.AddAsync(new UserAction
            {
                ActionDate = DateTime.Now,
                ActionSysCodeUniqueId = sysCode?.UniqueId ?? 0,
                Email = person?.EmailAddress,
                Name = $"{person?.FirstName} {person?.LastName}",
                Browser = logActionDto.Browser,
                Country = logActionDto.Country,
                Device = logActionDto.Device,
                Os = logActionDto.Os,
                UserId = logActionDto.UserId,
                WebClickUrl = logActionDto.WebClickUrl,
                Ipaddress = logActionDto.Ipaddress
            });

            return logAction.UserActionId;
        }
    }
}

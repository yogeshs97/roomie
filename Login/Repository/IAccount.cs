using Login.Models;
using Login.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Repository
{
    public interface IAccount
    {
        public Task<AccountLogin> GetAccount(AccountLoginViewModel model);
    }
}

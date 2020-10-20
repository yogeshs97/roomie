using Login.Models;
using Login.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Repository
{
    public class AccountRepository : IAccount
    {
        private readonly AppDbContext dbContext;

        public AccountRepository(AppDbContext context)
        {
            this.dbContext = context;
        }
        public async Task<AccountLogin> GetAccount(AccountLoginViewModel model)
        {
            var output = await dbContext.AccLogin.Where(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefaultAsync();
            return output;
        }
    }
}

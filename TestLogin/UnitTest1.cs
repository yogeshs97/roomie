using Login.Models;
using Login.Models.ViewModel;
using Login.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace TestLogin
{
    public class Tests
    {
        AppDbContext _dbContext;
        List<AccountLogin> accounts = new List<AccountLogin>();
        IQueryable<AccountLogin> accountsdata;
        Mock<DbSet<AccountLogin>> mockset;
        Mock<AppDbContext> mockcontext;
        [SetUp]
        public void Setup()
        {
            accounts = new List<AccountLogin>
            {
                new AccountLogin{ Id=1, Username="sharmayogesh",Password="yogesh"}
            };

            accountsdata = accounts.AsQueryable();

            mockset = new Mock<DbSet<AccountLogin>>();
            mockset.As<IQueryable<AccountLogin>>().Setup(m => m.Provider).Returns(accountsdata.Provider);
            mockset.As<IQueryable<AccountLogin>>().Setup(m => m.Expression).Returns(accountsdata.Expression);
            mockset.As<IQueryable<AccountLogin>>().Setup(m => m.ElementType).Returns(accountsdata.ElementType);
            mockset.As<IQueryable<AccountLogin>>().Setup(m => m.GetEnumerator()).Returns(accountsdata.GetEnumerator());

            var mocksetcontext = new DbContextOptions<AppDbContext>();
            mockcontext = new Mock<AppDbContext>(mocksetcontext);
            mockcontext.Setup(x => x.AccLogin).Returns(mockset.Object);
        }

        [Test]
        public async void Test1()
        {
            //Assert.Pass();
            var accountRepo = new AccountRepository(mockcontext.Object);
            var account = await accountRepo.GetAccount(new AccountLoginViewModel { Username="sharmayogesh",Password="yogesh"});
            var expectedOutput = new AccountLogin { Id = 1, Username = "sharmayogesh", Password = "yogesh" };
            Assert.AreEqual(expectedOutput, account);
        }
    }
}
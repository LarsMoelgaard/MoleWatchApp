using MoleWatchApp.Interfaces;
using MoleWatchApp.ViewModels;
using NUnit.Framework;

namespace UnitTest_MoleWatchApp
{
    public class Tests
    {
        private LoginViewModel LoginVM;
        private ILogin fakeLoginModel;

        [SetUp]
        public void Setup()
        {
            LoginViewModel LoginVM = new LoginViewModel();
            ILogin fakeLoginModel = new FakeLoginModel();

        }

        [Test]
        public void LoginWithCorrectLoginInfo_LoginVerified()
        {
            //Arrange 
            LoginVM.Password = "Test123";
            LoginVM.UsernameInput = "Test123";

        }
    }

    public class FakeLoginModel : ILogin
    {
        private string username = "Test123";
        private string password = "Test123";


        public bool VerifyPassword(string Username, string Password)
        {
            if (Username == username && Password == password)
            {
                return true;
            }
            else return false;
        }

        public bool VerifySmartLoginPassword()
        {
            throw new System.NotImplementedException();
        }
    }
}
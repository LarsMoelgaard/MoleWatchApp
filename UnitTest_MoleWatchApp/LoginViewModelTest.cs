using System.Threading;
using DataClasses.DTO;
using MoleWatchApp.Interfaces;
using MoleWatchApp.Interfaces.IModel;
using MoleWatchApp.ViewModels;
using MoleWatchApp.Views;
using NSubstitute;
using NUnit.Framework;
using Xamarin.Forms;

namespace UnitTest_MoleWatchApp
{
    public class LoginViewModelTest
    {
        private LoginViewModel LoginVM;
        private ILogin fakeLoginModel;

        [SetUp]
        public void Setup()
        {
            LoginVM = new LoginViewModel();
            fakeLoginModel = Substitute.For<ILogin>();
            LoginVM.loginModel = fakeLoginModel;
        }

        [TearDown]
        public void TearDown()
        {
            LoginVM.Password = null;
            //LoginVM.UsernameLabel = null;
            //LoginVM.UsernameInput = null;
            LoginVM = null;
            fakeLoginModel = null;
            
        }


        [TestCase("000000-1234", "Test123")]
        [TestCase("123456-7890", "123456")]
        public void LoginWithPassword_VerifyPasswordIsCalled(string cpr, string code)
        {
            //Arrange
            LoginVM.Password = code;
            LoginVM.UsernameInput = cpr;

            //Act
            LoginVM.LoginCommand.Execute(null);
            Thread.Sleep(100);

            //Assert
            fakeLoginModel.Received(1).VerifyPassword(cpr, code);
        }


        [TestCase("000000-1234", "Test123")]
        [TestCase("123456-7890", "123456")]
        public void LoginCorrectWithPassword_UsernamelabelDoesNotChange(string cpr, string code)
        {
            //Arrange 
            LoginVM.Password = code;
            LoginVM.UsernameInput = cpr;
            string labelText = LoginVM.UsernameLabel;

            fakeLoginModel.VerifyPassword(cpr, code).Returns(true);
            //Act 
            LoginVM.LoginCommand.Execute(null);
            Thread.Sleep(100);

            //Assert 
            Assert.That(labelText, Is.EqualTo(LoginVM.UsernameLabel));
        }


        [TestCase("000000-1234", "Test123")]
        [TestCase("123456-7890", "123456")]
        public void LoginWithWrongPassword_UsernameLabelChanges(string cpr, string code)
        {
            //Arrange 
            LoginVM.Password = code;
            LoginVM.UsernameInput = cpr;
            string labelText = LoginVM.UsernameLabel;
            fakeLoginModel.VerifyPassword(cpr, code).Returns(false);

            //Act 
            LoginVM.LoginCommand.Execute(null);
            Thread.Sleep(100);


            //Assert 
            Assert.That(labelText, !Is.EqualTo(LoginVM.UsernameLabel));

        }
    }
}
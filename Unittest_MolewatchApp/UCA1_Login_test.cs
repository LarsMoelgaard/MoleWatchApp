using NUnit.Framework;

namespace Unittest_MolewatchApp
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            LoginPage loginPage = new LoginPage();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}
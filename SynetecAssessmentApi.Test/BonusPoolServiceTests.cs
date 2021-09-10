using NUnit.Framework;
using SynetecAssessmentApi.Services;

namespace SynetecAssessmentApi.Test
{
    public class BonusPoolServiceTests
    {

        BonusPoolService _bonusPoolService;

        [OneTimeSetUp]
        public void Setup()
        {
            _bonusPoolService = new BonusPoolService(MockDBContext.GetMockDBContext());
        }

        [Test]
        public void CalculateAsync_Valid()
        {
            var result = _bonusPoolService.CalculateAsync(10000, 1);

            Assert.AreEqual("Mr Person 1", result.Result.Employee.Fullname);
            Assert.AreEqual("CEO", result.Result.Employee.JobTitle);
            Assert.AreEqual(1000, result.Result.Employee.Salary);
            Assert.AreEqual("Finance", result.Result.Employee.Department.Title);
            Assert.AreEqual("The finance department for the company", result.Result.Employee.Department.Description);
            Assert.AreEqual(1000M, result.Result.Amount);
        }

        [Test]
        public void CalculateAsync_No_EmployeeId()
        {
            var result = _bonusPoolService.CalculateAsync(10000, 0);

            Assert.AreEqual("Employee with Employee Id 0 not found", result.Exception.InnerException.Message);
        }

        [Test]
        public void CalculateAsync_Invalid_EmployeeId()
        {
            var result = _bonusPoolService.CalculateAsync(10000, 16);

            Assert.AreEqual("Employee with Employee Id 16 not found", result.Exception.InnerException.Message);
        }


        [Test]
        public void CalculateAsync_Intern()
        {
            var result = _bonusPoolService.CalculateAsync(10000, 6);

            Assert.AreEqual("Master Intern", result.Result.Employee.Fullname);
            Assert.AreEqual("Accountant", result.Result.Employee.JobTitle);
            Assert.AreEqual(0, result.Result.Employee.Salary);
            Assert.AreEqual("Finance", result.Result.Employee.Department.Title);
            Assert.AreEqual("The finance department for the company", result.Result.Employee.Department.Description);

            Assert.AreEqual(0, result.Result.Amount);
        }

        [Test]
        public void CalculateAsync_Fraction_Of_Pound()
        {
            var result = _bonusPoolService.CalculateAsync(15, 1);
            Assert.AreEqual(1.5M, result.Result.Amount);
        }


        [Test]
        public void CalculateAsync_Fraction_Rounded_2dp()
        {
            var result = _bonusPoolService.CalculateAsync(10, 2);
            Assert.AreEqual(2.44M, result.Result.Amount);
        }
    }
}
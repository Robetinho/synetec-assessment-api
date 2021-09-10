using Microsoft.EntityFrameworkCore;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Persistence; 

namespace SynetecAssessmentApi.Test
{ 
    public static class MockDBContext
    { 
        public static AppDbContext GetMockDBContext()
        {

            var options = new DbContextOptionsBuilder<AppDbContext>()
              .UseInMemoryDatabase(databaseName: "Test")
              .Options;

            var context = new AppDbContext(options);

            context.Employees.Add(new Employee(1, "Mr Person 1", "CEO", 1000, 1));
            context.Employees.Add(new Employee(2, "Mrs Person 2", "CTO", 2445, 2));
            context.Employees.Add(new Employee(3, "Miss Person 3", "HR", 2555, 3));
            context.Employees.Add(new Employee(4, "Dr Person 4", "IT", 4000, 4));
            context.Employees.Add(new Employee(6, "Master Intern", "Accountant", 0, 1));

            context.Departments.Add(new Department(1, "Finance", "The finance department for the company"));
            context.Departments.Add(new Department(2, "Human Resources", "The Human Resources department for the company"));
            context.Departments.Add(new Department(3, "IT", "The IT support department for the company"));
            context.Departments.Add(new Department(4, "Marketing", "The Marketing department for the company"));

            context.SaveChanges();

            return context;
        }
    }
}

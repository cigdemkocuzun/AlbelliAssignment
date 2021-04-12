using System.Data.SqlClient;
using System.Threading.Tasks;
using NUnit.Framework;
using Albelli.Domain.Customers;
using Albelli.Infrastructure.Processing;
using Albelli.IntegrationTests.SeedWork;
using Albelli.Application.CQRS.Customers.Command;
using Albelli.Application.CQRS.Customers.GetCustomerDetails;
using Albelli.Application.IntegrationHandlers;

namespace Albelli.IntegrationTests.Customers
{
    [TestFixture]
    public class CustomersTests : TestBase
    {
        [Test]
        public async Task RegisterCustomerTest()
        {
            const string email = "newCustomer@mail.com";
            const string name = "Sample Company";
            
            var customer = await CommandsExecutor.Execute(new RegisterCustomerCommand(email, name));
            var customerDetails = await QueriesExecutor.Execute(new GetCustomerDetailsQuery(customer.Id));

            Assert.That(customerDetails, Is.Not.Null);
            Assert.That(customerDetails.Name, Is.EqualTo(name));
            Assert.That(customerDetails.Email, Is.EqualTo(email));

        }
    }
}
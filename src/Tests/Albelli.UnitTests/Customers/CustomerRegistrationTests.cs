﻿using NSubstitute;
using NUnit.Framework;
using Albelli.Domain.Customers;
using Albelli.Domain.Customers.Rules;
using Albelli.UnitTests.SeedWork;

namespace Albelli.UnitTests.Customers
{
    [TestFixture]
    public class CustomerRegistrationTests : TestBase
    {
        [Test]
        public void GivenCustomerEmailIsUnique_WhenCustomerIsRegistering_IsSuccessful()
        {
            // Arrange
            var customerUniquenessChecker = Substitute.For<ICustomerUniquenessChecker>();
            const string email = "testEmail@email.com";
            customerUniquenessChecker.IsUnique(email).Returns(true);

            // Act
            var customer = Customer.CreateRegistered(email, "Sample name", customerUniquenessChecker);

            // Assert
            AssertPublishedDomainEvent<CustomerRegisteredEvent>(customer);
        }

        [Test]
        public void GivenCustomerEmailIsNotUnique_WhenCustomerIsRegistering_BreaksCustomerEmailMustBeUniqueRule()
        {
            // Arrange
            var customerUniquenessChecker = Substitute.For<ICustomerUniquenessChecker>();
            const string email = "testEmail@email.com";
            customerUniquenessChecker.IsUnique(email).Returns(false);

            // Assert
            AssertBrokenRule<CustomerEmailMustBeUniqueRule>(() =>
            {
                // Act
                Customer.CreateRegistered(email, "Sample name", customerUniquenessChecker);
            });
        }
    }
}
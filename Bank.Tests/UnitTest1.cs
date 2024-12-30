using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Bank.Tests
{
    public class CustomerTests
    {
        [Fact]
        public void CanCreateCustomer()
        {
            var customer = new Customer
            {
                FirstName = "caio",
                LastName = "jacobina",
                AccountNumber = "cj-0-00-00",
                Pin = "A1234"
            };

            Assert.Equals("caio", customer.FirstName);
            Assert.Equals("jacobina", customer.LastName);
        }
    }

    internal class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountNumber { get; set; }
        public string Pin { get; set; }
    }

    internal class FactAttribute : Attribute
    {
    }
}

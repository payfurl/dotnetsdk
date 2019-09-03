﻿using evertech.sdk;
using evertech.sdk.Models;
using NUnit.Framework;

namespace FunctionalTests
{
    [TestFixture]
    public class PaymentMethod
    {
        [SetUp]
        public void SetConfig()
        {
            Config.Setup("SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", Environment.LOCAL);
        }

        [Test]
        public void GetForCustomer()
        {
            var search = new CustomerSearch
            {
                Reference = "123123123"
            };

            var svc = new evertech.sdk.Customer();
            var result = svc.Search(search);

            var customerId = result.Customers[0].CustomerId;

            var payMethodSvc = new evertech.sdk.PaymentMethod();
            var paymentMethods = payMethodSvc.GetForCustomer(customerId);

            Assert.AreEqual(1, paymentMethods.Count);
        }
    }
}
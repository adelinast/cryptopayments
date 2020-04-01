using NUnit.Framework;

namespace CryptoPayments
{
    public class TransactionTest
    {
        private Transaction _transaction;

        [SetUp]
        public void Init()
        {
            _transaction = new Transaction();
            _transaction.FillKnownAddresses();
        }

        [TearDown]
        public void Dispose()
        {  
        }

        #region Sample_TestCode
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]

        public void ReturnFalseGivenValuesLessThan6(int value)
        {
            _transaction.Confirmations = value;
            var result = _transaction.IsValid();

            Assert.IsFalse(result, $"{value} should not be valid");
        }

        [TestCase(6)]
        public void ReturnTrueGivenValuesLessThan6(int value)
        {
            _transaction.Confirmations = value;
            var result = _transaction.IsValid();

            Assert.IsTrue(result, $"{value} should not be valid");
        }
        
        [TestCase("mvd6qFeVkqH6MNAS2Y2cLifbdaX5XUkbZJ")]
        public void ReturnTrueKnownAddresses(string Value)
        {
            Assert.IsTrue(Transaction.IsKnownAddress(Value));
        }

        [TestCase("mmFFG4jqAtw9MoCC88hw5FNfreQWuEHADp1")]
        public void ReturnFalseKnownAddresses(string Value)
        {
            Assert.IsFalse(Transaction.IsKnownAddress(Value));
        }
        #endregion
    }
}

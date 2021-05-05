using MyMockLibrary;
using NUnit.Framework;
using System;
using System.Linq.Expressions;

namespace TestMyMockLibrary
{
    public class IFooTests
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void Name_Property_ShouldSucceed()
        {
            // Arrange
            var mockExample = new MyMock<IFoo>();

            // Act
            mockExample.MockMethod(x => x.Name, "Is Property");
            var example = mockExample.Object;

            // Assert
            Assert.AreEqual("Is Property", example.Name);
        }

        [Test]
        public void Value_Property_ShouldSucceed()
        {
            // Arrange
            var mockExample = new MyMock<IFoo>();

            // Act
            mockExample.MockMethod(x => x.Value, 5);
            var example = mockExample.Object;

            // Assert
            Assert.AreEqual(5, example.Value);
        }

        [Test]
        public void GetValueByRef_ShouldSucceed()
        {
            // Arrange
            var mockExample = new MyMock<IFoo>();
            int myValue = 76;

            // Act
            mockExample.MockMethod(nameof(IFoo.GetValueByRef));
            var example = mockExample.Object;
            example.GetValueByRef(ref myValue);

            // Assert
            Assert.AreEqual(76, myValue);
        }

        [Test]
        public void GetValueByOut_ShouldSucceed()
        {
            // Arrange
            var mockExample = new MyMock<IFoo>();

            // Act
            mockExample.MockMethod(nameof(IFoo.GetValueByOut));
            var example = mockExample.Object;
            example.GetValueByOut(out int myValue);

            // Assert
            Assert.AreEqual(default(int), myValue);
        }

        [Test]
        public void GetValueByRef_Lambda_ShouldSucceed()
        {
            // Arrange
            var mockExample = new MyMock<IFoo>();
            int myValue = 76;

            // Act
            mockExample.MockMethod(x => x.GetValueByRef(ref myValue));
            var example = mockExample.Object;
            example.GetValueByRef(ref myValue);

            // Assert
            Assert.AreEqual(76, myValue);
        }

        [Test]
        public void GetValueByOut_Lambda_ShouldSucceed()
        {
            // Arrange
            var mockExample = new MyMock<IFoo>();
            int myValue;

            // Act
            mockExample.MockMethod(x => x.GetValueByOut(out myValue));
            var example = mockExample.Object;
            example.GetValueByOut(out myValue);

            // Assert
            Assert.AreEqual(default(int), myValue);
        }
    }
}

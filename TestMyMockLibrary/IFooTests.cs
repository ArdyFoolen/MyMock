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
    }
}

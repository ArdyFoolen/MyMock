using MyMockLibrary;
using NUnit.Framework;

namespace TestMyMock
{
    public class MyMockTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
		public void Mock_ShouldLambdaMock()
		{
			// Arrange
			var mockExample = new MyMock<IExample>();

			// Act
			mockExample.MockMethod(x => x.ExampleMethod(), "hi from MyMock");
			var example = mockExample.Object;

			// Assert
			Assert.AreEqual("hi from MyMock", example.ExampleMethod());
		}

		[Test]
		public void Mock_ShouldLambdaMock_Again()
		{
			// Arrange
			var mockExample = new MyMock<IExample>();

			// Act
			mockExample.MockMethod(x => x.MagicNumber(1234), 4321);
			var example = mockExample.Object;

			// Assert
			Assert.AreEqual(4321, example.MagicNumber(1234));
		}

		[Test]
		public void Mock_ShouldCombineLambdaMock()
		{
			// Arrange
			var mockExample = new MyMock<IExample>();

			// Act
			mockExample.MockMethod(x => x.ExampleMethod(), "hi from MyMock");
			mockExample.MockMethod(x => x.MagicNumber(1234), 4321);
			var example = mockExample.Object;

			// Assert
			Assert.AreEqual("hi from MyMock", example.ExampleMethod());
			Assert.AreEqual(4321, example.MagicNumber(1234));
		}

		[Test]
		public void Mock_ShouldMock()
		{
			// Arrange
			var mockExample = new MyMock<IExample>();

			// Act
			mockExample.MockMethod(nameof(IExample.ExampleMethod), "hi from MyMock");
			var example = mockExample.Object;

			// Assert
			Assert.AreEqual("hi from MyMock", example.ExampleMethod());
		}

		[Test]
		public void Mock_ShouldMock_Again()
		{
			// Arrange
			var mockExample = new MyMock<IExample>();

			// Act
			mockExample.MockMethod(nameof(IExample.MagicNumber), 4321);
			var example = mockExample.Object;

			// Assert
			Assert.AreEqual(4321, example.MagicNumber(1234));
		}

		[Test]
		public void Mock_ShouldCombineMock()
		{
			// Arrange
			var mockExample = new MyMock<IExample>();

			// Act
			mockExample.MockMethod(nameof(IExample.ExampleMethod), "hi from MyMock");
			mockExample.MockMethod(nameof(IExample.MagicNumber), 4321);
			var example = mockExample.Object;

			// Assert
			Assert.AreEqual("hi from MyMock", example.ExampleMethod());
			Assert.AreEqual(4321, example.MagicNumber(1234));
		}

		[Test]
		public void Mock_ShouldCombineMock_Again()
		{
			// Arrange
			var mockExample = new MyMock<IExample>();

			// Act
			mockExample.MockMethod(nameof(IExample.ExampleMethod), "hi from MyMock");
			var example = mockExample.Object;
			mockExample.MockMethod(nameof(IExample.MagicNumber), 4321);

			// Assert
			Assert.AreSame(example, mockExample.Object);
			Assert.AreEqual("hi from MyMock", example.ExampleMethod());
			Assert.AreEqual(4321, example.MagicNumber(1234));
		}
	}
}
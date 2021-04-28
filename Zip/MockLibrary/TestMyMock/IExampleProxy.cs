//// This class is not used, it is an example class that is auto generated
//// By the MyMock class with generic parameter IExample,
//// it generates this source code and compiles it

//using System;
//using System.Collections.Generic;
//using TestMyMock;

//namespace MyMockLibrary
//{
//	public partial class MyMock<TMockable>
//		where TMockable : class
//	{

//		private class IExampleProxy : IExample
//		{
//			private readonly Dictionary<string, Func<object>> _methodInterceptors;

//			public IExampleProxy(Dictionary<string, Func<object>> methodInterceptors)
//			{
//				_methodInterceptors = methodInterceptors;
//			}

//			public object InterceptMethod(string methodName)
//			{
//				if (!_methodInterceptors.ContainsKey(methodName))
//					throw new NotImplementedException();
//				return _methodInterceptors[methodName]();
//			}

//			// These are the methods generated from the interface
//			public String ExampleMethod()
//			{
//				var result = InterceptMethod(nameof(ExampleMethod));
//				return (String)result;
//			}

//			public int MagicNumber(int number)
//			{
//				var result = InterceptMethod(nameof(MagicNumber));
//				return (Int32)result;
//			}
//		}
//	}
//}

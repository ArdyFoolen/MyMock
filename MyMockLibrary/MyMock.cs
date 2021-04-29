﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyMockLibrary
{
    public partial class MyMock<TMockable>
        where TMockable : class
    {
        #region Private properties

        private readonly Dictionary<string, Func<object>> _methodInterceptors = new Dictionary<string, Func<object>>();
        private readonly TypeInfo _type = typeof(TMockable).GetTypeInfo();
        private TMockable mockable = null;

        #endregion

        public MyMock<TMockable> MockMethod<TResult>(
            Expression<Func<TMockable, TResult>> methodCall, TResult result)
        {
            if (methodCall.Body is MethodCallExpression)
                return MockMethodCall((MethodCallExpression)methodCall.Body, result);
            if (methodCall.Body is MemberExpression)
                return MockProperty((MemberExpression)methodCall.Body, result);
            throw new ArgumentException();
        }

        public MyMock<TMockable> MockMethod<TResult>(
            string methodName, TResult result)
        {
            _methodInterceptors[methodName] = () => result;
            return this;
        }

        public TMockable Object
        {
            get
            {
                if (mockable == null)
                {
                    Type genericType = CreateType().MakeGenericType(typeof(TMockable));
                    mockable = (TMockable)Activator.CreateInstance(genericType, _methodInterceptors);
                }
                return mockable;
            }
        }

        #region Private methods to CreateType

        private MyMock<TMockable> MockMethodCall<TResult>(
            MethodCallExpression method, TResult result)
        {
            return MockMethod(method.Method.Name, result);
        }

        private MyMock<TMockable> MockProperty<TResult>(
            MemberExpression property, TResult result)
        {
            return MockMethod($"get_{property.Member.Name}", result);
        }

        private Type CreateType()
        {
            var typeToCreate = typeof(TMockable);
            var newTypeName = $"{_type.Name}Proxy";
            var typeFullName = GetTypeFullName(typeToCreate);

            var referenceTypes = GetMockableMethods()
                .Select(x => x.ReturnType)
                .Concat(GetMockableMethods().SelectMany(xx => xx.GetParameters())
                    .Select(aa => aa.GetType()));

            var namespaces = string.Join("\r\n",
                referenceTypes.Select(x => x.Namespace)
                .Concat(new string[] { "System", "System.Collections.Generic", typeToCreate.Namespace })
                .Distinct()
                .Select(x => $"using {x};")
                .ToList());

            var properties = string.Join("\r\n", GetMockableProperties().Select(x => WriteProperty(x)));
            var methods = string.Join("\r\n", GetMockableMethods().Select(x => WriteMethod(x)));
            var sourceCode = string.Format(ProxyFormats.ProxyClassFormat, namespaces, _type.Name, typeFullName, properties, methods);

            var compiler = new AssemblyCompiler()
                .UseReference<TMockable>();
            var assembly = compiler.Compile(sourceCode, newTypeName);

            return assembly.DefinedTypes.Where(x => newTypeName.Equals(x.Name)).First();
        }

        private string WriteProperty(PropertyInfo mockableProperty)
        {
            var returnType = mockableProperty.GetMethod.ReturnType;
            var returnTypeName = returnType == typeof(void) ? "void" : returnType.Name;
            var propertyName = mockableProperty.Name;
            var typeCode = Type.GetTypeCode(returnType);

            var returnValue = "";
            if (typeCode == TypeCode.Object && returnTypeName != "void")
                returnValue = $"result as {returnTypeName}";
            else if (returnTypeName == "void")
                returnValue = string.Empty;
            else
                returnValue = $"({returnTypeName})result";

            return string.Format(ProxyFormats.ProxyPropertyFormat, propertyName, returnTypeName, returnValue);
        }

        private string WriteMethod(MethodInfo mockableMethod)
        {
            var returnType = mockableMethod.ReturnType;
            var returnTypeName = returnType == typeof(void) ? "void" : returnType.Name;
            var methodName = mockableMethod.Name;
            var typeCode = Type.GetTypeCode(mockableMethod.ReturnType);

            var returnValue = "";
            if (typeCode == TypeCode.Object && returnTypeName != "void")
                returnValue = $"result as {returnTypeName}";
            else if (returnTypeName == "void")
                returnValue = string.Empty;
            else
                returnValue = $"({returnTypeName})result";

            var parameters = string.Join(", ",
                mockableMethod.GetParameters()
                .Select(x => $"{x.ParameterType.Name} {x.Name}"));

            return string.Format(ProxyFormats.ProxyMethodFormat, methodName, returnTypeName, parameters, returnValue);
        }

        private IEnumerable<MethodInfo> GetMockableMethods()
            => _type.GetMethods().Where(x => (x.IsAbstract || x.IsVirtual) && !x.IsSpecialName);
        private IEnumerable<PropertyInfo> GetMockableProperties()
            => _type.GetProperties();

        private string GetTypeFullName(Type type)
        {
            var nameBuilder = type.Name;
            var current = type;
            while (current.DeclaringType != null)
            {
                nameBuilder = $"{current.DeclaringType.Name}.{nameBuilder}";
                current = current.DeclaringType;
            }
            return nameBuilder;
        }

        #endregion
    }
}

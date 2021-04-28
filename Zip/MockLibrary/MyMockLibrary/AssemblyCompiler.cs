using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyMockLibrary
{
	internal class AssemblyCompiler
	{
		private readonly IList<string> _references;

		public AssemblyCompiler()
		{
			_references = new List<string>();
			UseReference<object>();
			UseReference<AssemblyCompiler>();
		}

		public AssemblyCompiler UseReference<T>()
		{
			_references.Add(typeof(T).GetTypeInfo().Assembly.Location);
			return this;
		}

		public Assembly Compile(string sourceCode, string assemblyName)
		{
			var tree = CSharpSyntaxTree.ParseText(sourceCode);
			var compilation = CreateCompileOptions(tree, assemblyName);
			return CreateAssembly(compilation);
		}

		private static Assembly CreateAssembly(Compilation compilation)
		{
			var ms = new MemoryStream();
			var result = compilation.Emit(ms);

			if (!result.Success)
				throw new Exception(message: "Compilation failed");

			return Assembly.Load(ms.ToArray());
		}

		private CSharpCompilation CreateCompileOptions(SyntaxTree tree, string assemblyName)
			=> CSharpCompilation.Create(
				assemblyName,
				syntaxTrees: new[] { tree },
				_references.Distinct().Select(x => MetadataReference.CreateFromFile(x)).ToList(),
				new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
	}
}

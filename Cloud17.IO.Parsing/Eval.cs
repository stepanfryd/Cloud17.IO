using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Cloud17.IO.Parsing
{
	/// <summary>
	/// Class provides eval functions
	/// </summary>
	public static class Eval
	{
		#region Fields and constants

		/// <summary>
		///   Contains code assemblies created by pre processor scripts
		/// </summary>
		public static Dictionary<string, Assembly> CodeAssemblies = new Dictionary<string, Assembly>();

		#endregion

		private static Assembly GetAssembly(string preprocessCode)
		{
			var codeString = GetCodeString(preprocessCode);
			var codeHash = GetCodeHashString(GetCodeHash(codeString));

			if (CodeAssemblies.ContainsKey(codeHash))
			{
				return CodeAssemblies[codeHash];
			}
			var provider = CodeDomProvider.CreateProvider("CSharp");
			var compilerParameters = new CompilerParameters();
			compilerParameters.ReferencedAssemblies.Add("system.dll");
			compilerParameters.CompilerOptions = "/t:library";
			compilerParameters.GenerateInMemory = true;

			var compilerResult = provider.CompileAssemblyFromSource(compilerParameters, codeString);
			if (compilerResult.Errors.Count > 0)
			{
				return null;
			}

			var assmb = compilerResult.CompiledAssembly;
			CodeAssemblies.Add(codeHash, assmb);

			return assmb;
		}

		private static string GetCodeString(string preprocessCode)
		{
			var sb = new StringBuilder("");
			sb.Append("using System;");
			sb.Append("using System.Globalization;");
			sb.Append("public class CodeEvaler{");
			sb.Append("public object Eval(){");
			sb.Append("return " + preprocessCode + ";");
			sb.Append("}");
			sb.Append("}");
			sb.Replace("\n", " ");
			sb.Replace("\r", " ");
			sb.Replace("  ", " ");
			return sb.ToString();
		}

		private static byte[] GetCodeHash(string preprocessCode)
		{
			var hash = SHA256.Create();
			return hash.ComputeHash(Encoding.UTF8.GetBytes(preprocessCode));
		}

		private static string GetCodeHashString(byte[] codeHash)
		{
			var sb = new StringBuilder();
			foreach (var b in codeHash)
			{
				sb.Append(b.ToString("x2"));
			}

			return sb.ToString();
		}

		/// <summary>
		/// Evaluate provides code string
		/// </summary>
		/// <param name="preprocessCode">Code to process</param>
		/// <returns></returns>
		public static object Invoke(string preprocessCode)
		{
			var assmb = GetAssembly(preprocessCode);
			var obj = assmb.CreateInstance("CodeEvaler");
			if (obj == null) return null;

			var t = obj.GetType();
			var mi = t.GetMethod("Eval");
			return mi.Invoke(obj, null);
		}
	}
}
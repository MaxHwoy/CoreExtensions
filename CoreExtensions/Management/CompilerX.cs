using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.CSharp;
using System.CodeDom.Compiler;



namespace CoreExtensions.Management
{
	public static class CompilerX
	{
		public static bool RunCode(string filename, out string[] errors)
		{
            errors = null;
            if (!File.Exists(filename)) return false;
            var csc = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v4.0" } });
            var parameters = new CompilerParameters(new string[] { "mscorlib.dll", "System.Core.dll" })
            {
                GenerateExecutable = false,
                GenerateInMemory = true,
                TreatWarningsAsErrors = false,
            };
            var results = csc.CompileAssemblyFromFile(parameters, filename);
            if (results.Errors.HasErrors)
            {
                errors = new string[results.Errors.Count];
                for (int a1 = 0; a1 < results.Errors.Count; ++a1)
                    errors[a1] = results.Errors[a1].ErrorText;
                return false;
            }
            else return true;
        }

        public static bool RunCode(string filename, string[] dlls, out string[] errors)
        {
            errors = null;
            if (!File.Exists(filename)) return false;
            var csc = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v4.0" } });

            var libs = new string[dlls.Length + 2];
            libs[0] = "mscorlib.dll"; libs[1] = "System.Core.dll";
            for (int a1 = 0; a1 < dlls.Length; ++a1) libs[a1 + 2] = dlls[a1];

            var parameters = new CompilerParameters(libs)
            {
                GenerateExecutable = false,
                GenerateInMemory = true,
                TreatWarningsAsErrors = false,
            };
            var results = csc.CompileAssemblyFromSource(parameters, File.ReadAllText(filename));
            if (results.Errors.HasErrors)
            {
                errors = new string[results.Errors.Count];
                for (int a1 = 0; a1 < results.Errors.Count; ++a1)
                    errors[a1] = results.Errors[a1].ErrorText;
                return false;
            }
            else return true;
        }
    }
}

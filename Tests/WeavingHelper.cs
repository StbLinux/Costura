﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Fody;
#pragma warning disable 618

static class WeavingHelper
{
    public static TestResult CreateIsolatedAssemblyCopy(string assemblyPath, string config, IEnumerable<string> references, string assemblyName)
    {
        var weavingTask = new ModuleWeaver
        {
            Config = XElement.Parse(config),
            ReferenceCopyLocalPaths = references.Select(r => Path.Combine(CodeBaseLocation.CurrentDirectory, r)).ToList(),
        };
        return weavingTask.ExecuteTestRun(assemblyPath,
            assemblyName: assemblyName,
            ignoreCodes: new []{ "0x80131869" },
            runPeVerify:false);
    }
}
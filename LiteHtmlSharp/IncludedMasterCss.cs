﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LiteHtmlSharp
{
    public static class IncludedMasterCss
    {
        public static string CssString => _includedMasterCss.Value;

        static Lazy<string> _includedMasterCss = new Lazy<string>(GetMasterCssResource);

        static string GetMasterCssResource()
        {
            var assembly = typeof(IncludedMasterCss).GetTypeInfo().Assembly;
            var masterCssResourceName = assembly.GetName().Name + ".master.css";
            using (var reader = new StreamReader(assembly.GetManifestResourceStream(masterCssResourceName), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}

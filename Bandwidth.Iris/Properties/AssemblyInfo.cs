using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

[assembly: AssemblyTitle("Bandwidth.Iris")]
[assembly: AssemblyDescription(".NET SDK for use with the Iris API")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Bandwidth")]
[assembly: AssemblyProduct("Bandwidth.Iris")]
[assembly: AssemblyCopyright("Copyright © Bandwidth 2020")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguage("en")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]

[assembly: AssemblyVersion("2.1.2")]
[assembly: AssemblyFileVersion("2.1.2")]

#if DEBUG

[assembly: InternalsVisibleTo("Bandwidth.Iris.Tests")]
#endif
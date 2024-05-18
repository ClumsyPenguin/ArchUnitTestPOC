using System.Reflection;

namespace ArchTestPOC;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
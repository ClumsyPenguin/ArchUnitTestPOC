namespace ArchTestPOC.Attributes;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public class CacheAttribute : Attribute
{
    public CacheAttribute(int minutes)
    {
        
    }
}
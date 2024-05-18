namespace ArchTestPOC.Attributes;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public class SerializeTestAttribute : Attribute
{
    public SerializeTestAttribute(Type type)
    {
        
    }
}
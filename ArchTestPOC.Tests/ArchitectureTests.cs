using ArchTestPOC.Attributes;
using ArchTestPOC.Tests.Extensions;
using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ArchTestPOC.Tests;

public class ArchitectureTests
{
    private readonly Architecture _architecture = new ArchLoader().LoadAssembly(AssemblyReference.Assembly).Build();

    [Fact]
    public void EveryCachedTypeShouldHaveSerializeTest()
    {
        var methodsThatShouldHaveTests =
            MethodMembers()
                .That()
                .ArePublic().And()
                .AreNoConstructors()
                .And().HaveAttributeWithArguments(typeof(CacheAttribute), new object[] { 5 });
        
        var result = methodsThatShouldHaveTests
            .Should()
            .HaveSerializeTest();
        
        result.Check(_architecture);
    }
}
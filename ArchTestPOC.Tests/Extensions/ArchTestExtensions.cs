using System.Reflection;
using ArchTestPOC.Attributes;
using ArchTestPOC.Services;
using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Fluent.Conditions;
using ArchUnitNET.Fluent.Syntax.Elements.Members.MethodMembers;
using ArchUnitNET.Loader;
using Assembly = System.Reflection.Assembly;
using Attribute = System.Attribute;

namespace ArchTestPOC.Tests.Extensions;

public static class ArchTestExtensions
{
    private static readonly Architecture TestArchitecture = new ArchLoader().LoadAssembly(Assembly.GetExecutingAssembly()).Build();
    public static IArchRule HaveSerializeTest(this MethodMembersShould should)
    {
        var description = "Have unit tests " + should.Description;
        
        return should.FollowCustomCondition(new ArchitectureCondition<MethodMember>(Condition, description));

        IEnumerable<ConditionResult> Condition(IEnumerable<MethodMember> methods, Architecture architecture)
        {
            foreach (var method in methods)
            {
                var result = GetMethodHasUnitTestResult(method);
                if (result is null) continue;

                yield return result;
            }
        }
    }

    private static ConditionResult? GetMethodHasUnitTestResult(MethodMember method)
    {
        return method.ReturnType is null 
            ? null 
            : new ConditionResult(method, ReturnTypeHasTestMethod(method.ReturnType));
    }

    private static bool ReturnTypeHasTestMethod(IType returnType)
    {
        var methodsWithSerializeTestAttribute = TestArchitecture.MethodMembers
            .Where(m => m.Attributes.Any(a => a.Name == nameof(SerializeTestAttribute)))
            .ToList();

        return methodsWithSerializeTestAttribute
            .Select(method => method.AttributeInstances
                .Any(attr => attr.AttributeArguments
                    .Any(arg => arg.Value is TypeInstance<Class> underlyingType &&
                                underlyingType.Type.Name == returnType.Name)))
            .FirstOrDefault();
    }
}
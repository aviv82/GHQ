using AutoMapper;
using GHQ.Core.Mappings;
using System.Reflection;

namespace GHQ.Core.Extensions;

public static class ProfileExtensions
{
    public static int ApplyFromMappings(this Profile profile, Assembly assembly)
    {
        return profile.ApplyMappings(assembly, typeof(IMapFrom<>));
    }

    public static int ApplyToMappings(this Profile profile, Assembly assembly)
    {
        return profile.ApplyMappings(assembly, typeof(IMapTo<>));
    }

    private static int ApplyMappings(this Profile profile, Assembly assembly, Type mapType)
    {
        var typesWithInterfaces = assembly.GetTypes()
            .Select(x => new TypeWithInterfaceTypes
            {
                Type = x,
                InterfaceTypes = x.GetInterfaces().Where(y => y.IsGenericType && y.GetGenericTypeDefinition() == mapType).ToList()
            })
            .Where(x => x.InterfaceTypes.Any())
            .ToList();
        var count = 0;

        foreach (var typeWithInterfaces in typesWithInterfaces)
        {
            var instance = Activator.CreateInstance(typeWithInterfaces.Type, null);

            foreach (var interfaceType in typeWithInterfaces.InterfaceTypes)
            {
                var methodInfo = mapType.MakeGenericType(interfaceType.GetGenericArguments()).GetMethod("Mapping");
                if (methodInfo == null) continue;

                methodInfo.Invoke(instance, new object[] { profile });
                count++;
            }
        }

        return count;
    }

    private class TypeWithInterfaceTypes
    {
        public Type Type { get; set; } = default!;
        public IEnumerable<Type> InterfaceTypes { get; set; } = default!;
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Javalike_CDI;

using System.ComponentModel;
using System.Reflection;

public enum InjectionScope {
    Singleton,
    Scoped,
    Transient
}

public static class InjectionUtils {
    private static readonly IEnumerable<Type> Types = Assembly.GetExecutingAssembly().GetTypes();

    private static Action<Type> Adder(WebApplicationBuilder builder, InjectionScope scope, string? name) {
        return scope switch {
            InjectionScope.Singleton => (type) => {
                if (name == null) builder.Services.AddSingleton(type);
                else builder.Services.AddKeyedSingleton(type, serviceKey: name);
            },
            InjectionScope.Scoped => (type) => {
                if (name == null) builder.Services.AddScoped(type);
                else builder.Services.AddKeyedScoped(type, serviceKey: name);
            },
            InjectionScope.Transient => (type) => {
                if (name == null) builder.Services.AddTransient(type);
                else builder.Services.AddKeyedTransient(type, serviceKey: name);
            },
            _ => throw new InvalidEnumArgumentException()
        };
    }
    public static void Inject(WebApplicationBuilder builder, Type attributeType) {
        foreach (var type in Types.Where(type => type.GetCustomAttribute(attributeType) != null)) {
            var injectionAttribute = (MetaAttribute)Attribute.GetCustomAttribute(type, attributeType)!;
            Adder(builder, injectionAttribute.Scope, injectionAttribute.Name)(type);
        }
    }
}

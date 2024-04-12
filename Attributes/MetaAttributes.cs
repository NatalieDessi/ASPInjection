namespace Javalike_CDI.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class Scoped(string? name = null) : MetaAttribute(InjectionScope.Scoped, name);

[AttributeUsage(AttributeTargets.Class)]
public class Singleton(string? name = null) : MetaAttribute(InjectionScope.Singleton, name);

[AttributeUsage(AttributeTargets.Class)]
public class Transient(string? name = null) : MetaAttribute(InjectionScope.Transient, name);

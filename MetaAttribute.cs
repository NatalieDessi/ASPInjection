namespace Javalike_CDI;

public class MetaAttribute : Attribute {
    public InjectionScope Scope;
    public string? Name;

    protected MetaAttribute(InjectionScope scope = InjectionScope.Singleton, string? name = null) {
        Scope = scope;
        Name = name;
    }
}

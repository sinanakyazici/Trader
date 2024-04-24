using System.Reflection;

namespace BuildingBlocks.Core.Domain;

public abstract class Enumeration : Entity<int>
{
    public string Name { get; }

    protected Enumeration(int id, string name) => (Id, Name) = (id, name);

    public override string ToString() => Name;

    public static IEnumerable<T> GetAll<T>() where T : Enumeration => typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly).Select(f => f.GetValue(null)).Cast<T>();

    public static T Parse<T>(int id) where T : Enumeration
    {
        return GetAll<T>().First(x => x.Id == id);
    }
}
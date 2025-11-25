namespace Financeiro.Shared;

public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
            return false;

        var other = (ValueObject)obj;

        using var thisComponents = GetEqualityComponents().GetEnumerator();
        using var otherComponents = other.GetEqualityComponents().GetEnumerator();

        while (thisComponents.MoveNext() && otherComponents.MoveNext())
        {
            if (!Equals(thisComponents.Current, otherComponents.Current))
                return false;
        }

        return !thisComponents.MoveNext() && !otherComponents.MoveNext();
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(17, (current, obj) => current * 23 + (obj?.GetHashCode() ?? 0));
    }
}

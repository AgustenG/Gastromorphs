using System;

public abstract class Attribute : IComparable<Attribute>
{
    protected int id;
    protected string name;
    protected string description;
    protected string iconUri;


    public Attribute(int id, string name, string description, string iconUri)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.iconUri = iconUri;
    }
    public int CompareTo(Attribute other)
    {
        return this.id.CompareTo(other.id);
    }
}

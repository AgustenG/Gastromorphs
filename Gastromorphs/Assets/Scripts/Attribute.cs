using System;
using Unity.VisualScripting.ReorderableList;
public abstract class Attribute : IComparable<Attribute>
{
    protected int id;
    protected string name;
    protected string description;
    protected string iconUri;

    public int ID
    {
        get { return id; }
    }
    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }

    public string IconUri
    {
        get { return iconUri; }
    }

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

using UnityEngine;

public abstract class Attribute : ScriptableObject
{
    protected int id;
    protected string name;
    protected string description;
    protected string iconURI;


    public Attribute(int id, string name, string description, string iconURI)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.iconURI = iconURI;
    }

}

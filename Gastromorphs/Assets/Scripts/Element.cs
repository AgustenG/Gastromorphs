public class Element : Attribute
{
    public Element(int id, string name, string description, string iconURI) : base(id, name, description, iconURI)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.iconURI = iconURI;
    }


}

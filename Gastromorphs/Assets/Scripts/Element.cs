public class Element : Attribute
{
    public Element(int id, string name, string description, string IconURI) //: base(id, name, description, IconURI)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.IconURI = IconURI;
    }
}

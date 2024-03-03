[System.Serializable]
public class Type : Attribute
{
    public Type(int id, string name, string description, string iconUri) : base(id, name, description, iconUri)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.iconUri = iconUri;
    }
}

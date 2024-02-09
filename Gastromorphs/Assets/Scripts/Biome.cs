public class Biome : Attribute
{
    public Biome(int id, string name, string description, string iconUri) : base(id, name, description, iconUri)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.iconUri = iconUri;
    }
}

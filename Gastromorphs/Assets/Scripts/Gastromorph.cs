using System;
using System.Collections.Generic;


[System.Serializable]
public class Gastromorph : IComparable<Gastromorph>
{
    public int Gastromorph_id { get; }
    public string Name { get; }
    public string Description { get; }
    public List<Type> Type { get; }
    public List<Biome> Biomes { get; }
    public List<Flavour> Flavours { get; }
    public string ModelUri { get; }
    public string IconUri { get; }


    public Gastromorph(int gastromorph_id, string name, string description, List<Biome> biomes, List<Type> elements, List<Flavour> flavours)
    {
        this.Gastromorph_id = gastromorph_id;
        this.Name = name;
        this.Description = description;
        this.Biomes = biomes;
        this.Type = new List<Type>(elements);
        this.Flavours = new List<Flavour>(flavours);
        IconUri = name + ".png";
        ModelUri = name + ".fbx";
    }

    public int CompareTo(Gastromorph other)
    {
       return this.Gastromorph_id.CompareTo(other.Gastromorph_id);
    }
}

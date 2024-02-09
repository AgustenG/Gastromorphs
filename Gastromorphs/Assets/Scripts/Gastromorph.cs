using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;

public class Gastromorph : IComparable<Gastromorph>
{
    public int Gastromorph_id { get; }
    public string Name { get; }
    public string Description { get; }
    public List<Element> Elements { get; }
    public List<Biome> Biomes { get; }
    public List<Flavour> Flavours { get; }
    public List<string> ModelUri { get; }
    public string IconUri { get; }

    public Gastromorph(int gastromorph_id, string name)
    {
        Gastromorph_id = gastromorph_id;
        Name = name;
        IconUri = name + ".png";
    }

    public Gastromorph(int gastromorph_id, string name, string description, List<string> animUri)
    {
        this.Gastromorph_id = gastromorph_id;
        this.Name = name;
        this.Description = description;
        this.ModelUri = animUri;
        IconUri = name + ".png";
    }
    public Gastromorph(Gastromorph gastromorph, List<Element> elements, List<Biome> biomes, List<Flavour> flavours)
    {
        Gastromorph_id = gastromorph.Gastromorph_id;
        Name = gastromorph.Name;
        Description = gastromorph.Description;
        ModelUri = new List<string>(gastromorph.ModelUri);
        this.Biomes = new List<Biome>(biomes);
        this.Elements = new List<Element>(elements);
        this.Flavours = new List<Flavour>(flavours);
        IconUri = gastromorph.Name + ".png";
    }

    public Gastromorph(int gastromorph_id, string name, string description, List<Biome> biomes, List<Element> elements, List<Flavour> flavours, List<string> animUri)
    {
        this.Gastromorph_id = gastromorph_id;
        this.Name = name;
        this.Description = description;
        this.Biomes = new List<Biome>(biomes);
        this.Elements = new List<Element>(elements);
        this.Flavours = new List<Flavour>(flavours);
        this.ModelUri = new List<string>(animUri);
        IconUri = name + ".png";
    }

    public int CompareTo(Gastromorph other)
    {
       return this.Gastromorph_id.CompareTo(other.Gastromorph_id);
    }
}

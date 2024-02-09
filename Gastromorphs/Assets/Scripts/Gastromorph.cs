using System.Collections.Generic;

public class Gastromorph 
{
    private int gastromorph_id;
    private string name;
    private string description;
    private List<Biome> biomes;
    private List<Element> elements;
    private List<Flavour> flavours;
    private string IconURI;
    private List<string> AnimURI;

    public Gastromorph(int gastromorph_id , string name, string description, List<Biome> biomes, List<Element> elements, List<Flavour> flavours, string IconURI, List<string> AnimURI)
    {
        this.gastromorph_id = gastromorph_id;
        this.name = name;
        this.description = description;
        this.biomes = biomes;
        this.elements = elements;
        this.flavours = flavours;
        this.IconURI = IconURI;
        this.AnimURI = AnimURI;
    }
}

using UnityEngine;

public abstract class Attribute : ScriptableObject
{
    protected int id { get; set; }
    protected string description { get; set; }
    protected string IconURI { get; set; }

    protected virtual void GetIcon()
    { 
    
    }
}

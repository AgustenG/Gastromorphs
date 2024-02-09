using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataObject
{
    int id { get; set; }
    string name { get; set; }
    string description { get; set; }
    string IconURI { get; set; }

    public abstract void GetIcon();
}

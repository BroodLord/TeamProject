using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OreAbstractClass : ItemBase
{
    public string XMLName;
    public Vector3Int ID;
    public Vector3 pos;

    public abstract void DestoryOre();
}

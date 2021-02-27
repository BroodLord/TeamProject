using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : OreAbstractClass
{
    void Start()
    {
        XMLName = GetName();
    }

    public override void DestoryOre()
    {
        //this.GetComponent(OreAbstractClass)
    }
}

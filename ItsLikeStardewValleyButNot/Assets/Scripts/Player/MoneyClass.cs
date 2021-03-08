using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is pretty easy to understand so it doesn't need commenting

public class MoneyClass : MonoBehaviour
{
    private float Money;
    public float StartingGold;
    // Start is called before the first frame update
    void Start()
    {
        Money = StartingGold;
    }

    public float GetMoney()
    {
        return Money;
    }

    public void SetMoney(float Amount)
    {
        Money = Amount;
    }

    public void AddAmount(float Amount)
    {
        Money += Amount;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

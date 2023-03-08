using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaModel 
{

    public int SeaStep;
    private int Money;
    public void SetStep(int money)
    {
        this.Money += money;
        this.SeaStep = this.Money / 10;
    }
}

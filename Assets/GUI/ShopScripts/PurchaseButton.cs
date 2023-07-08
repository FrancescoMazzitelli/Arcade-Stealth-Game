using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseButton
{
    private int index;

    public PurchaseButton(int index)
    {
        this.index = index;
    }

    public int Index
    {
        get { return index; }
        set { index = value; }
    }
}

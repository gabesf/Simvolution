using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions
{
    public float Heaviside(float c, float x)
    {
        if(x < c)
        {
            return 0;
        }

        return 1;
    }

    public float Identity(float x)
    {
        return x;
    }

    public float RectifiedLinearUnit(float x, float c)
    {
        if(x < c)
        {
            return 0;
        }

        return x;
    }
}

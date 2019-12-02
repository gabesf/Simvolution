using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticBuildingInstructions
{
    private List<byte> buildingInstructions;

    public GeneticBuildingInstructions()
    {
        buildingInstructions = new List<byte>();
    }

    public List<byte> GetBuildingInstructions()
    {
        return buildingInstructions;
    }

}

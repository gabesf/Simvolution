using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneticCodes;

public class GeneticCode
{
    private List<byte> geneticCodeList;
    

    private float noise = 0.1f;
    private float mutationChance = 0.3f;
    private float noiseChance = 0.5f;
    
    public float GetNoiseChance()
    {
        return noiseChance;
    }

    public float GetNoise()
    {
        return noise;
    }

    public float GetMutationChance()
    {
        return mutationChance;  
    }

    public GeneticCode()
    {
        //Debug.Log("Im being called");
        geneticCodeList = new List<byte>();


        //AddFirstSphereCode(geneticCodeList);
        AddFirstSphereCode(geneticCodeList);
        //AddSphereToCoordinates(geneticCodeList, new Vector3(90, 0, 0));
        //AddPlaneToCoordinates(geneticCodeList, new Vector3(0, 0, 0));
        //AddSphereToCoordinates(geneticCodeList, new Vector3(-90f, 0, 0));
        //AddPlaneToCoordinates(geneticCodeList, new Vector3(0, 0, 0));
        //AddFirstSphereCode(geneticCodeList);
        //AddFirstSphereCode(geneticCodeList);
        //AddFirstSphereCode(geneticCodeList);
        //AddSphereToCoordinates(geneticCodeList, new Vector3(90, 0, 0));
        //AddPlaneToCoordinates(geneticCodeList, new Vector3(0, 0, 0));
        //AddSphereToCoordinates(geneticCodeList, new Vector3(-90f, 0, 0));
        //AddPlaneToCoordinates(geneticCodeList, new Vector3(0, 0, 0));


        //AddBoxToEndCode(geneticCodeList);
        //AddSphereToCoordinates(geneticCodeList, new Vector3(90, 0, 0));
        //AddPlaneToCoordinates(geneticCodeList, new Vector3(0, 0, 0));

        //Antagonists
        //AddSphereToCoordinates(geneticCodeList, new Vector3(90f, 0, 0));
        //AddPlaneToCoordinates(geneticCodeList, new Vector3(0, 0, 0));
        //AddSphereToCoordinates(geneticCodeList, new Vector3(-90f, 0, 0));
        //AddPlaneToCoordinates(geneticCodeList, new Vector3(0, 180, 0));

        //Single Ortogonal
        //AddSphereToCoordinates(geneticCodeList, new Vector3(90, 0, 0));
        //AddPlaneToCoordinates(geneticCodeList, new Vector3(0, 0, 0));

        //Double Alligned
        //AddSphereToCoordinates(geneticCodeList, new Vector3(90, 0, 0));
        //AddPlaneToCoordinates(geneticCodeList, new Vector3(0, 0, 0));
        //AddSphereToCoordinates(geneticCodeList, new Vector3(-90f, 0, 0));
        //AddPlaneToCoordinates(geneticCodeList, new Vector3(0, 0, 0));

        //AddSphereToCoordinates(geneticCodeList, new Vector3(-90f, 0, 0));
        //AddPlaneToCoordinates(geneticCodeList, new Vector3(0, 0, 0));



        /* Antagonists
         * 
         * AddSphereToCoordinates(geneticCodeList, new Vector3(90f, 0, 0));
         * AddPlaneToCoordinates(geneticCodeList, new Vector3(0, 0, 0));
         * AddSphereToCoordinates(geneticCodeList, new Vector3(-90f, 0, 0));
         * AddPlaneToCoordinates(geneticCodeList, new Vector3(0, 180, 0));
         * 
         * Double Alligned
         * AddSphereToCoordinates(geneticCodeList, new Vector3(90, 0, 0));
         * AddPlaneToCoordinates(geneticCodeList, new Vector3(0, 0, 0));
         * AddSphereToCoordinates(geneticCodeList, new Vector3(-90f, 0, 0));
         * AddPlaneToCoordinates(geneticCodeList, new Vector3(0, 0, 0));
         * 
         * 
         * Single Linear
         * AddSphereToCoordinates(geneticCodeList, new Vector3(0, 0, 0));
         * AddPlaneToCoordinates(geneticCodeList, new Vector3(0, 0, 0));
         * 
         * Single Ortogonal
         * AddSphereToCoordinates(geneticCodeList, new Vector3(90, 0, 0));
         * AddPlaneToCoordinates(geneticCodeList, new Vector3(0, 0, 0));
         */

        //AddSphereToEndCode(geneticCodeList);


    }

    private void AddSphereToCoordinates(List<byte> geneticCode, Vector3 coordinates)
    {
        geneticCode.Add((byte)PhysicalMutation.AddStructure); //add structure
        geneticCode.Add((byte)ClassOfStructure.ThreeDim); //add volume
        geneticCode.Add((byte)TypeOfStructure.Sphere); //add sphere
        geneticCode.Add(1); //scale

        geneticCode.Add((byte)(coordinates.x/1.41f)); //x rot
        geneticCode.Add((byte)(coordinates.y/1.41f)); //y rot
        geneticCode.Add((byte)(coordinates.z/1.41f)); //z rot

        geneticCode.Add((byte)AttachClass.ToCenter);
    }

    private void AddPlaneToCoordinates(List<byte> geneticCode, Vector3 coordinates)
    {
        geneticCode.Add((byte)PhysicalMutation.AddStructure); //add structure
        geneticCode.Add((byte)ClassOfStructure.TwoDim); //add volume
        geneticCode.Add((byte)4); //scale
        //geneticCode.Add((byte)Random.Range(0, 255)); //x rot
        //geneticCode.Add((byte)Random.Range(0, 255)); //y rot
        //geneticCode.Add((byte)Random.Range(0, 255)); //z rot
        geneticCode.Add((byte)(coordinates.x / 1.41f)); //x rot
        geneticCode.Add((byte)(coordinates.y / 1.41f)); //y rot
        geneticCode.Add((byte)(coordinates.z / 1.41f)); //z rot
        geneticCode.Add((byte)AttachClass.ToLast);
    }

    private void AddPlaneCode(List<byte> geneticCode)
    {
        geneticCode.Add((byte)PhysicalMutation.AddStructure); //add structure
        geneticCode.Add((byte)ClassOfStructure.TwoDim); //add volume
        geneticCode.Add((byte)4); //scale
        geneticCode.Add((byte)Random.Range(0, 255)); //x rot
        geneticCode.Add((byte)Random.Range(0, 255)); //y rot
        geneticCode.Add((byte)Random.Range(0, 255)); //z rot
        //geneticCode.Add((byte)0); //x rot
        //geneticCode.Add((byte)0); //y rot
        //geneticCode.Add((byte)0); //z rot
        geneticCode.Add((byte)AttachClass.ToLast);
    }

    public void SetGeneticCodeList(List<byte> _geneticCodeList)
    {
        geneticCodeList = _geneticCodeList;
    }

    private void AddBoxToEndCode(List<byte> geneticCode)
    {
        geneticCode.Add((byte)PhysicalMutation.AddStructure); //add structure
        geneticCode.Add((byte)ClassOfStructure.ThreeDim); //add volume
        geneticCode.Add((byte)TypeOfStructure.Cube); //add sphere
        geneticCode.Add(1); //scale

        geneticCode.Add((byte)Random.Range(0, 255)); //x rot
        geneticCode.Add((byte)Random.Range(0, 255)); //y rot
        geneticCode.Add((byte)Random.Range(0, 255)); //z rot

        geneticCode.Add((byte)AttachClass.ToLast);
    }

    private void AddFirstBoxCode(List<byte> geneticCode)
    {
        geneticCode.Add((byte)PhysicalMutation.AddStructure); //add structure
        geneticCode.Add((byte)ClassOfStructure.ThreeDim); //add volume
        geneticCode.Add((byte)TypeOfStructure.Cube); //add sphere
        geneticCode.Add(1); //scale

        geneticCode.Add((byte)0); //x rot
        geneticCode.Add((byte)0); //y rot
        geneticCode.Add((byte)0); //z rot

        geneticCode.Add((byte)AttachClass.ToCenter);
    }

    private void AddFirstSphereCode(List<byte> geneticCode)
    {
        geneticCode.Add((byte)PhysicalMutation.AddStructure); //add structure
        geneticCode.Add((byte)ClassOfStructure.ThreeDim); //add volume
        geneticCode.Add((byte)TypeOfStructure.Sphere); //add sphere
        geneticCode.Add(1); //scale
        
        geneticCode.Add((byte)Random.Range(0,255)); //x rot
        geneticCode.Add((byte)Random.Range(0, 255)); //y rot
        geneticCode.Add((byte)Random.Range(0, 255)); //z rot

        geneticCode.Add((byte)AttachClass.ToCenter);
    }

    private void AddSphereToEndCode(List<byte> geneticCode)
    {
        geneticCode.Add((byte)PhysicalMutation.AddStructure); //add structure
        geneticCode.Add((byte)ClassOfStructure.ThreeDim); //add volume
        geneticCode.Add((byte)TypeOfStructure.Sphere); //add sphere
        geneticCode.Add(1); //scale

        geneticCode.Add((byte)Random.Range(0, 255)); //x rot
        geneticCode.Add((byte)Random.Range(0, 255)); //y rot
        geneticCode.Add((byte)Random.Range(0, 255)); //z rot

        geneticCode.Add((byte)AttachClass.ToLast);
    }



    public List<byte> GetGeneticCodeList()
    {
        return geneticCodeList;
    }


    
}

namespace GeneticCodes
{
    enum AttachClass: byte
    {
        ToCenter,
        //ToOpen,
        //ToRandom,
        ToLast
    }
    enum PhysicalMutation : byte
    {
        AddStructure,
        //AddSensor,
        //AddNerve,
        //StartABranch,
        //EndABranch
    }

    enum ClassOfStructure : byte
    {
        //OneDim,
        TwoDim,
        ThreeDim
    }

    enum TypeOfStructure : byte
    {
        Sphere,
        Cube,
    }
}
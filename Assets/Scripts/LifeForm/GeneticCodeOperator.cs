using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneticCodes;
public class GeneticCodeOperator
{
    GeneticCodeReader geneticCodeReader;
    GeneticCodeReplicator geneticCodeReplicator;
    MeshGenerator meshGenerator;
    bool debbuging = false;

    public GeneticCodeOperator()
    {
        meshGenerator = new MeshGenerator();
    }

    public void ProcessGeneticCode(GameObject lifeForm, ref List<byte> geneticCode, List<byte> buildingInstructions)
    {

        bool validForm = true;
        Debug.Log("Processing GC " + geneticCode.Count);
        for(int j = 0; j < geneticCode.Count; j++)
        {
            //Debug.Log($"[{j}] = {geneticCode[j]}");
        }
        int i = 0;
        if (debbuging) Debug.Log("L1:");
        do
        {

            switch (geneticCode[i])

            {

                case (byte)GeneticCodes.PhysicalMutation.AddStructure:

                    if (debbuging) Debug.Log("L1.");
                    i++;
                    i = AddStructure(lifeForm, i, ref geneticCode, buildingInstructions);
                    break;



                    default:
                    if (debbuging) Debug.Log("L1?");
                    System.Array values = GeneticCodes.PhysicalMutation.GetValues(typeof(GeneticCodes.PhysicalMutation));
                    System.Random random = new System.Random();
                    byte randomByte = (byte)values.GetValue(random.Next(values.Length));
                    geneticCode[i] = randomByte;

                    break;
            }
        } while (i < geneticCode.Count);



        //Debug.Log("Processed Genetic Code " + geneticCode.Count);

        for (int j = 0; j < geneticCode.Count; j++)
        {
            //Debug.Log($"[{j}] = {geneticCode[j]}");
        }
    }
    
    private int AddStructure(GameObject lifeForm, int i, ref List<byte> geneticCode, List<byte> buildingInstructions)
    {
        bool processed = false;
        
        if(i>=geneticCode.Count)
        {
            if (debbuging) Debug.Log("L2+");
            geneticCode.Add((byte)Random.Range(0,255));
        }
        if (debbuging) Debug.Log("L2:");

        do
        {
            switch (geneticCode[i])
            {
                

                case (byte)ClassOfStructure.TwoDim:
                    if (debbuging) Debug.Log($"L2-{ClassOfStructure.TwoDim} i = {i}");
                    i++;
                    i = AddPlane(lifeForm, i, ref geneticCode, buildingInstructions);
                    processed = true;
                    //add plane
                    break;

                case (byte)ClassOfStructure.ThreeDim:
                    if (debbuging) Debug.Log($"L2-{ClassOfStructure.ThreeDim} i = {i}");
                    //Debug.Log($"i = {i} = {geneticCode[i]} - ThreeDim");
                    i++;
                    i = AddVolume(lifeForm, i, ref geneticCode, buildingInstructions);
                    processed = true;
                    break;


                default:


                    

                    System.Array values = GeneticCodes.ClassOfStructure.GetValues(typeof(GeneticCodes.ClassOfStructure));
                    System.Random random = new System.Random();
                    byte randomByte = (byte)values.GetValue(random.Next(values.Length));
                    //randomByte = (byte)GeneticCodes.ClassOfStructure.TwoDim;
                    geneticCode[i] = randomByte;

                    
                    
                    
                    break;
            }
        } while (processed == false);
        
        return i;
    }

    




private int AddPlane(GameObject lifeForm, int i, ref List<byte> geneticCode, List<byte> buildingInstructions)
    {
       
        if (debbuging) Debug.Log($"The difference between i{i} + 6 = {i + 6} and geneticCode.Count ({geneticCode.Count}) is {i + 6 - geneticCode.Count}");
        if (i+6 > geneticCode.Count)
        {
           if (debbuging) Debug.Log($"L3+ (Size Before) = {geneticCode.Count}");
           for(int j = 0; j < i + 6 - geneticCode.Count + 3; j++) { geneticCode.Add((byte)Random.Range(0, 255)); }
            if (debbuging) Debug.Log($"L3+ (Size After) = {geneticCode.Count}");
        }

        if (debbuging) Debug.Log($"L3: (Size = {geneticCode.Count}");
 
        GameObject plane = new GameObject("Plane");
        Vector3 dimensions = new Vector3(0.5f, 0.01f, 1f);
    
        meshGenerator.AddTwoDimMesh(plane, Vector3.zero, dimensions);
        

        

        //Debug.Log($"{i} = {geneticCode[i]} - Scale");
        float scale = (float)geneticCode[i];
        if (scale > 1) scale = 1;
        if (debbuging) Debug.Log("L3...1");
        //plane.transform.localScale = new Vector3(1, 1, scale);


        //Debug.Log("Added Scale");
        i++;
        float xRot = geneticCode[i++] * 1.41f;
        if (debbuging) Debug.Log("L3...2");
        float yRot = geneticCode[i++] * 1.41f;
        if (debbuging) Debug.Log("L3...3");
        float zRot = geneticCode[i++] * 1.41f;
        if (debbuging) Debug.Log("L3...4");

        //Debug.Log("Added Rotations");

        List<GameObject> parts = lifeForm.GetComponent<LifeFormExec>().GetParts();

        
        //SphereCollider sc = sphere.AddComponent(typeof(SphereCollider)) as SphereCollider;

        //Debug.Log($"{i} = {geneticCode[i]}");
        if (debbuging) Debug.Log($"L3... (Size = {geneticCode.Count} and i = {i}");
        bool processed = false;
        do
        {
            switch (geneticCode[i])
            {
                case (byte)AttachClass.ToCenter:
                    if (debbuging) Debug.Log("L3-" + AttachClass.ToCenter);

                    if(parts.Count == 0)
                    {
                        GameObject.Destroy(plane);
                        GameObject.Destroy(lifeForm);
                    }
                    
                    Debug.Log("should be here");
                    i++;
                    processed = true;
                    break;
                case (byte)AttachClass.ToLast:
                    
                    if (debbuging) Debug.Log("L3-" + AttachClass.ToLast);

                    if(parts.Count == 0)
                    {
                        
                        geneticCode[i] = (byte)GeneticCodes.AttachClass.ToCenter;
                        break;
                    }
                    i++;

                    plane.transform.parent = parts[parts.Count - 1].transform;

                    plane.transform.rotation = parts[parts.Count - 1].transform.rotation;

                    //plane.transform.rotation = Quaternion.Euler(xRot, yRot, 0);
                    plane.name = $"PlaneCell{lifeForm.GetComponent<LifeFormExec>().GetParts().Count}";
                    plane.GetComponent<Renderer>().enabled = false;
                    plane.tag = "2DStruct";
                    PartExec partExec = plane.AddComponent<PartExec>();
                    GameObject.Destroy(plane.GetComponent<SphereCollider>());

                    partExec.SetPartArea(dimensions.x * dimensions.z / 2);
                    plane.transform.localPosition = new Vector3(0, 0, 0);
                    plane.transform.rotation = Quaternion.Euler(xRot, yRot, 0);

                    partExec.SetIsLocatedAtOrigin(true);
                    //partExec.SetLocalPositionStored(plane.transform.localPosition);



                    lifeForm.GetComponent<LifeFormExec>().GetParts().Add(plane);
                    //attach to last branch

                    processed = true;
                    Debug.Log("Exited");
                    break;

                default:
                    System.Array values = GeneticCodes.AttachClass.GetValues(typeof(GeneticCodes.AttachClass));
                    System.Random random = new System.Random();
                    byte randomByte = (byte)values.GetValue(random.Next(values.Length));
                    geneticCode[i] = randomByte;
                    geneticCode[i] = (byte)GeneticCodes.AttachClass.ToLast;
                    //attach to random branch and epigenetics
                    break;
            }
        } while (processed == false);

        


        //i++;
        return i;
    }

    private int AddVolume(GameObject lifeForm, int i, ref List<byte> geneticCode, List<byte> buildingInstructions)
    {
        //Debug.Log("I got here!");
        List<GameObject> parts = lifeForm.GetComponent<LifeFormExec>().GetParts();
        //Debug.Log($"{i} = {geneticCode[i]} - Type Of Sctructure");
        if (debbuging) Debug.Log("P1");
        if (i >= geneticCode.Count)
        {
            if (debbuging) Debug.Log("P2");
            geneticCode.Add((byte)Random.Range(0, 3));
        }
        if (debbuging) Debug.Log("P3");
        GameObject newStructure;
        
        bool _processed = false;
        do
        {
            switch (geneticCode[i])
            {
                case (byte)TypeOfStructure.Sphere:
                    if (debbuging) Debug.Log("P4");
                    newStructure = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    newStructure.name = $"SphereCell{parts.Count}";
                    _processed = true;
                    break;

                case (byte)TypeOfStructure.Cube:
                    if (debbuging) Debug.Log("P5");
                    newStructure = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    newStructure.name = $"CubeCell{parts.Count}";
                    _processed = true;
                    break;

                default:
                    if (debbuging) Debug.Log("P6");
                    System.Array values = GeneticCodes.TypeOfStructure.GetValues(typeof(GeneticCodes.TypeOfStructure));
                    System.Random random = new System.Random();
                    byte randomByte = (byte)values.GetValue(random.Next(values.Length));
                    geneticCode[i] = randomByte;


                    newStructure = new GameObject("Something");
                    break;
            }
        } while (_processed == false);
        

        //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        i++;

        //Debug.Log($"{i} = {geneticCode[i]} - Scale");
        if (debbuging) Debug.Log("P7");
        if (i >= geneticCode.Count)
        {
            if (debbuging) Debug.Log("P8");
            geneticCode.Add((byte)Random.Range(1, 4));
        }
        if (debbuging) Debug.Log("P9");
        float scale = (float)geneticCode[i];
        if (scale > 1) scale = 1;

        newStructure.transform.localScale = new Vector3(scale, scale, scale);
        i++;

        //Debug.Log("I got here also");

        if (debbuging) Debug.Log("P10");
        if (i >= geneticCode.Count)
        {
            if (debbuging) Debug.Log("P11");
            //geneticCode.Add((byte)Random.Range(0, 255));
            //geneticCode.Add((byte)Random.Range(0, 255));
            //geneticCode.Add((byte)Random.Range(0, 255));
        }

        if (debbuging) Debug.Log("P12");
        if (i >= geneticCode.Count)
        {
            geneticCode.Add((byte)Random.Range(0, 255));
        }
        float xRot = geneticCode[i++] * 1.41f;

        if (i >= geneticCode.Count)
        {
            geneticCode.Add((byte)Random.Range(0, 255));
        }
        float yRot = geneticCode[i++] * 1.41f;

        if (i >= geneticCode.Count)
        {
            geneticCode.Add((byte)Random.Range(0, 255));
        }
        float zRot = geneticCode[i++] * 1.41f;

        if (debbuging) Debug.Log("P13");


        //SphereCollider sc = sphere.AddComponent(typeof(SphereCollider)) as SphereCollider;
        if (i >= geneticCode.Count)
        {
            geneticCode.Add((byte)Random.Range(0, 255));
        }

        //Debug.Log("Got here 3");
        bool processed = false;
        //Debug.Log($"{i} = {geneticCode[i]}");
        do
        {
            switch (geneticCode[i])
            {
                case (byte)AttachClass.ToCenter:

                    //Debug.Log("have I been called? ");
                    newStructure.transform.parent = lifeForm.transform;
                    newStructure.transform.localPosition = new Vector3(0, 0, 0);
                    newStructure.transform.rotation = Quaternion.Euler(xRot, yRot, 0);
                    
                    newStructure.GetComponent<Renderer>().enabled = false;
                    newStructure.tag = "3DStruct";
                    newStructure.AddComponent<PartExec>();
                    GameObject.Destroy(newStructure.GetComponent<SphereCollider>());

                    lifeForm.GetComponent<LifeFormExec>().GetParts().Add(newStructure);
                    processed = true;
                    //attach to center
                    break;
                case (byte)AttachClass.ToLast:
                    //Debug.Log("I probably was called!");
                    
                    if(parts.Count > 0)
                    {
                        newStructure.transform.parent = parts[parts.Count - 1].transform;
                        newStructure.transform.localPosition = new Vector3(0, 0, 0);
                        newStructure.transform.rotation = Quaternion.Euler(xRot, yRot, 0);
                        newStructure.GetComponent<Renderer>().enabled = false;
                        newStructure.tag = "3DStruct";
                        newStructure.AddComponent<PartExec>();
                        GameObject.Destroy(newStructure.GetComponent<SphereCollider>());
                        lifeForm.GetComponent<LifeFormExec>().GetParts().Add(newStructure);
                        processed = true;
                        //Debug.Log()
                    } else
                    {
                        
                        geneticCode[i] = (byte)AttachClass.ToCenter;
                        
                    }
                    
                    
                    //attach to last branch
                    break;
                case 2:
                    Debug.Log("not implemented!");
                    processed = true;
                    //attach to random branch and epigenetics
                    break;

                default:
                    //Debug.Log("got here!");
                    geneticCode[i] = (byte)AttachClass.ToLast;
                    break;
            }
        } while (processed == false);



        i++;
        return i;
    }

    
}

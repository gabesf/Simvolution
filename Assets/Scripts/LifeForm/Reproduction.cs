using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public static class Reproduction 
{
    static int counter = 0;
    public static void Reproduce(GameObject oLifeForm)
    {
        //Debug.Break();
        
        counter++;
        GameObject lifeForm = new GameObject("newLife" + counter);

        LifeFormExec lifeFormExec = lifeForm.AddComponent<LifeFormExec>();
        
        lifeFormExec.SetGeneticCode(DuplicateGeneticCode(oLifeForm.GetComponent<LifeFormExec>().GetGeneticCode()));

        //if(counter >= 0)
        //{
        //    lifeFormExec.reproduced = true;
        //}

        lifeForm.transform.position = oLifeForm.transform.position;
        //lifeForm.transform.position = new Vector3(3*counter, 2, 0);
    } 

    private static GeneticCode DuplicateGeneticCode(GeneticCode oGeneticCode)
    {

        GeneticCode newGeneticCode = new GeneticCode();
        float mutationChance = oGeneticCode.GetMutationChance();
        float noise = oGeneticCode.GetNoise();
        float noiseChance = oGeneticCode.GetNoiseChance();
        List<byte> newGeneticCodeList = new List<byte>();
        List<byte> oldGeneticCodeList = oGeneticCode.GetGeneticCodeList();

        //Debug.Log("Mutation Chance " + mutationChance);

        int randomInsert = Random.Range(0, oldGeneticCodeList.Count);
        //randomInsert = 1;
        
        //Debug.Log("Random Insert at = " + randomInsert);
        byte randomValue = (byte)(Random.Range(0, 255));
        randomValue = 10;
        //Debug.Log("Random Value = " + randomValue);


        for (int i = 0; i < oldGeneticCodeList.Count; i++)
        {
            bool addNoise = false;
            if(Random.Range(0f, 1f) > noiseChance)
            {
                addNoise = true;
            }
            
            if(Random.Range(0f, 1f) < mutationChance)
            {
                Debug.Log("Random Insertion @ " + i);
                newGeneticCodeList.Add((byte)(Random.Range(0, 255)));
                Debug.Log("Safely inserted");
            }

            byte byteToAdd = oldGeneticCodeList[i];

            if (addNoise)
            {
                //Debug.Log("Adding noise to " + byteToAdd);
                byteToAdd = (byte)(byteToAdd * Random.Range(-noise, +noise));
                //Debug.Log("Added noise : " + byteToAdd);
            }

            newGeneticCodeList.Add(oldGeneticCodeList[i]);
        }

        Debug.Log("Exited");
        newGeneticCode.SetGeneticCodeList(newGeneticCodeList);
        //newGeneticCodeList = new List<byte>();

        return newGeneticCode;
    }
}

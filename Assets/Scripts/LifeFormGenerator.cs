using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeFormGenerator : MonoBehaviour
{
    
    public Camera myCam;
    List<GameObject> life;
    GameObject lifeContainer;
    bool insertController = false;
    public int numberOfInitialLifeForms = 100;
    // Start is called before the first frame update
    void Start()
    {
        lifeContainer = GameObject.Find("Life");
        life = new List<GameObject>();
        for(int i = 0; i < numberOfInitialLifeForms; i++)
        {
            GameObject lifeForm = new GameObject("lifeForm");
            lifeForm.AddComponent<LifeFormExec>();

            lifeForm.transform.position = new Vector3(Random.Range(-1f, 1f) * 50, Random.Range(0, 1f) * 50, Random.Range(-1f, 1f) * 50);
            if (i == 0)
            {
                lifeForm.transform.position = new Vector3(0, 3, 0);
                if (insertController)
                {
                    lifeForm.AddComponent<Controller>().SetLifeFormExec(lifeForm.GetComponent<LifeFormExec>());
                    lifeForm.GetComponent<LifeFormExec>().SetIsRemoteControlled(true);
                    myCam.transform.localRotation = Quaternion.Euler(0, 90, 0);
                    myCam.transform.localPosition = new Vector3(-8f, 0f, 0f);
                    myCam.transform.parent = lifeForm.transform;
                }
                
            }


            
            //

            //lifeForm.transform.parent = lifeContainer.transform;
        }

    }


}

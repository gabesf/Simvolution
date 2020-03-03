using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    LifeFormExec lifeFormExec;

    void LifeformReproduction()
    {
        lifeFormExec.AddEnergy(750);

    }


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("I am a control panel");
        GameObject controlPanel = GameObject.Instantiate(Resources.Load("ControlPanelPrefab", typeof(GameObject))) as GameObject ;

        //Button myBtn = 
        //controlPanel.transform.parent = gameObject.transform;
        

        //Canvas canvas = controlPanel.GetComponent<Canvas>();
        //Button reproductionButton = controlPanel.GetComponent<Button>();

        //Debug.Log(reproductionButton.ToString());
        //reproductionButton.onClick.AddListener(LifeformReproduction);
        //controlPanel = GameObject.Instantiate(controlPanelPrefab);
        
        lifeFormExec = gameObject.GetComponent<LifeFormExec>();
        lifeFormExec.AddEnergy(750);

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}

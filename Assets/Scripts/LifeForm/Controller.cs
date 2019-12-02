using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    LifeFormExec lifeFormExec;
    float lX, lY, rX, rY;

    List<float> controls = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            controls.Add(0f);
        }

    }

    public void SetLifeFormExec(LifeFormExec _lifeFormExec)
    {
        lifeFormExec = _lifeFormExec;
    }

    // Update is called once per frame
    void Update()
    {
        controls[0] = Input.GetAxis("Vertical");
        controls[1] = Input.GetAxis("VerticalRight");
        controls[2] = Input.GetAxis("Horizontal");
        controls[3] = Input.GetAxis("HorizontalRight");

        for(int i = 0; i < controls.Count; i++)
        {
            Debug.Log($"{i} = {controls[i]}");
        }
        lifeFormExec.SetControls(controls);


        


    }
}

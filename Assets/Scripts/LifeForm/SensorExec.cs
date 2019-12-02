using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorExec : MonoBehaviour
{
    Transform closestFoodTransform;
    Vector3 sensorOutput = Vector3.zero;

    float sensorRadius = 0;

    public void SetSensorRadius(float _sensorRadius)
    {
        sensorRadius = _sensorRadius;
    }
         
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(closestFoodTransform)
        {
            sensorOutput = transform.position - closestFoodTransform.transform.position;
            
            Debug.DrawLine(transform.position, closestFoodTransform.position, Color.red, 0, false);
            if (Vector3.Distance(transform.position, closestFoodTransform.transform.position) > sensorRadius)
            {
                sensorOutput = Vector3.zero;
                closestFoodTransform = null;
            }
        } else
        {
            //Debug.Log("It's not defined");
        }

        
        //closestFoodTransform = null;
        
    }

    public Vector3 GetSensorOutput()
    {
        return sensorOutput;
    }

    private void OnTriggerEnter(Collider other)
    {
       //Debug.Log("collision!" + other.transform.name);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.name == "food")
        {
            float dist = Vector3.Distance(other.transform.position, transform.position);
            //print("Distance to other: " + dist);

            if(!closestFoodTransform)
            {
                closestFoodTransform = other.transform;
            }

            if(dist < Vector3.Distance(closestFoodTransform.position, transform.position))
            {
                closestFoodTransform = other.transform;
            }

            
            //Debug.Log("it's a food!");
        }
    }
}

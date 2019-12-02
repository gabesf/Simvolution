//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartExec : MonoBehaviour
{
    bool islocatedAtOrigin = false;
    List<float> controls = new List<float>();
    float activityLevel = 1;

    public void SetActivityLevel(float _activityLevel)
    {
        activityLevel = _activityLevel;
        Debug.Log("Activity Level Set to: " + activityLevel);
    }

    public void SetIsLocatedAtOrigin(bool _islocatedAtOrigin)
    {
        islocatedAtOrigin = _islocatedAtOrigin;
    }

    public bool GetIsLocatedAtOrigin()
    {
        return islocatedAtOrigin;
    }

    float angleLimit = 45;
    float increment = 1;
    float phase = 0;
    float partStoredAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        phase = Random.Range(0, 3.14f);
        increment = Random.Range(0f, 0.2f);
        angleLimit = Random.Range(30, 45f);

    }

    bool motionEnabled = true;
    float x;


    float partArea = 0;

    float angle;

    public void SetPartArea(float _partArea)
    {
        partArea = _partArea;
    }

    public float GetPartArea()
    {
        return partArea;
    }

   
    

    private float UpdateRotation()
    {
        phase += (increment* activityLevel);
        float angleDifference = (Mathf.Sin(phase) * angleLimit) - partStoredAngle;
        partStoredAngle = Mathf.Sin(phase) * angleLimit;

        //Debug.Log($"Sin({phase}) = {Mathf.Sin(phase)}");
        //Debug.Log($"angle difference = {angleDifference}");
        return angleDifference;
        
    }

    public void Exec()
    {
        //Debug.Log($"I am a {gameObject.tag} and will work a bit");

        switch(gameObject.tag)
        {
            case "3DStruct":
                foreach (Transform child in transform)
                {
                    if (child.gameObject.tag == "2DStruct" && motionEnabled)
                    {
                        transform.RotateAround(transform.position, transform.right, UpdateRotation());
                        
                        //transform.localRotation = Quaternion.Euler(new Vector3(UpdateRotation(), 0f, 0f));
                    }
                    else
                    {

                    }

                }

                break;

            case "2DStruct":

                CalculateForces();

                break;

        }
        
        
    }

    Vector3 lastAngle;

    private void CalculateForces()
    {
        Vector3 currentAngle = transform.parent.transform.rotation.eulerAngles;
        //Debug.Log("Inside part = " + currentAngle);


        //Debug.Log($"Current Angle = {currentAngle.x} Last Angle = {lastAngle.x}");

        float angleVariationPerFrame = Mathf.Abs(lastAngle.x - currentAngle.x );
        //Debug.Log(angleVariationPerFrame);
        if (angleVariationPerFrame > 180.0f) angleVariationPerFrame = Mathf.Abs(angleVariationPerFrame -= 360);
        
        //Debug.Log("The delta is " + angleVariationPerFrame);
        //Debug.Log("The power is " + angleVariationPerFrame * GetPartArea());

        float longitudinalForceConstant = 0.1f;

        Vector3 longitudinalForce =  (-transform.forward * Mathf.Abs(angleVariationPerFrame * GetPartArea())) * longitudinalForceConstant;

        Debug.DrawLine(transform.position + transform.up, transform.position + transform.up + longitudinalForce, Color.magenta);
        Debug.DrawLine(transform.position, transform.position + transform.up * (angleVariationPerFrame * GetPartArea()), Color.green);

        transform.root.GetComponent<LifeFormExec>().AddForce(longitudinalForce, transform.position);

        lastAngle = currentAngle;
        //check delta angle of parent
        //check delta speed
        //store delta angle
        //calculate tangential force
        //calculate linear force
        //add to lifeform (which will calculate the torque)
    }

    public void UpdateAspect(float energyLevel, float maxEnergy)
    {

        gameObject.GetComponent<Renderer>().material.color = new Color32(0, 0, (byte)((energyLevel/maxEnergy)*255), 1);
    }
}

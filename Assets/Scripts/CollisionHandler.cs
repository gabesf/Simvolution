using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    bool lastFrameCollided = true;
    int collisionCounter = 0;
    int noCollisionCounter = 0;
    private void FixedUpdate()
    {
        noCollisionCounter++;
        //Debug.Log(noCollisionCounter);
        if(noCollisionCounter > 1)
        {
            //Debug.Log("Must be not colliding anymore!");
            transform.root.gameObject.GetComponent<LifeFormExec>().SetFormingCollision(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.transform.root == transform.root && other.transform.name != "Sensor")
        {
            //Debug.Log("Inside");
            transform.root.gameObject.GetComponent<LifeFormExec>().SetFormingCollision(true);
            noCollisionCounter = 0;
            //Debug.Log("we are from the same object!");
        } else
        {
            if(other.transform.name == "food")
            {
                GameObject.Destroy(other.gameObject);
                //Debug.Log("Got food!");
                //GameObject.Destroy(other.gameObject);
                transform.root.gameObject.GetComponent<LifeFormExec>().AddEnergy(350f);
            }
           //Debug.Log("not same object, for now, do nothing!");
        }

        //Debug.Log($"My parent is {transform.root}");
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Inside OnTriggerStay, the collider is " + other.name);
        if (other.transform.root == transform.root && other.transform.name != "Sensor")
        {
            //Debug.Log("I'm inside the if" + other.name);
            noCollisionCounter = 0;
            transform.root.gameObject.GetComponent<LifeFormExec>().SetFormingCollision(true);
            //transform.root.gameObject.GetComponent<LifeFormExec>().SetFormingCollision(false);

            //Debug.Log("we are from the same object!");
        }
        //
        
        //Debug.Log("Staying collision!");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root == transform.root)
        {
            //transform.root.gameObject.GetComponent<LifeFormExec>().SetFormingCollision(false);

            //Debug.Log("we are from the same object!");
        }
        else
        {
           //Debug.Log("not same object, for now, do nothing!");
        }
        //Debug.Log("Leaving collision!");
    }

}

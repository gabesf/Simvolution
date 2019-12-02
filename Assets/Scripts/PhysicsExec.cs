using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsExec : MonoBehaviour
{

    float volume = 0.01f;
    float mass = 0.00001f;

    Vector3 velocity;
    Vector3 acceleration;

    // Start is called before the first frame update

    

    // Update is called once per frame
    void Update()
    {
        
        Vector3 gravity = new Vector3(0, - mass * 9.82f, 0);
        
        //Debug.Log($"Gravity = {gravity}");
        velocity += gravity;
        //Debug.Log($"Velocity = {velocity}");

        //iterate through all childs to apply the resultant force

        transform.position += velocity;

        if(gameObject.name == "food")
        {
            if(transform.position.y < 0)
            {
                GameObject.Destroy(gameObject);
            }
        }


        //Debug.Log($"Position = {transform.position} ");
    }
}

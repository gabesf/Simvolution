using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neural;

public class LifeFormExec : MonoBehaviour
{
    NetworkModel neural;

    #region operation variables
    bool firstFrame = true;
    bool isRemoteControlled = false;
    List<GameObject> parts;
    List<float> controls = new List<float>();
    public bool addControlPanel = false;

    #endregion

    #region biological variables
    enum CurrentStage : byte
    {
        expanding,
        contracting,
        ready,
    }
    GeneticCode geneticCode;
    GeneticBuildingInstructions geneticBuildingInstructions;
    GeneticCodeOperator geneticCodeOperator;
    CurrentStage currentStage;
    GameObject sensor;
    //SimpleNeuralNet neural;

    bool forming = true;
    bool formingCollision = true;
    bool expandingPhaseFinished = false;
    bool contractingPhaseFinished = false;
    public bool reproduced = true;
    int currentPartBeingFormed = 0;
    float basalEnergyConsumption = 0.1f;
    float variableEnergyConsumption = 0;
    float maxEnergy = 1000;
    public float energyLevel = 500;
    #endregion

    #region physics variables
    private Vector3 forces = Vector3.zero;
    private Vector3 torques = Vector3.zero;
    private Vector3 acceleration = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    private Vector3 angularVelocity = Vector3.zero;
    private Vector3 centerOfGravity = new Vector3(0f, 0f, 0f);
    private float mass = 100f;

    Vector3 angularAcceleration = Vector3.zero;
    Vector3 momentOfInertia = new Vector3(1, 1, 1);
    #endregion

    #region operation functions

    void CreateSensor()
    {

        sensor = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        SphereCollider sc = sensor.GetComponent<SphereCollider>();
        sensor.GetComponent<Renderer>().enabled = false;
        sc.isTrigger = true;
        sensor.name = "Sensor";
        sensor.transform.localScale = new Vector3(5f, 5f, 5f);
        sensor.AddComponent<SensorExec>().SetSensorRadius(5f / 2);
        sensor.AddComponent<Rigidbody>().isKinematic = true;
        sensor.transform.parent = gameObject.transform;
        sensor.transform.localPosition = Vector3.zero;
        sensor.transform.localRotation = Quaternion.identity;
    }

    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        currentStage = CurrentStage.expanding;

        //Debug.Log("Start being called");
        if (geneticCode == null)
        {
            //Debug.Log(gameObject.name + " is creating a genetic code!");
            geneticCode = new GeneticCode();
        }
        else
        {
            //Debug.Log(gameObject.name + " already have a genetic code!");
        }

        parts = new List<GameObject>();

        

        geneticCodeOperator = new GeneticCodeOperator();
        geneticBuildingInstructions = new GeneticBuildingInstructions();

        #region neural network tests
        /*
        neural = new NetworkModel();
        neural.Layers.Add(new NeuralLayer(4, 1, "INPUT"));
        neural.Layers.Add(new NeuralLayer(2, 1, "HIDDEN"));
        neural.Layers.Add(new NeuralLayer(4, 2, "OUTPUT"));

        neural.Build();
        //Debug.Log("----Before Training------------");
        neural.Print();

        NeuralData X = new NeuralData(4);
        X.Add(1, 1);
        X.Add(1, 1);
        X.Add(1, 0);
        X.Add(1, 1);

        NeuralData Y = new NeuralData(4);
        Y.Add(1);
        Y.Add(1);
        Y.Add(0);
        Y.Add(0);

        neural.Train(X, Y, iterations: 10, learningRate: 0.1);
        //List<double> outputs = neural.Process(X);

        */
        #endregion

        //Debug.Log("----After Training------------");

        //for(int i = 0; i < outputs.Count; i++)
        //{
        //    Debug.Log($"Output{i} = {outputs[i]}");
        //}

        //neural.Print();

        //X.Add(2.0f);
        //X.Add(3.0f);

        //List<double> outputs = neural.Train(X);

        //Debug.Log("Output");
        //for (int i = 0; i < outputs.Count; i++)
        //{
        //    Debug.Log($"Out{i} = {outputs[i]}");
        //}


        List<byte> geneticCodeList = geneticCode.GetGeneticCodeList();

        geneticCodeOperator.ProcessGeneticCode(gameObject, ref geneticCodeList, geneticBuildingInstructions.GetBuildingInstructions());

        geneticCode.SetGeneticCodeList(geneticCodeList);

        //read geneticCode
        //operateGeneticCode      

    }

    public void SetControls(List<float> _controls)
    {
        controls = _controls;
    }

    public void SetIsRemoteControlled(bool _isRemoteControlled)
    {
        isRemoteControlled = _isRemoteControlled;
    }

    public List<GameObject> GetParts()
    {
        return parts;
    }
    #endregion

    #region biological functions

    public void SetFormingCollision(bool _formingCollision)
    {
        formingCollision = _formingCollision;
    }

    bool Contract()
    {
        for (int i = parts.Count - 1; i > 0; i--)
        {
            parts[i].transform.localPosition -= parts[i].transform.forward * 0.35f;
        }

        return false;
    }

    bool Expand()
    {

        //later will have to check what type of structure to find the right collider!

        if (currentPartBeingFormed > parts.Count - 1)
        {
            //Debug.Log("All set!");
            return false;
        }

        GameObject part = parts[currentPartBeingFormed];
        part.GetComponent<Renderer>().enabled = true;
        if (part.GetComponent<SphereCollider>()) //if there is a sphere collider 
        {
            if (!formingCollision) //and there is NO collision, mean that the part is already on a safe position
            {
                part.GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
                currentPartBeingFormed++;
                formingCollision = true;
            }
            else //if there IS collision
            {
                //part.transform.localPosition += new Vector3(0.01f, 0, 0);
                part.transform.localPosition += part.transform.forward * 0.01f;
                //move locally!
            }

        }
        else //if there is no sphere collider, must add
        {
            switch (part.tag)
            {
                case "3DStruct":
                    part.AddComponent<Rigidbody>().isKinematic = true;
                    SphereCollider sc = part.AddComponent(typeof(SphereCollider)) as SphereCollider;
                    part.AddComponent<CollisionHandler>();
                    sc.isTrigger = true;
                    formingCollision = true;
                    break;

                case "2DStruct":
                    currentPartBeingFormed++;
                    break;


            }





        }

        return true;




    }

    private void ResetLocalPosition()
    {
        for (int i = 0; i < parts.Count; i++)
        {
            if (parts[i].GetComponent<PartExec>().GetIsLocatedAtOrigin()) parts[i].transform.localPosition = Vector3.zero;
        }
    }

    private void Reproduce()
    {
        Reproduction.Reproduce(gameObject);
        Reproduction.Reproduce(gameObject);
        Reproduction.Reproduce(gameObject);

        AddEnergy(-500);
        //reproduced = true;
    }

    public void SetGeneticCode(GeneticCode _geneticCode)
    {
        geneticCode = _geneticCode;
    }

    public GeneticCode GetGeneticCode()
    {
        return geneticCode;
    }

    public void AddEnergy(float energy)
    {
        energyLevel += energy;
    }
    #endregion

    #region physics functions

    private void ResetForcesAndTorques()
    {
        forces = Vector3.zero;
        torques = Vector3.zero;
        acceleration = Vector3.zero;
        angularAcceleration = Vector3.zero;
    }

    private Vector3 ConvertForceToTorque(Vector3 _force, Vector3 _forcePositionAbs)
    {
        //_force = transform.forward;
        Vector3 positionDifference = transform.position - _forcePositionAbs;

        Vector3 forceApplicationPointRelative = transform.position - _forcePositionAbs;
        Vector3 centerOfGravityAbs = transform.position + centerOfGravity;

        Vector3 r = centerOfGravityAbs - _forcePositionAbs;
        r = _forcePositionAbs - centerOfGravityAbs;
        //Vector3 v2 = _forcePositionAbs - (transform.position - positionDifference + (_force * 100));
        Vector3 _F = _force;

        Vector3 _torque = Vector3.Cross(r, _F);

        Debug.DrawLine(transform.position + centerOfGravity, transform.position + centerOfGravity + r, Color.cyan, 0, false);
        Debug.DrawLine(transform.position + centerOfGravity + r, transform.position + centerOfGravity + r + _F, Color.red, 0, false);

        Debug.DrawLine(transform.position + centerOfGravity, transform.position + centerOfGravity + _torque, Color.yellow, 0, false);
        //Debug.Log($"Torque Mag = {_torque.magnitude}");

        return _torque;
    }

    private void CheckBoundaries()
    {

        if(TerrainGen.circularUniverse)
        {
            if (transform.position.x > 50)
            {
                transform.position = new Vector3(-50, transform.position.y, transform.position.z);
            }

            if (transform.position.x < -50)
            {
                transform.position = new Vector3(50, transform.position.y, transform.position.z);
            }

        } else
        {
            if (transform.position.x > 50)
            {
                transform.position = new Vector3(50, transform.position.y, transform.position.z);
            }

            if (transform.position.x < -50)
            {
                transform.position = new Vector3(-50, transform.position.y, transform.position.z);
            }
        }

        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        if (transform.position.z > 50)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 50);
        }

        if (transform.position.z < -50)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -50);
        }
    }

    public void AddForce(Vector3 _force, Vector3 _position)
    {
        //Debug.Log("Adding a force" + _force);
        forces += _force;
        torques += ConvertForceToTorque(_force, _position);
    }

    private void ApplyForce()
    {

        acceleration += forces / mass;
        //Debug.Log("Acceleration =" + acceleration);
    }


    #endregion

    private void ManagePhysics()
    {
        float dragForceConstant = 10f;

        float speed = velocity.magnitude;
        Vector3 friction = Vector3.zero;


        float dragForceMagnitude = speed * speed * dragForceConstant;
        Vector3 dragForce = -velocity.normalized * dragForceMagnitude;

        //Debug.Log("Friction " + friction.ToString("N6"));
        forces += friction;
        forces += dragForce;

        ApplyForce();
        velocity += acceleration;

        transform.RotateAround(transform.position + centerOfGravity, torques, torques.magnitude * 5);
        transform.position += velocity;

        CheckBoundaries();
    }

    

    private void ManageEnergy()
    {
        energyLevel -= basalEnergyConsumption + variableEnergyConsumption;
        if (energyLevel > maxEnergy) energyLevel = maxEnergy;
        
        if (energyLevel < 0)
        {
            GameObject.Destroy(gameObject);
        }

        if (energyLevel > 750)
        {
            Reproduce();
            Debug.Log("reproduction time!");
        }

    }

    private void ProcessParts()
    {
        int controlCounter = 0;

        for (int i = 0; i < parts.Count; i++)
        {
            if (parts[i].tag == "2DStruct" && isRemoteControlled && controlCounter < controls.Count)
            {
                parts[i].transform.parent.GetComponent<PartExec>().SetActivityLevel(controls[controlCounter]);
                controlCounter++;
                //Debug.Log("This is a 2DStruct, there for the parent is " + parts[i].transform.parent.tag);

            }
            parts[i].GetComponent<PartExec>().Exec();
            parts[i].GetComponent<PartExec>().UpdateAspect(energyLevel, maxEnergy);
        }
    }

    private void ManageNeural()
    {
        
        

        


    }

    private void Exec()
    {
        if(!reproduced)
        {
            Reproduce();
        }
        ResetForcesAndTorques();

        ManageEnergy();

        ManageNeural();

        ProcessParts();

        ManagePhysics();
        
    }
    
    
    // Update is called once per frame
    void Update()
    {

        if(addControlPanel)
        {
            addControlPanel = false;
            if(!gameObject.GetComponent<ControlPanel>())
            {
                gameObject.AddComponent<ControlPanel>();

            }

        }

        if(!firstFrame)
        {
            switch (currentStage)
            {
                case CurrentStage.expanding:
                    //Debug.Log("Expanding");
                    if (!Expand())
                    {
                        //Debug.Log("Expanded!");
                        currentStage = CurrentStage.contracting;
                    }

                    break;

                case CurrentStage.contracting:
                    if(!Contract())
                    {
                        ResetLocalPosition();
                        CreateSensor();
                        //Debug.Log("Contracting");
                        currentStage = CurrentStage.ready;
                    }
                    
                    break;

                case CurrentStage.ready:
                    
                    Exec();
                    
                    break;
            }
        }

        firstFrame = false;
       
 
     
    }

    
}

namespace LifeFormStage
{
    
}
 
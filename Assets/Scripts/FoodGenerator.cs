using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    GameObject TerrainGenerator;
    TerrainGen TerrainGeneratorScript;
    GameObject food;
    List<GameObject> foodList;

    int foodCounter = 0;
    int foodCounterLimit = 1000;
    float height = 5;
    float xLimit = 5;
    float yLimit = 5;

    public FoodGenerator()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        TerrainGenerator = GameObject.Find("TerrainGenerator");
        TerrainGeneratorScript = TerrainGenerator.GetComponent<TerrainGen>();
        food = new GameObject("Food");
        foodList = new List<GameObject>();


    }

    bool firstFrame = true;
    
    void CreateFoodUnit()
    {
        
        GameObject foodParticle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        foodParticle.name = "food";
        Renderer renderer = foodParticle.GetComponent<Renderer>();
        renderer.material.SetColor("_Color", Color.green);
        foodParticle.transform.position = new Vector3(Random.Range(-1f, 1f) * 50, 50, Random.Range(-1f, 1f) * 50);
        foodParticle.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        foodParticle.transform.parent = food.transform;
        //foodParticle.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        SphereCollider sc = foodParticle.AddComponent(typeof(SphereCollider)) as SphereCollider;
        foodParticle.AddComponent<PhysicsExec>();
        //foodList.Add(foodParticle);

        if (firstFrame)
        {
            firstFrame = false;
            foodParticle.transform.position = new Vector3(1, 4, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {

        

        foodCounter++;
        if(foodCounter > 1)
        {
            CreateFoodUnit();
            CreateFoodUnit();
            CreateFoodUnit();
            foodCounter = 0;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGen : MonoBehaviour
{
    GameObject planeBottom;
    GameObject planeTop;
    GameObject planeLeft;
    GameObject planeRight;
    GameObject planeFront;
    GameObject planeBack;

    public static Vector3 universeSize;
    public static bool circularUniverse = true;

    public Vector3 UniverseSize;
    // Start is called before the first frame update
    void Start()
    {
        planeBottom = GameObject.CreatePrimitive(PrimitiveType.Plane);
        MeshGenerator mg = new MeshGenerator();
        mg.CreatePlane(planeBottom, new Vector3(10, 10, 0));
        Transform t = planeBottom.GetComponent<Transform>();
        t.position = new Vector3(0, 0, 0);
        t.localScale = new Vector3(UniverseSize.x, 1, UniverseSize.y);
        t.parent = gameObject.transform;
    }

    public Vector3 GetUniverseSize()
    {
        return UniverseSize;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetTerrain()
    {
        return planeBottom;
    }
}

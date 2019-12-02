using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator
{
    public List<Vector3> Vertices { get; } = new List<Vector3>();
    public List<Vector3> Normals { get; } = new List<Vector3>();



    public void CreateOneDimMesh()
    {

    }

    public void CreatePlane(GameObject emptyGameObject, Vector3 dimensions)
    {
        GameObject oPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        MeshFilter oMeshFilter = oPlane.GetComponent<MeshFilter>();
        Mesh oMesh = oMeshFilter.sharedMesh;

        //Debug.Log($"There are {oMesh.vertices.Length} vertices!");
        for(int i = 0; i < oMesh.vertices.Length; i++)
        {
            //Debug.Log(oMesh.vertices[i]);
        }
        //Debug.Log(oMesh.vertices);
    }

    public void AddTwoDimMesh(GameObject emptyGameObject, Vector3 anchor, Vector3 dimensions)
    {
        //Debug.Log($"will create a plane with {dimensions}");
        //Debug.Log($"Got {emptyGameObject.name}");
        GameObject oBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject.Destroy(oBox);
        MeshFilter oMeshFilter = oBox.GetComponent<MeshFilter>();
        Mesh oMesh = oMeshFilter.sharedMesh;

        MeshFilter cMeshFilter = emptyGameObject.AddComponent<MeshFilter>();
        MeshRenderer cMeshRenderer = emptyGameObject.AddComponent<MeshRenderer>();

        Mesh cMesh = new Mesh();
        cMesh.name = "cBoxMesh";
        cMesh.vertices = oMesh.vertices;
        cMesh.vertices = CalculateVertices(dimensions);
        cMesh.triangles = oMesh.triangles;
        cMesh.normals = oMesh.normals;
        cMesh.uv = oMesh.uv;
        cMeshFilter.mesh = cMesh;
        cMeshRenderer.material = new Material(oBox.GetComponent<MeshRenderer>().sharedMaterial);
        //DisplayVertices(cMesh.vertices);
    }

    //0, 13, 23     // 0.5 , -0.5 , 0.5
    //1, 14, 16     // -0.5, -0.5 , 0.5     
    //2, 8, 22      // 0.5, 0.5, 0.5     
    //3, 9 , 17     // -0.5 , 0.5, 0.5     
    //4, 10, 21     // 0.5 , 0.5 , -0.5     
    //5, 11, 18     // -0.5, 0.5, -0.5     
    //6, 12, 20     // 0.5 , -0.5, -0,5     
    //7, 15, 19     //-0.5, -0.5, -0.5

    

    //Z GRANDE
    //X MEDIO
    //Y PEQUENO

    Vector3[] CalculateVertices(Vector3 dimensions)
    {
        Vector3[] newVertices = new Vector3[24];

        //dimensions = new Vector3(0.5f, 0.1f, 2f);

        //beggining
        newVertices[5] = newVertices[11] = newVertices[18] = new Vector3(-dimensions.x, +dimensions.y, -dimensions.z);
        newVertices[4] = newVertices[10] = newVertices[21] = new Vector3(+dimensions.x, +dimensions.y, -dimensions.z);
        newVertices[7] = newVertices[15] = newVertices[19] = new Vector3(-dimensions.x, -dimensions.y, -dimensions.z);
        newVertices[6] = newVertices[12] = newVertices[20] = new Vector3(dimensions.x, -dimensions.y, -dimensions.z);

        newVertices[5] = newVertices[11] = newVertices[18] = new Vector3(-dimensions.x, +dimensions.y, 0);
        newVertices[4] = newVertices[10] = newVertices[21] = new Vector3(+dimensions.x, +dimensions.y, 0);
        newVertices[7] = newVertices[15] = newVertices[19] = new Vector3(-dimensions.x, -dimensions.y, 0);
        newVertices[6] = newVertices[12] = newVertices[20] = new Vector3(dimensions.x, -dimensions.y, 0);

        //end
        //newVertices[0] = newVertices[13] = newVertices[23] = new Vector3(dimensions.x, -dimensions.y, 2 * dimensions.z);
        //newVertices[1] = newVertices[14] = newVertices[16] = new Vector3(-dimensions.x, -dimensions.y, 2* dimensions.z);
        //newVertices[3] = newVertices[9] = newVertices[17] = new Vector3(-dimensions.x, dimensions.y, 2* dimensions.z);
        //newVertices[2] = newVertices[8] = newVertices[22] = new Vector3(dimensions.x, dimensions.y, 2* dimensions.z);

        newVertices[0] = newVertices[13] = newVertices[23] = new Vector3(dimensions.x*0.5f, 0, 2 * dimensions.z);
        newVertices[1] = newVertices[14] = newVertices[16] = new Vector3(-dimensions.x * 0.5f, 0, 2 * dimensions.z);
        newVertices[3] = newVertices[9] = newVertices[17] = new Vector3(-dimensions.x * 0.5f, 0, 2 * dimensions.z);
        newVertices[2] = newVertices[8] = newVertices[22] = new Vector3(dimensions.x * 0.5f, 0, 2 * dimensions.z);

        return newVertices;
    }

    public void CreateTwoDimMesh()
    {
        Debug.Log("Will create a 2D mesh");


        GameObject oBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
        MeshFilter oMeshFilter = oBox.GetComponent<MeshFilter>();
        Mesh oMesh = oMeshFilter.sharedMesh;

        GameObject cBox = new GameObject("cBox");
        MeshFilter cMeshFilter = cBox.AddComponent<MeshFilter>();
        MeshRenderer cMeshRenderer = cBox.AddComponent<MeshRenderer>();

        Mesh cMesh = new Mesh();
        cMesh.name = "cBoxMesh";
        cMesh.vertices = oMesh.vertices;
        cMesh.triangles = oMesh.triangles;
        cMesh.normals = oMesh.normals;
        cMesh.uv = oMesh.uv;
        cMeshFilter.mesh = cMesh;
        cMeshRenderer.material = new Material(oBox.GetComponent<MeshRenderer>().sharedMaterial);

    }
}

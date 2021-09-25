using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface2d))]
public class NavMeshGenerator : MonoBehaviour
{
    private NavMeshSurface2d _navMesh;

    void Awake()
    {
        _navMesh = GetComponent<NavMeshSurface2d>();
    }

    public void GenerateNavMesh() 
    {
        _navMesh.BuildNavMesh();
    }    

    
}

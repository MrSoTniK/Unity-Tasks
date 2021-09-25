using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float _fov;                      //90      
    [SerializeField] private float _viewDistance;             //50
    [SerializeField] private int _rayCount;                   //50
    [SerializeField] private int _additionalRaysQuantity;     //2
    [SerializeField] private int _rayMultiplier;              //3 
    [SerializeField] private LayerMask _environment;

    private Mesh _mesh;
    private MeshFilter _meshFilter;
    private float _startingAngle;
    private Vector3 _origin;

    public void SetOrigin(Vector3 origin) 
    {
        _origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection) 
    {
        _startingAngle = GetAngleFromVector(aimDirection) + _fov / 2f;
    }

    private void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _mesh = new Mesh();
        _origin = Vector3.zero;
    }

    private void LateUpdate()
    {
        float angle = _startingAngle;
        Vector3[] verticies = new Vector3[_rayCount + _additionalRaysQuantity];
        int[] triangles = new int[_rayCount * _rayMultiplier];

        float angleIncrease = _fov / _rayCount;
        int vertexIndex = 0, triangleIndex = 0;

        verticies[vertexIndex] = _origin;
        vertexIndex++;

        for (int i = 0; i <= _rayCount; i++) 
        {
            Vector3 vertex;
          //  RaycastHit2D hit = Physics2D.Raycast(_origin, GetVectorFromAngle(angle), _viewDistance, _environment);
    //        For debugging rays:
    //        Debug.DrawLine(transform.position, _origin + GetVectorFromAngle(angle), Color.red, _viewDistance);
        //    if (hit.collider == null)          
        //        vertex = _origin + GetVectorFromAngle(angle) * _viewDistance;          
        //    else          
        //        vertex = hit.point;

            vertex = _origin + GetVectorFromAngle(angle) * _viewDistance;
            verticies[vertexIndex] = vertex;

            if(i > 0) 
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }
            vertexIndex++;
            angle -= angleIncrease;
        }

        _mesh.vertices = verticies;
        _mesh.triangles = triangles;
        _meshFilter.mesh = _mesh;
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRadius = angle * (Mathf.PI / 180f);
        Vector3 point = new Vector3(Mathf.Cos(angleRadius), Mathf.Sin(angleRadius));
        return point;
    }

    private float GetAngleFromVector(Vector3 dir)
    {
        dir = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle < 0)
            angle += 360;

        return angle;
    }
}
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewDrawer : MonoBehaviour
{
    [SerializeField] private float _meshResolution;
    [SerializeField] private FieldOfView _fov;
    [SerializeField] private MeshFilter _viewMeshFilter;
    [SerializeField] private int _edgeResolveIterationsQuantity;
    [SerializeField] private float _edgeDistanceThreshold;

    private const int _trianglesCoefficient = 2;
    private const int _verticesInTriangle = 3;
    private const string _viewMeshName = "View Mesh";
    private Mesh _viewMesh;  

    private void Awake()
    {
        _viewMesh = new Mesh();
        _viewMesh.name = _viewMeshName;
        _viewMeshFilter.mesh = _viewMesh;
    }

    private void LateUpdate()
    {
        DrawFieldOfView();      
    }

    private void DrawFieldOfView()
    {
        int stepsCount = Mathf.RoundToInt(_fov.ViewAngle * _meshResolution);
        float stepsAngleSize = _fov.ViewAngle / stepsCount;
        List<Vector3> viewPoints = new List<Vector3>();

        ViewCastInfo oldViewCast = new ViewCastInfo();

        for (int i = 0; i <= stepsCount; i++)
        {
            float angle = transform.eulerAngles.y - _fov.ViewAngle / 2 + stepsAngleSize * i;
            ViewCastInfo viewCast = ViewCast(angle);

            if(i > 0) 
            {
                bool edgeDistanceThresholdExceeded = Mathf.Abs(oldViewCast.Distance - viewCast.Distance) > _edgeDistanceThreshold;

                if (oldViewCast.Hit != viewCast.Hit || (oldViewCast.Hit && viewCast.Hit && edgeDistanceThresholdExceeded)) 
                {
                    EdgeInfo edgeInfo = FindEdge(oldViewCast, viewCast);

                    if (edgeInfo.ClosiestPointOnEdgeOnObstacle != Vector3.zero)
                        viewPoints.Add(edgeInfo.ClosiestPointOnEdgeOnObstacle);

                    if (edgeInfo.ClosiestPointOnEdgeOffObstacle != Vector3.zero)
                        viewPoints.Add(edgeInfo.ClosiestPointOnEdgeOffObstacle);
                }               
            }

            viewPoints.Add(viewCast.Point);
            oldViewCast = viewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - _trianglesCoefficient) * _verticesInTriangle];
        int verticesCount = vertexCount - 1;
        int maxVerticesCountForTriangles = verticesCount - 1;

        vertices[0] = Vector3.zero;
        for (int i = 1; i < verticesCount; i++)
        {
            vertices[i] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < maxVerticesCountForTriangles)
            {
                triangles[i * _verticesInTriangle] = 0;
                triangles[i * _verticesInTriangle + 1] = i + 1;
                triangles[i * _verticesInTriangle + 2] = i + 2;
            }
        }

        _viewMesh.Clear();
        _viewMesh.vertices = vertices;
        _viewMesh.triangles = triangles;
        _viewMesh.RecalculateNormals();
    }

    private ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 direction = _fov.DirFromAngle(globalAngle, true);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, _fov.ViewRadius, _fov.ObstaclesLayers))
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        else
            return new ViewCastInfo(false, transform.position + direction * _fov.ViewRadius, _fov.ViewRadius, globalAngle);
    }

    private EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast) 
    {
        float minAngle = minViewCast.Angle;
        float maxAngle = maxViewCast.Angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for(int i = 0; i < _edgeResolveIterationsQuantity; i++) 
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);

            bool edgeDistanceThresholdExceeded = Mathf.Abs(minViewCast.Distance - maxViewCast.Distance) > _edgeDistanceThreshold;
            if (newViewCast.Hit == minViewCast.Hit && !edgeDistanceThresholdExceeded) 
            {
                minAngle = angle;
                minPoint = newViewCast.Point;
            }
            else 
            {
                maxAngle = angle;
                maxPoint = newViewCast.Point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }

    private struct ViewCastInfo
    {
        public bool Hit;
        public Vector3 Point;
        public float Distance;
        public float Angle;

        public ViewCastInfo(bool hit, Vector3 point, float distance, float angle)
        {
            Hit = hit;
            Point = point;
            Distance = distance;
            Angle = angle;
        }
    }

    private struct EdgeInfo
    {
        public Vector3 ClosiestPointOnEdgeOnObstacle;
        public Vector3 ClosiestPointOnEdgeOffObstacle;

        public EdgeInfo(Vector3 closiestPointOnEdgeOnObstacle, Vector3 closiestPointOnEdgeOffObstacle) 
        {
            ClosiestPointOnEdgeOnObstacle = closiestPointOnEdgeOnObstacle;
            ClosiestPointOnEdgeOffObstacle = closiestPointOnEdgeOffObstacle;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundariesGenerator : MonoBehaviour
{
    [SerializeField] private Transform _boundaryPrefab;
	[SerializeField] private MapGenerator _mapGenerator;
    [SerializeField] private float _boundaryScale;

    private void Start()
    {
        GenerateBoundaries();
    }

    private void GenerateBoundaries()
	{      
        float centerX = transform.position.x;
        float centerZ = transform.position.z;

        GenerateBoundary(-_mapGenerator.MapSizeCoefficient, centerZ, new Vector3(_boundaryPrefab.localScale.x, _boundaryPrefab.localScale.y, _boundaryScale));
		GenerateBoundary(centerX, _mapGenerator.MapSize.y * _mapGenerator.MapSizeCoefficient, new Vector3(_boundaryScale, _boundaryPrefab.localScale.y, _boundaryPrefab.localScale.z));
		GenerateBoundary(_mapGenerator.MapSize.x * _mapGenerator.MapSizeCoefficient, centerZ, new Vector3(_boundaryPrefab.localScale.x, _boundaryPrefab.localScale.y, _boundaryScale));
		GenerateBoundary(centerX, -_mapGenerator.MapSizeCoefficient, new Vector3(_boundaryScale, _boundaryPrefab.localScale.y, _boundaryPrefab.localScale.z));
	}

	private void GenerateBoundary(float x, float z, Vector3 scale) 
    {
        Vector3 boundaryPosition = new Vector3(x, 0, z);
        Transform boundary = Instantiate(_boundaryPrefab, boundaryPosition, Quaternion.Euler(Vector3.right));
        boundary.localScale = scale;
        boundary.SetParent(transform);
    }
}

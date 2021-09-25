using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _portalPrefab;

    public void GenerateExit(List<Vector3> freeTiles)
    {
        float minValueX, maxValueX, minValueY, maxValueY;

        FindBorderValues(freeTiles, out minValueX, out maxValueX, out minValueY, out maxValueY);
        List<Vector3> borderTiles = freeTiles.Where(tile => tile.x == minValueX || tile.x == maxValueX || tile.y == minValueY || tile.y == maxValueY).ToList();

        System.Random random = new System.Random();
        int exitPositionID = random.Next(0, borderTiles.Count);

        Instantiate(_portalPrefab, borderTiles[exitPositionID], Quaternion.identity);
    }

    private void FindBorderValues(List<Vector3> tiles, out float minValueX, out float maxValueX, out float minValueY, out float maxValueY) 
    {
        minValueX = 0;
        maxValueX = 0;
        minValueY = 0;
        maxValueY = 0;

        foreach (var tile in tiles) 
        { 
            if(tile.x > maxValueX)             
                maxValueX = tile.x;
            
            if(tile.x < minValueX)
                minValueX = tile.x;

            if (tile.y > maxValueY)
                maxValueY = tile.y;

            if (tile.y < minValueY)
                minValueY = tile.y;
        }
    }
}

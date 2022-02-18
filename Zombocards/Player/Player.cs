using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Card
{
    public event UnityAction<Vector2, Vector2> PlayerMoved;
    public int Score { get; private set; }

    protected override IEnumerator MakeMove(Vector3 targetPosition)
    {
        Vector2 previousPosition = transform.position;
        Vector2 direction = ((Vector2) targetPosition - previousPosition).normalized;

        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed);
            yield return new WaitForEndOfFrame();
        }

        PlayerMoved?.Invoke(previousPosition, direction);
    }
}

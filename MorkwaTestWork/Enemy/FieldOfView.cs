using UnityEngine;


public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float _viewRadius;
    [SerializeField] private float _viewAngle;
    [SerializeField] private LayerMask _playerLayers;
    [SerializeField] private LayerMask _obstaclesLayers;

    public float ViewAngle => _viewAngle;
    public float ViewRadius => _viewRadius;
    public LayerMask ObstaclesLayers => _obstaclesLayers;

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) 
    {
        if (!angleIsGlobal)
            angleInDegrees += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public bool TryToFindPlayer() 
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, _viewRadius, _playerLayers);
        bool isPlayerFound = false;

        if (targetsInViewRadius.Length > 0) 
        {
            Transform targetTransform = targetsInViewRadius[0].transform;
            Vector3 dirToTarget = (targetTransform.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToTarget) <= _viewAngle / 2) 
            {
                float distToTarget = Vector3.Distance(transform.position, targetTransform.position);
                if(!Physics.Raycast(transform.position, dirToTarget, distToTarget, _obstaclesLayers)) 
                {
                    isPlayerFound = true;
                }
            }
        }

        return isPlayerFound;
    }
}
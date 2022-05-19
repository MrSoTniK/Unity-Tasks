using UnityEngine;
using UnityEngine.Events;

public class Squad : MonoBehaviour
{
    public int Power { private set; get; }

    public UnityAction<int> PointsChanged;
    public UnityAction ChangeScore;

    private void Awake()
    {
        Power = 1;
    }

    public void ChangePointsValue(string Expression, float value, bool isGoodZone) 
    {
        switch (Expression) 
        {
            case ConstantsContainer.Expression.Visualization.Addition:
                Power += (int) value;
                break;
            case ConstantsContainer.Expression.Visualization.Subtraction:
                Power -= (int) value;
                break;
            case ConstantsContainer.Expression.Visualization.Multiplication:
                Power = Mathf.RoundToInt(Power * value);
                break;
            case ConstantsContainer.Expression.Visualization.Division:
                Power = Mathf.RoundToInt(Power / value);
                break;
        }
     
        if (Power <= 0) 
        {
            Power = 0;
            Die();
        }

        PointsChanged?.Invoke(Power);
        if(isGoodZone)
            ChangeScore?.Invoke();
    }

    private void Die() 
    {
        gameObject.SetActive(false);
    }
}
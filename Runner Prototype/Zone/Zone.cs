using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] private int _partsQuantity;

    public ValuesRandomizer ValueRandomizer { private set; get; }

    public int GoodZoneNumber { private set; get; }


    private void Awake()
    {
        ValueRandomizer = GetComponentInParent<ValuesRandomizer>();
        GoodZoneNumber = ValueRandomizer.Randomizer.Next(1, _partsQuantity + 1);
    }
}
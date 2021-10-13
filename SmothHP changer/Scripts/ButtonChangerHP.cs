using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonChangerHP : MonoBehaviour
{
    [SerializeField] [Range(-10,10)] private int _damage;

    public UnityAction<int> ChangingHP;

    public void OnClick() 
    {
        ChangingHP.Invoke(_damage);
    }
}
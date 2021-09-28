using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterCoinsCollector : MonoBehaviour
{
    [SerializeField] private TMP_Text _coindIndicator;

    public int CoinsQuantity { get; private set; }
    private int _coinsQuantity = 0;

    private void Collect(GameObject coin) 
    {
        _coinsQuantity++;
        Destroy(coin);
        _coindIndicator.text = _coinsQuantity.ToString();
    }

    private void OnTriggerEnter2D(Collider2D body)
    {
        if (body.TryGetComponent<Coin>(out Coin coin))
            Collect(coin.gameObject);
    }
}

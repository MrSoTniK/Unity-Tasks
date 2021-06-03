using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderLogic : MonoBehaviour
{
    private void OnTriggerEnter(Collider body)
    {
        if(body.tag == "Projectile")
        Destroy(body.gameObject);
    }
}

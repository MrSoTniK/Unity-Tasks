using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signaling : MonoBehaviour
{    
    [SerializeField] private float _fadingSpeed;

    private AudioSource _signalingSound;
    private int _occurrenceCount;
    private int _exitCount;   

    private void Start()
    {     
        _signalingSound = GetComponent<AudioSource>();
        _occurrenceCount = 0;
        _exitCount = 0;
    }

    private void OnTriggerEnter()
    {
        _occurrenceCount = ChangeSignalizationVolume(_occurrenceCount, Fade(_fadingSpeed, 0, 1), 1);      
    }

    private void OnTriggerExit()
    {
        _exitCount = ChangeSignalizationVolume(_exitCount, Fade(_fadingSpeed, 1, 0), 2);      
    }

    private int ChangeSignalizationVolume(int zoneEnteringQuantity, IEnumerator function, int enteringQuantityValue) 
    {
        int maxEnteringQuantity = 2;

        if (zoneEnteringQuantity > maxEnteringQuantity)       
            zoneEnteringQuantity = 0;       

        zoneEnteringQuantity++;

        if (_signalingSound != null && zoneEnteringQuantity == enteringQuantityValue)       
            StartCoroutine(function);
        
        return zoneEnteringQuantity;
    }

    private IEnumerator Fade(float speed, float currentVolume, float targetVolume)
    {     
        float elapsedTime = 0;
        float pauseBetweenElapsing = 0.01f;

        _signalingSound.volume = currentVolume;
        _signalingSound.Play();

        while (_signalingSound.volume != targetVolume)
        {
            elapsedTime += Time.deltaTime;
            _signalingSound.volume = Mathf.MoveTowards(currentVolume, targetVolume, speed * elapsedTime);
            yield return new WaitForSeconds(pauseBetweenElapsing);
        }

        if (_signalingSound.volume == 0)
            _signalingSound.Stop();
    }   
}

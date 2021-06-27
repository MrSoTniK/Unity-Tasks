using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signaling : MonoBehaviour
{    
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
        if(_occurrenceCount > 2) 
        {
            _occurrenceCount = 0;
        }
        _occurrenceCount++;
        float divisionRemainder = _occurrenceCount % 2;
        if (_signalingSound != null && divisionRemainder != 0)
        {
            StartCoroutine(FadeIn(0.2f));
        }      
    }

    private void OnTriggerExit()
    {
        if (_exitCount > 2)
        {
            _exitCount = 0;
        }
        _exitCount++;
        float divisionRemainder = _exitCount % 2;
        if (_signalingSound != null && divisionRemainder == 0)
        {
            StartCoroutine(FadeOut(0.2f));
        }
    }

    IEnumerator FadeIn(float speed)
    {
        _signalingSound.volume = 0f;
        _signalingSound.Play();

        for (float i = 0; i <= 1; i += speed)
        {
            _signalingSound.volume = i;
            yield return new WaitForSeconds(1f);
        }     
    }

    IEnumerator FadeOut(float speed)
    {
        for (float i = 1; i >= 0; i -= speed)
        {
            _signalingSound.volume = i;
            yield return new WaitForSeconds(1f);
        }
        _signalingSound.Stop();      
    }
}

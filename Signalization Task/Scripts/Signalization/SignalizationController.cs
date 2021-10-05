using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SignalizationController : MonoBehaviour
{
    [SerializeField] private float _fadingSpeed;
    [SerializeField] [Range(0,1)] private float _maxVolume;

    private AudioSource _signalingSound;
    private Coroutine _currentCoroutine;

    private void Start()
    {
        _signalingSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
            ChangeSignalizationVolume(Fade(_maxVolume));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
            ChangeSignalizationVolume(Fade(0));
    }

    private void ChangeSignalizationVolume(IEnumerator function)
    {
        if (_signalingSound != null) 
        {          
            if(_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(function);         
        }
    }

    private IEnumerator Fade(float targetVolume)
    {
        float elapsedTime = 0;
        float currentVolume = _signalingSound.volume;

        if(targetVolume > 0)
            _signalingSound.Play();

        while (_signalingSound.volume != targetVolume)
        {
            elapsedTime += Time.deltaTime;
            _signalingSound.volume = Mathf.MoveTowards(currentVolume, targetVolume, _fadingSpeed * elapsedTime);
            yield return new WaitForFixedUpdate();
        }

       if (_signalingSound.volume == 0)
            _signalingSound.Stop();
    }
}

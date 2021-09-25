using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YesNoButtonController : MonoBehaviour
{
    [SerializeField] private int _sceneBuildIndex;

    public void OnClick()
    {
        SceneManager.LoadScene(_sceneBuildIndex);
    }
}

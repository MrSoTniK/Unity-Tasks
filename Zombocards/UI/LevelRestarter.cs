using UnityEngine;
using IJunior.TypedScenes;

public class LevelRestarter : MonoBehaviour
{
    public void RestartLevel_1()
    {
        Level_1.Load();
    }
}
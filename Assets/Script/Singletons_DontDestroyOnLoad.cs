using UnityEngine;

public class Singletons_DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}

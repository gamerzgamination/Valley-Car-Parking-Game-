using UnityEngine;
using UnityEngine.UI;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        this.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField]
    string nextSceneName;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("player"))
            SceneManager.LoadScene(nextSceneName);
    }
}

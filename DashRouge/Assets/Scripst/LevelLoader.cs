using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public TextMesh text;
    void Start()
    {
        text.text = "Level " + (SceneManager.GetActiveScene().buildIndex - 1).ToString();
    }

    
}

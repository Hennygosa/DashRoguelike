using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int numberOfEnemies;

    private void Update()
    {
        numberOfEnemies = GameObject.FindGameObjectsWithTag("enemy").Length 
            + GameObject.FindGameObjectsWithTag("rangedEnemy").Length 
            + GameObject.FindGameObjectsWithTag("BossSpyder").Length;
        if (numberOfEnemies == 0)
        {
            ShowExit();
        }
    }
    public void ShowExit()
    {
        gameObject.transform.position = new Vector3(0, 0, 69);
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("player"))
        {
            if (SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadScene(0);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

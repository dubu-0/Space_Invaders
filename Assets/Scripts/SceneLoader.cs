using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private float loadSceneDelay = 4.5f;
    [SerializeField] private AudioClip winSound;

    private Score _score;

    private void Start()
    {
        _score = FindObjectOfType<Score>();
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
        
        if (_score)
        {
            Destroy(_score.gameObject);   
        }
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);

        if (_score)
        {
            _score.ResetScore();   
        }
    }
    
    public void LoadSecondLevel()
    {
        SceneManager.LoadScene(2);
        
        if (_score)
        {
            _score.ResetScore();   
        }
    }
    
    public void LoadThirdLevel()
    {
        SceneManager.LoadScene(3);
        
        if (_score)
        {
            _score.ResetScore();   
        }
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(WaitAndLoadGameOver());
    }

    public void LoadNextScene()
    {
        AudioSource.PlayClipAtPoint(winSound, Camera.main.transform.position);
        StartCoroutine(LoadNextSceneAfterDelay());
    }

    private IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(loadSceneDelay); 
        
        var currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentBuildIndex + 1);
    }
    
    private IEnumerator WaitAndLoadGameOver()
    {
        yield return new WaitForSeconds(loadSceneDelay);
        SceneManager.LoadScene("Game Over Menu");
    }
}

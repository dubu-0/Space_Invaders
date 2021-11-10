using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private int numberOfEnemies;

    private SceneLoader _sceneLoader;

    private void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
    }

    private void Update()
    {
        if (numberOfEnemies <= 0)
        {
            _sceneLoader.LoadNextScene();
            numberOfEnemies = 1;
        }
    }

    public void AddEnemyToLevel()
    {
        numberOfEnemies++;
    }

    public void RemoveEnemyFromLevel()
    {
        numberOfEnemies--;
    }
}

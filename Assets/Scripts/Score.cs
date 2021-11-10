using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;

    private static Score _instance = null;
    
    private int _score;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        if (_score > 0)
        {
            textMesh.text = _score.ToString();    
        }
        else
        {
            textMesh.text = "";
        }
    }
    
    public void AddToScore(int points)
    {
        _score += points;
        UpdateScore();
    }

    public void ResetScore()
    {
        _score = 0;
        UpdateScore();
    }
}

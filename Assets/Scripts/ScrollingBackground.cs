using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private float scrollingSpeed = 0.03f;

    private Material _myMaterial;
    
    private Vector2 _offset;

    private void Start()
    {
        _myMaterial = GetComponent<Renderer>().material;
        _offset = new Vector2(0f, scrollingSpeed);
    }

    private void Update()
    {
        _myMaterial.mainTextureOffset += _offset * Time.deltaTime;
    }
}

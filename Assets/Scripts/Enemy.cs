using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [Header("Projectile settings")] 
    [SerializeField] private GameObject[] laserPrefabs;
    [SerializeField] private Vector3[] projectileSpawnOffsets;
    [SerializeField] private float[] projectileSpeeds;
    [SerializeField] [Range(0.0001f, 10f)] private float minTimeBetweenShots;
    [SerializeField] [Range(0.0001f, 10f)] private float maxTimeBetweenShots;

    [Header("SFX")]
    [SerializeField] [Range(0f, 1f)] private float shotVolume = 0.1f;
    [SerializeField] private AudioClip[] shotsSFXs;
    
    private float _delay;
    private Level _level;

    private void Awake()
    {
        _level = FindObjectOfType<Level>();
        _level.AddEnemyToLevel();
    }

    private void Start()
    {
        _delay = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        _delay -= Time.deltaTime;
        
        if (_delay <= 0)
        {
            Fire();
        }
    }

    private void Fire()
    {
        for (int i = 0; i < laserPrefabs.Length; i++)
        {
            var enemyPosition = transform.position;
            var noRotation = laserPrefabs[i].transform.rotation;
            var laser = Instantiate(laserPrefabs[i], enemyPosition + projectileSpawnOffsets[i], noRotation);
            
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -projectileSpeeds[i]);
            
            PlayShotSFX(i);
            
            _delay = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void PlayShotSFX(int index)
    {
        if (gameObject.GetComponent<SpriteRenderer>().isVisible)
        {
            AudioSource.PlayClipAtPoint(shotsSFXs[index], Camera.main.transform.position, shotVolume);   
        }
    }
}

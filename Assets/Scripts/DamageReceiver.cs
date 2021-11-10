using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField] private float health = 1000f;
    
    [Header("VFX")]
    [SerializeField] private ParticleSystem explosionFX;
    [SerializeField] private float explosionLifetime = 3f;
    [SerializeField] private Vector3 explosionPositionOffset;

    [Header("SFX")] 
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] [Range(0f, 1f)] private float explosionVolume = 0.45f;

    private SceneLoader _sceneLoader;
    private Level _level;
    private Score _score;

    private float _maxHealth;

    private void Awake()
    {
        _maxHealth = health;
    }

    private void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _level = FindObjectOfType<Level>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        _score = FindObjectOfType<Score>();
        
        ReceiveDamage(coll);
        DestroyHandler(coll);
    }

    public float GetCurrentHealth()
    {
        return health;
    }
    
    public float GetMaxHealth()
    {
        return _maxHealth;
    }
    
    private void ReceiveDamage(Collider2D coll)
    {
        var damageDealer = coll.gameObject.GetComponent<DamageDealer>();

        if (damageDealer)
        {
            health -= damageDealer.GetDamage();   
        }
    }

    private void DestroyHandler(Collider2D coll)
    {
        if (health <= 0)
        {
            PlayExplosionEffect();
            PlayExplosionSoundEffect();

            if (gameObject.layer == 7)
            {
                if (gameObject.CompareTag("Light Alien"))
                {
                    _score.AddToScore(13);                
                }
                else if (gameObject.CompareTag("Medium Alien"))
                {
                    _score.AddToScore(57);                
                }
                else if (gameObject.CompareTag("Frigate"))
                {
                    _score.AddToScore(700);
                }
                else if (gameObject.CompareTag("Boss"))
                {
                    _score.AddToScore(15000);
                }
                
                _level.RemoveEnemyFromLevel();
            }
            
            Destroy(gameObject);
            LoadGameOverScene(gameObject.layer == 6);
        }
    }

    private void PlayExplosionEffect()
    {
        if (explosionFX)
        {
            var explosionEffect = Instantiate(explosionFX, transform.position + explosionPositionOffset, transform.rotation);
            Destroy(explosionEffect, explosionLifetime);   
        }
    }

    private void PlayExplosionSoundEffect()
    {
        if (explosionSound)
        {
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, explosionVolume);
        }
    }

    private void LoadGameOverScene(bool playerWasDestroyed)
    {
        if (playerWasDestroyed)
        {
            _sceneLoader.LoadGameOverScene();
        }
    }
}

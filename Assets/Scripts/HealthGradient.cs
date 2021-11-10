using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthGradient : MonoBehaviour
{
    [SerializeField] private Gradient gradient;

    private DamageReceiver _receiver;
    private SpriteRenderer _shipSpriteRenderer;

    private float _currentHealth;
    private float _maxHealth;

    private void Start()
    {
        _receiver = gameObject.GetComponent<DamageReceiver>();
        _shipSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _maxHealth = _receiver.GetMaxHealth();
        
        _shipSpriteRenderer.color = gradient.Evaluate(1f);
    }

    private void Update()
    {
        _currentHealth = _receiver.GetCurrentHealth();
        _shipSpriteRenderer.color = gradient.Evaluate(_currentHealth / _maxHealth);
    }
}

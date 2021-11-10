using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Projectile settings")]
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Vector3 projectileSpawnOffset;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float rateOfFire;

    [Header("Player settings")]
    [SerializeField] private float playerMovementSpeed;
    [SerializeField] private float padding;

    [Header("SFX")] 
    [SerializeField] private AudioClip shotSFX;
    [SerializeField] [Range(0f, 1f)] private float shotVolume = 0.15f;
    
    private float _playSpaceWidth;
    private float _playSpaceHeight;
    private Coroutine _firing;

    private void Start()
    {
        CalculatePlaySpaceSize();
    }

    private void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        transform.position = GetNewPosition();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _firing = StartCoroutine(FireContinuously());
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(_firing);
        }
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            var playerPosition = transform.position;
            var noRotation = Quaternion.identity;

            var laser = Instantiate(laserPrefab, playerPosition + projectileSpawnOffset, noRotation);

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, projectileSpeed);

            PlayShotSFX();
            
            yield return new WaitForSeconds(1f / rateOfFire);
        }
    }

    private void PlayShotSFX()
    {
        AudioSource.PlayClipAtPoint(shotSFX, Camera.main.transform.position, shotVolume);
    }

    private void CalculatePlaySpaceSize()
    {
        if (Camera.main != null)
        {
            var mainCamera = Camera.main;
            _playSpaceWidth = mainCamera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;
            _playSpaceHeight = mainCamera.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y;
        }
        else
        {
            Debug.LogError("Main Camera is missing!");
        }
    }
    
    private Vector2 GetNewPosition()
    {
        var currentPosition = transform.position;
        
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerMovementSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerMovementSpeed;

        var newPositionX = Mathf.Clamp(currentPosition.x + deltaX, padding, _playSpaceWidth - padding);
        var newPositionY = Mathf.Clamp(currentPosition.y + deltaY, padding, _playSpaceHeight - padding);
        
        var newPosition = new Vector2(newPositionX, newPositionY);
        
        return newPosition;
    }
}

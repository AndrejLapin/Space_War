using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int points = 10;

    [Header("Shooting")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] float[] projectileSpeedX = { 10f };
    [SerializeField] float[] projectileSpeedY = { 0f };
    [SerializeField] float[] rotation = { 0f };
    [SerializeField] float[] burstDelay = { 0f };
    float currentBurstShot;

    //[SerializeField] GameObject gun;

    [Header("Destruction")]
    [SerializeField] GameObject explosion;
    [SerializeField] float durationOfExplosion = 1f;

    [Header("SFX")]
    [SerializeField] AudioClip die;
    [SerializeField] float dieVol = 1f;
    [SerializeField] AudioClip shoot;
    [SerializeField] float shotVol = 1f;

    GameSession gameSession;

    Coroutine firingCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
        }
    }

    private void Fire()
    {
        StartCoroutine(FireDelay());
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    IEnumerator FireDelay()
    {
        for (int shotIndex = 0; shotIndex < projectileSpeedX.Length; shotIndex++)
        {
            GameObject laser = Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, rotation[shotIndex])) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeedY[shotIndex], -projectileSpeedX[shotIndex]);

            AudioSource.PlayClipAtPoint(shoot, Camera.main.transform.position, shotVol);
            yield return new WaitForSeconds(burstDelay[shotIndex]);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameSession.UpdateScore(points);
        GameObject death = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
        Destroy(death, durationOfExplosion);
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(die, Camera.main.transform.position, dieVol);
    }
}

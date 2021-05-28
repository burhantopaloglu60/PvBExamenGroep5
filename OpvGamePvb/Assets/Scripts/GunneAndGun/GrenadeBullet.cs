﻿using UnityEngine;
using System;
using WaveSystem;

public class GrenadeBullet : MonoBehaviour
{
    [SerializeField, Range(2, 100)] private float _bulletSpeed = 2;
    [SerializeField] private Rigidbody _bulletRb;
    [SerializeField] private int _hitBonus;


    private GameObject _barrelEnd;
    private GameObject _barrelBegin;
    private GameObject _landingPlace;
    [SerializeField]
    private GameObject _particle;
    
    
    
    
    private void Start()
    {
        _landingPlace = GameObject.Find("LandingPlace");
        _barrelEnd = GameObject.FindWithTag("GBarrelEnd");
        _barrelBegin = GameObject.FindWithTag("GBarrelBegin");
        Instantiate(_particle,_barrelEnd.transform.position,Quaternion.identity);
        Vector3 direction = (_barrelEnd.transform.position - _barrelBegin.transform.position).normalized;
        _bulletRb.velocity = (direction * _bulletSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "EnemyShip": DeleteBullet();
                //Destroy(other.gameObject);
                PointInput.Instance.AddMultiplier(1);
                WavesManager.Instance.OnEnemyDeath.Invoke(1,other.gameObject);
                break;
            case "Wataa": DeleteBullet();
                PointInput.Instance.ResetMultiplier();
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayArea"))
        {
            DeleteBullet();
        }
    }

    private void DeleteBullet()
    {
        _landingPlace.transform.position = transform.position;
        Destroy(this.gameObject);
    }
}
﻿using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _bulletRb;
    [SerializeField, Range(2, 50)]
    private float _bulletSpeed = 2;
    
    private GameObject _barrelEnd;
    private GameObject _barrelBegin;
    private GameObject _landingPlace;
    
    
    private void Start()
    {
        _landingPlace = GameObject.Find("LandingPlace");
        _barrelEnd = GameObject.FindWithTag("BarrelEnd");
        _barrelBegin = GameObject.FindWithTag("BarrelBegin");
        Vector3 direction = (_barrelEnd.transform.position - _barrelBegin.transform.position).normalized;
        _bulletRb.velocity = (direction * _bulletSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "EnemyShip": DeleteBullet();
                break;
            case "Wataa": DeleteBullet();
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

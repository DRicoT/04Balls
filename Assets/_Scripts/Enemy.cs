using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Enemy : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private float moveForce;

    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //HACE QUE EL ENEMIGO SIGA AL PLAYER
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        _rigidbody.AddForce(moveForce * lookDirection, ForceMode.Force); //ForceMode por defecto es .Force
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KillZone"))
        {
            StartCoroutine(DestroyEnemy());
        }

    }

    IEnumerator DestroyEnemy()
    {
        moveForce = 0;
        yield return new WaitForSecondsRealtime(2);
        Destroy(this.gameObject);
    }
}

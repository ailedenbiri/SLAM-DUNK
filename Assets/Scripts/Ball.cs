using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private AudioSource BallSound;


    private void OnTriggerEnter(Collider other)
    {
       

        if (other.CompareTag("Basket"))
        {
            _GameManager.Basket(transform.position);
        }
        else if (other.CompareTag("GameOver"))
        {
           _GameManager.Lose();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        BallSound.Play();
    }
}

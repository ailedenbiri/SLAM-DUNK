using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Strength : MonoBehaviour
{
    [SerializeField] private float Angle;
    [SerializeField] private float Strength;

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Angle, 90, 0) * Strength, ForceMode.Force);
    }

}

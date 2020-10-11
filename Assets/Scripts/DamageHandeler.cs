using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandeler : MonoBehaviour
{
    public float crashTollerence;
    public GameObject explosionEffect;
    private Rigidbody rb;
    private bool alive = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision other) {
        if (other.relativeVelocity.magnitude > crashTollerence && alive)
        {
            alive = false;
            GameObject instantiatedExplosion = Instantiate(explosionEffect, transform.position, transform.rotation);
            AudioSource soundEffect = instantiatedExplosion.GetComponent<AudioSource>();
            if (soundEffect != null)
            {
                soundEffect.Play();
            }
            rb.freezeRotation = false;
        }
    }
    public bool isAlive()
    {
        return alive;
    }
}

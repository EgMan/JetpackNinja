using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageHandeler : MonoBehaviour
{
    public float crashTollerence;
    public GameObject explosionEffect;
    public GameObject deathText;
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
            deathText.SetActive(true);
            deathText.GetComponent<Text>().enabled = true;
        }
    }
    public bool isAlive()
    {
        return alive;
    }
}

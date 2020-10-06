using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour
{
    public float thrustMultiplier = 25f;
    public AudioSource soundEffect;
    private Rigidbody rb;
    private CharacterController character;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        character = gameObject.GetComponent<CharacterController>();
    }

    private void FixedUpdate() {
        float jetpackInput = OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);
        rb.AddForce(new Vector3(0, jetpackInput * thrustMultiplier, 0), ForceMode.Force);
        soundEffect.volume = jetpackInput;

        if (Input.GetKey(KeyCode.J))
        {
            rb.AddForce(new Vector3(0, thrustMultiplier, 0), ForceMode.Force);
        }
    }
}

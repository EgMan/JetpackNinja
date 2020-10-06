using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetPack : MonoBehaviour
{
    public float maxFuel;
    public DamageHandeler lifeline;
    public Slider FuelBar, ThrustBar;
    public Text FuelLabel, ThrustLabel;
    public float thrustMultiplier = 25f;
    public AudioSource soundEffect;
    private Rigidbody rb;
    private CharacterController character;
    private float currentFuel;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        character = gameObject.GetComponent<CharacterController>();
        currentFuel = maxFuel;
    }

    private void FixedUpdate()
    {

        float jetpackInput = OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);
        if (currentFuel > 0 && lifeline.isAlive())
        {
            //Thrust
            rb.AddForce(new Vector3(0, jetpackInput * thrustMultiplier, 0), ForceMode.Force);
            soundEffect.volume = jetpackInput;
            ThrustBar.value = Mathf.Floor(jetpackInput * 100);
            ThrustLabel.text = $"Thrust {ThrustBar.value}%";

            //Fuel
            currentFuel -= jetpackInput;
            FuelBar.value = Mathf.Floor((100 * currentFuel) / maxFuel);
            FuelLabel.text = $"Fuel {FuelBar.value}%";
        }
        else
        {
            soundEffect.volume = 0;
            ThrustBar.value = 0;
            ThrustLabel.text = "Thrust 0%";

            currentFuel = 0;
            FuelBar.value = 0;
            FuelLabel.text = "Fuel 0%";
        }



        if (Input.GetKey(KeyCode.J))
        {
            rb.AddForce(new Vector3(0, thrustMultiplier, 0), ForceMode.Force);
        }
    }
}

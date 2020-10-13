using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelContainer : MonoBehaviour
{

    public float spinSpeed = 0.1f;
    public int units;
    public Text frontNumber, backNumber;
    private void Start() {
        frontNumber.text = $"{units}";
        backNumber.text = $"{units}";
    }
    private void FixedUpdate()
    {
            Vector3 euler = transform.rotation.eulerAngles;
            euler.y += spinSpeed;
			transform.rotation = Quaternion.Euler(euler);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBPlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float snapRotateAmount = 2f;
    public DamageHandeler lifeline;

    
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate() {

            if (!lifeline.isAlive())
            {
                return;
            }
            //input
			float moveForward = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ? moveSpeed:0;
			float moveBack = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ? moveSpeed:0;
			float moveLeft = Input.GetKey(KeyCode.A) ? moveSpeed:0;
			float moveRight = Input.GetKey(KeyCode.D) ? moveSpeed:0;

            // bool turnLeft = Input.GetKey(KeyCode.LeftArrow) || OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft);
            // bool turnRight =  Input.GetKey(KeyCode.RightArrow) || OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight);
            float yawAmount = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x + 
                (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) + 
                (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0);


            // Translational movement
            Vector3 forward = transform.forward;
            Vector3 forwardOrhogonal = Quaternion.Euler(0, 90, 0) * forward;
            

            Vector2 keyboardMovementVect = new Vector2(moveRight - moveLeft, moveForward - moveBack);
            Vector2 joystickMovementVect = moveSpeed * OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            Vector2 combinedMovement = keyboardMovementVect + new Vector2(joystickMovementVect.x, joystickMovementVect.y);

            // rb.AddForce(transform.localToWorldMatrix.MultiplyPoint3x4(new Vector3(combinedMovement.x, 0, combinedMovement.y)));
            // rb.AddForce(new Vector3(combinedMovement.x, 0, combinedMovement.y));
            rb.AddForce(combinedMovement.y * forward + combinedMovement.x * forwardOrhogonal);

            // Rotational movement
            Vector3 euler = transform.rotation.eulerAngles;
            euler.y += yawAmount * snapRotateAmount;
            // if (turnRight)
            // {
            //     euler.y += snapRotateAmount;
            // }
            // if (turnLeft)
            // {
            //     euler.y -= snapRotateAmount;
            // }

			transform.rotation = Quaternion.Euler(euler);
    }
}

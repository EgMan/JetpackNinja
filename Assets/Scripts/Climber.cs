using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climber : MonoBehaviour
{
    public ClimberHand left, right;
    private ClimberHand currentlyGrabbing = null;
    private Vector3 initialHandPos, initialCameraHandOffset, initialClimbablePos;
    private Rigidbody rb;
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        updateClimbingHand();
        setCameraOffset();
    }
    private void updateClimbingHand()
    {
        if (currentlyGrabbing == left)
        {
            if (!left.grabbing)
            {
                if (!right.grabbing)
                {
                    resetClimbingHand();
                }
                else
                {
                    changeClimbingHand(right);
                }
            }
        }
        else if (currentlyGrabbing == right)
        {
            if (!right.grabbing)
            {
                if (!left.grabbing)
                {
                    resetClimbingHand();
                }
                else
                {
                    changeClimbingHand(left);
                }
            }
        }
        else
        {
            if (right.grabbing)
            {
                changeClimbingHand(right);
            }
            else if (left.grabbing)
            {
                changeClimbingHand(left);
            }
        }
    }

    private void resetClimbingHand()
    {
        currentlyGrabbing = null;
        rb.useGravity = true;
        rb.isKinematic = false;
    }
    private void changeClimbingHand(ClimberHand hand)
    {
        currentlyGrabbing = hand;
        initialHandPos = hand.transform.position;
        initialCameraHandOffset = rb.position - initialHandPos;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void setCameraOffset()
    {
        if (!currentlyGrabbing || !currentlyGrabbing.grabbing)
        {
            return;
        }
        // TODO add grabbed object offset
        rb.MovePosition(rb.position + initialHandPos - currentlyGrabbing.transform.position);
    }
}

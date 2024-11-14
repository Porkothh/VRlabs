using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Throwable : MonoBehaviour
{
    private Rigidbody rb;
    private float throwForceMultiplier = 1.5f;
    private bool isAttached = false;
    private Vector3 previousPosition;
    private Hand attachedHand;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void HandHoverUpdate(Hand hand)
    {
        if (hand.GetGrabStarting() != GrabTypes.None)
        {
            hand.AttachObject(gameObject, GrabTypes.Grip);
            isAttached = true;
            attachedHand = hand;
            previousPosition = transform.position;
        }
    }

    private void OnAttachedToHand(Hand hand)
    {
        isAttached = true;
        attachedHand = hand;
    }

    private void OnDetachedFromHand(Hand hand)
    {
        isAttached = false;

        Vector3 velocity = (transform.position - previousPosition) / Time.deltaTime;
        
        rb.velocity = velocity * throwForceMultiplier;
        rb.angularVelocity = attachedHand.GetTrackedObjectVelocity() * throwForceMultiplier;
    }

    private void FixedUpdate()
    {
        if (isAttached)
        {
            previousPosition = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

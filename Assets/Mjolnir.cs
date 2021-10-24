using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mjolnir : MonoBehaviour
{
    public Transform hand;
    public Rigidbody rb;

    public bool isHeld;
    public bool isRetracting;

    public float throwPower;
    public float retractPower;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Catch();
    }

    void Update()
    {
        if (isHeld && Input.GetMouseButtonDown(0))
        {
            Throw();
        }
        else if (!isHeld && Input.GetMouseButton(1))
        {
            isRetracting = true;
        }
        else if (!isHeld && Input.GetMouseButtonUp(1))
        {
            isRetracting = false;
        }
    }

    private void FixedUpdate()
    {
        if (isRetracting)
        {
            Retract();
        }
    }

    void Throw()
    {
        rb.isKinematic = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        transform.parent = null;

        rb.AddForce(transform.forward * throwPower, ForceMode.Impulse);
        isHeld = false;
    }

    void Retract()
    {
        if (Vector3.Distance(hand.position, transform.position) < 1)
        {
            Catch();
        }

        Vector3 directionToHand = hand.position - transform.position;
        rb.velocity = (directionToHand.normalized * retractPower);
    }

    void Catch()
    {
        isRetracting = false;
        isHeld = true;

        rb.isKinematic = true;
        rb.interpolation = RigidbodyInterpolation.None;
        transform.position = hand.position;
        transform.parent = hand;
        transform.rotation = hand.rotation;
    }
}

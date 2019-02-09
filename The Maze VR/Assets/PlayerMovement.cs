using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float MovementAmount = 0.08f;
    private const float RotationAmount = 5f;

    public Rigidbody Rb;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PlayerMovement script loaded");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int forwardMovement = Convert.ToInt32(Input.GetKey("z")) - Convert.ToInt32(Input.GetKey("s"));
        int rotation = Convert.ToInt32(Input.GetKey("d")) - Convert.ToInt32(Input.GetKey("q"));
        transform.position += transform.forward * MovementAmount * forwardMovement;
        transform.Rotate(Vector3.up * rotation * RotationAmount, Space.World); 
    }
}

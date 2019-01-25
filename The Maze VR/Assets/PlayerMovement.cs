using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody Rb;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PlayerMovement script loaded");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("z"))
        {
            Debug.Log("Go Forwards !");
            Rb.AddForce(200f * Time.deltaTime,0,0);
        }
        
        if (Input.GetKey("s"))
        {
            Rb.AddForce(-200f * Time.deltaTime,0,0);
        }
        
        if (Input.GetKey("q"))
        {
            Rb.AddForce(0,0,200f * Time.deltaTime);
        }
        
        if (Input.GetKey("d"))
        {
            Rb.AddForce(0,0,-200f * Time.deltaTime);
        }
    }
}

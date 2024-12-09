using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20.0f;
    private Rigidbody playerRb;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    private Animator playerAnim;
    public float horizontalInput;
    public float fowardInput;
    public float turnSpeed = 45.0f;

    // Start is called before the first frame update
    void Start()
    {

        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier; 
        playerAnim = GetComponent<Animator>();
    }

    
    // Update is called once per frame
    
    
    void Update()
    {
        horizontalInput = Input.GetAxis ("Horizontal");
        fowardInput = Input.GetAxis ("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * speed * fowardInput); 
        transform.Rotate(Vector3.up * Time.deltaTime * horizontalInput * turnSpeed);

        if(fowardInput != 0)
        {
            playerAnim.SetBool("Static_b", true);
            playerAnim.SetFloat("Speed_f", 0.26f);

        }
        if(fowardInput == 0 )
        {
            playerAnim.SetFloat("Speed_f", 0f);
        }

        if(Input.GetKeyDown(KeyCode.Space) && isOnGround) 
        {
             Debug.Log("Here");
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); 
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig"); 
        }
    }

    private void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}

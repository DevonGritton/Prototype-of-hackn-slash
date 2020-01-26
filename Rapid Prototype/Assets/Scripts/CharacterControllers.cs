using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllers : MonoBehaviour
{
    public float moveSpeed = 10.0f;

    [SerializeField]
    private string controllerPrefix = "";
    
    public bool isGrounded = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis(controllerPrefix + "Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;

        Jump();
    }
    void Jump()
    {
        if (Input.GetButtonDown(controllerPrefix + "Jump") && isGrounded == true)
        {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}

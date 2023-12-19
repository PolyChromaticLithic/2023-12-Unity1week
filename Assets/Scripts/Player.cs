using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public float movementSpeed = 5f;
    public bool isMove;
    Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        isMove = true;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (isMove)
        {
            Vector2 currentPos = rbody.position;

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
            inputVector = Vector2.ClampMagnitude(inputVector, 1);
            Vector2 movement = inputVector * movementSpeed;
            Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
            rbody.MovePosition(newPos);
        }
    }
}
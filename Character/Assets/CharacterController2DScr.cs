using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2DScr : MonoBehaviour
{
    public CharacterControllerParameter parameter;
    Rigidbody2D rigidbody2D;
    SpriteRenderer playerSprite;
    bool groundCheck = false;

    //Variable
    bool isCharge = false;

    //VariableMemory
    float speedMemory;


    void Start()
    {
        speedMemory = parameter.speed;
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float horizontalValue = Input.GetAxis("Horizontal");
        float verticalValue = Input.GetAxis("Vertical");

        //Input
        if(horizontalValue != 0 )
        {
            if(horizontalValue > 0)
            {
                playerSprite.flipX = true;
            }
            else
            {
                playerSprite.flipX = false;
            }

            Move(horizontalValue);
        }
        if (verticalValue != 0 && groundCheck)
        {
            Jump(parameter.jumpStr);
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            isCharge = true;
            Charge();
        }
        else
        {
            parameter.speed = speedMemory;
            isCharge = false;
        }

    }

    #region - Movements -

    //Move
    void Move(float horizontal)
    {
        var movePos = horizontal * parameter.speed;


        transform.position += new Vector3(movePos, 0, 0) * Time.deltaTime;
    }

    //Jump
    void Jump(float vertical)
    {
        if (vertical > 0 && groundCheck)
        {
            rigidbody2D.velocity = new Vector2(0, vertical);
        }
    }

    //Fall

    void Fall()
    {

    }

    void Charge()
    {
        if (isCharge)
        {
            if (parameter.speed == speedMemory)
            {
                parameter.speed *= parameter.chargeMulti;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.transform.tag)
        {
            case "Ground":
                groundCheck = true;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.transform.tag)
        {
            case "Ground":
                groundCheck = false;
                break;
        }
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2DScr : MonoBehaviour
{
    #region - Parameter -

    public CharacterControllerParameter parameter;
    new Rigidbody2D rigidbody2D;
    SpriteRenderer playerSprite;
    bool groundCheck = false;

    public enum States
    {
        idle,
        jump,
        charge
    }

     public States playerState;

    //Variable
    bool isCharge = false;

    //VariableMemory
    float speedMemory;

    #endregion

    void Start()
    {
        speedMemory = parameter.speed;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Keyboard Input
        float horizontalValue = Input.GetAxis("Horizontal");
        float verticalValue = Input.GetAxis("Vertical");

        //Input
        if(horizontalValue != 0 )
        {
            if(horizontalValue > 0)
            {
                transform.localScale = new Vector3(- 1, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
            Move(horizontalValue);
        }

        //Jump
        if (verticalValue != 0 && groundCheck)
        {
            Jump(parameter.jumpStr);
        }

        //Charge
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

        //GroundCheck
        if(!groundCheck)
        {
            playerState = States.jump;
        }
        else if(!isCharge)
        {
            playerState = States.idle;
        }
    }

    #region - Movements -

    //Move
    void Move(float horizontal)
    {
        var movePos = horizontal * parameter.speed;
        transform.position += new Vector3(movePos, 0, 0) * Time.deltaTime;

        playerState = States.idle;
    }

    //Jump
    void Jump(float vertical)
    {
        if (vertical > 0 && groundCheck)
        {
            rigidbody2D.velocity = new Vector2(0, vertical);
        }
    }

    void Charge()
    {
        if (isCharge)
        {
            if (parameter.speed == speedMemory)
            {
                parameter.speed *= parameter.chargeMulti;
            }
            playerState = States.charge;
        }
    }
    #endregion

    #region - Trigger Events -

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

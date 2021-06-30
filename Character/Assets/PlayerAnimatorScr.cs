using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerAnimatorScr : MonoBehaviour
{
    Animator playerAnimator;
    CharacterController2DScr controlls;

    void Start()
    {
        controlls = GetComponent<CharacterController2DScr>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        switch(controlls.playerState)
        {
            case CharacterController2DScr.States.idle:
                playerAnimator.SetBool("isRunning", false);
                playerAnimator.SetBool("isAir", false);
                break;
            case CharacterController2DScr.States.charge:
                playerAnimator.SetBool("isRunning", true);
                playerAnimator.SetBool("isAir", false);
                break;
            case CharacterController2DScr.States.jump:
                playerAnimator.SetBool("isRunning", false);
                playerAnimator.SetBool("isAir", true);
                break;
        }
    }
}

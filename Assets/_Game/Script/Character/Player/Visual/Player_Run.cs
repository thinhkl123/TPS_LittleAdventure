using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Run : StateMachineBehaviour
{
    PlayerVFXManager playerVFXManager;
    bool isRunning;
    bool isIdle;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerVFXManager = animator.GetComponent<PlayerVFXManager>();
        isRunning = false;
        isIdle = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float speedValue = animator.GetFloat("Speed");

        if (speedValue > 0)
        {
            if (isIdle)
            {
                SoundManager.Ins.Play("Move");

            }
            isIdle = false;
        }
        else
        {
            SoundManager.Ins.Stop("Move");
            isIdle = true;
        }

        if (speedValue > 0.2f)
        {
            if (!isRunning)
            {
                playerVFXManager.UpdateFootStepVFX(true);
                //SoundManager.Ins.Play("Move");
            }

            isRunning = true;
        }
        else
        {
            playerVFXManager.UpdateFootStepVFX(false);
            //SoundManager.Ins.Stop("Move");
            isRunning = false;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerVFXManager.UpdateFootStepVFX(false);
        SoundManager.Ins.Stop("Move");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

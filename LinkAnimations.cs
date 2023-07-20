using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkAnimations : MonoBehaviour
{
    public enum PlayerStates
    {
        Idle,
        Move,
        Hurt,
        SwordSlam,
        GarenSlash
    }

    public PlayerStates CurrentState
    {
        set
        {
            currentState = value;
            if (!stateLock || animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                stateLock = false;
                switch (currentState)
                {
                    case PlayerStates.Idle:
                        animator.Play("idle");
                        break;
                    case PlayerStates.Move:
                        animator.Play("movement");
                        break;
                    case PlayerStates.Hurt:
                        stateLock = true;
                        canMove = false;
                        animator.Play("hurt");
                        break;
                    case PlayerStates.SwordSlam:
                        stateLock = true;
                        canMove = false;
                        animator.Play("sword slam");
                        break;
                    case PlayerStates.GarenSlash:
                        stateLock = true;
                        canMove = true;
                        animator.Play("link garen slash");
                        break;
                }
            }
        }
    }

    private PlayerStates currentState;

    private Animator animator;
    public bool stateLock;
    public bool canMove;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        stateLock = false;
        canMove = true;
    }
}

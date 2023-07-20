using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LinkCombat : MonoBehaviour
{
    private LinkAnimations LinkAnimations;

    [HideInInspector] public bool garenSlash;

    private void Awake()
    {
        LinkAnimations = GetComponent<LinkAnimations>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Keypad1))
        {
            LinkAnimations.CurrentState = LinkAnimations.PlayerStates.SwordSlam;
        }

        if (Input.GetKey(KeyCode.Keypad2))
        {
            LinkAnimations.CurrentState = LinkAnimations.PlayerStates.GarenSlash;
        }
    }
}

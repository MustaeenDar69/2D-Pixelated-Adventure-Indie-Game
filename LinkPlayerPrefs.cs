using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkPlayerPrefs : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetFloat("linkHealth", 100f);
        PlayerPrefs.SetFloat("normalSwordDmg", 5f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LinkHealthSystem : MonoBehaviour
{

    [SerializeField] private Image fillBar;

    public float health;

    private void Update()
    {
        health = PlayerPrefs.GetFloat("linkHealth");
        fillBar.fillAmount = health / 100;
    }
}

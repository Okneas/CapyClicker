using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Clicker : MonoBehaviour, IPointerClickHandler
{
    float timer = 0;
    AudioSource sound;
    Animator animator;
    public Player player;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        sound.Play();
        player.capyPoints += player.capyPointsPerTap;
        player.AllCapyPointsPerRun += player.capyPointsPerTap;
    }
    private void Update()
    {
        if (timer < 1)
            timer += Time.deltaTime;

        if (timer > 1)
        {
            Sum();
            timer = 0;
        }
    }

    public void Sum()
    {
        player.capyPoints += player.capyPointsPerSecond;
        player.AllCapyPointsPerRun += player.capyPointsPerSecond;
    }
}

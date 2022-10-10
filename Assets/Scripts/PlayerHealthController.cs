using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth, maxHealth;

    public float invincibleLength;
    private float invincibleCounter;

    private SpriteRenderer theSR;

    public float blinkLength;
    private float blinkCounter;
    private bool blinkOff;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;
            blinkCounter -= Time.deltaTime;

            if (blinkCounter <= 0)
            {
                blinkOff = !blinkOff;
                blinkCounter = blinkLength;
                Blink();
            }

            if (invincibleCounter <= 0)
            {
                blinkOff = false;
                Blink();
            }
        }
    }

    public void DealDamage()
    {
        if (invincibleCounter <= 0)
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                LevelManager.instance.RespawnPlayer();
            }
            else
            {
                invincibleCounter = invincibleLength;
                blinkCounter = blinkLength;
                blinkOff = true;
                Blink();

                PlayerController.instance.KnockBack();
            }

            UIController.instance.UpdateHealthDisplay();
        }
    }

    public void InstantDeath()
    {
        currentHealth = 0;
        UIController.instance.UpdateHealthDisplay();
    }

    private void Blink()
    {
        if (blinkOff)
        {
            theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .2f);
        }
        else
        {
            theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
        }
    }
}

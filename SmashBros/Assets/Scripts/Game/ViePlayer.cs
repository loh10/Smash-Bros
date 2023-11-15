using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ViePlayer : MonoBehaviour
{
    public MultiplayerManager mpm;
    int currentLife = 3;
    public List<Transform> Vie;
    public bool dead;

    private void Awake()
    {
        Time.timeScale = 1;
        mpm = GameObject.Find("EventManager").GetComponent<MultiplayerManager>();
        AddAllLife();
    }
    private void Start()
    {
        ActivateInput(this.GetComponent<PlayerInput>());
    }
    private void AddAllLife()
    {
        for (int i = 1; i < 4; i++)
        {
            Vie.Add(GameObject.Find("P"+mpm.Nbplayer+"vie"+i.ToString()).transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "border")
        {
            if (currentLife == 1)
            {
                clearLife();
                DesactivateInput(this.GetComponent<PlayerInput>());
                this.gameObject.transform.position = new Vector2(1000, 1000);
                Destroy(this.gameObject.GetComponent<Rigidbody2D>());
                this.gameObject.tag = "Dead";
                dead = true;
            }
            else
            {
                this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                dead = true;
                currentLife--;
                UpdateLife();
                this.gameObject.transform.position = Vector3.zero;
            }
        }
    }

    void DesactivateInput(PlayerInput playerInput)
    {
        playerInput.DeactivateInput();
    }

    void ActivateInput(PlayerInput playerInput)
    {
        playerInput.ActivateInput();
    }
    private void UpdateLife()
    {
        clearLife();
        for (int i = 0; i < currentLife; i++)
        {
            Vie[i].gameObject.SetActive(true);
        }
    }

    private void clearLife()
    {
        for (int i = 0; i < Vie.Count; i++)
        {
            Vie[i].gameObject.SetActive(false);
        }
    }
}

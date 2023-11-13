using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViePlayer : MonoBehaviour
{
    public MultiplayerManager mpm;
    int currentLife = 3;
    public List<Transform> Vie;

    private void Awake()
    {
        mpm = GameObject.Find("EventManager").GetComponent<MultiplayerManager>();
        AddAllLife();
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
                this.gameObject.SetActive(false);
                print("mort");
            }
            else
            {
                print(currentLife);
                this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                this.gameObject.GetComponent<expulsionPercentage>().setCharacterExpulsionPercentage(0);
                currentLife--;
                UpdateLife();
                this.gameObject.transform.position = Vector3.zero;
            }
        }
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

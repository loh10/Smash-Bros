using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PlateformMove : MonoBehaviour
{
    public GameObject platform;
    public GameObject wp1,wp2;
    GameObject currentWp;
    public int speed;
    private void Awake()
    {
        currentWp = wp1;
    }
    private void Update()
    {
        if(currentWp == wp1)
        {
            if(platform.transform.position.x <= currentWp.transform.position.x)
            {
                currentWp = wp2;
            }
            platform.transform.Translate(Vector2.left*speed * Time.deltaTime);
        }
        else
        {
            if (platform.transform.position.x >= currentWp.transform.position.x)
            {
                currentWp = wp1;
            }
            platform.transform.Translate(Vector2.right*speed * Time.deltaTime);
        }
    }

}

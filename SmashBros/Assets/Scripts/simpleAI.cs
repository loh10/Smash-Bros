using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleAI : MonoBehaviour
{
    void Update()
    {
            Vector2 targetPosition = new Vector2(0,transform.position.y);
            Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, 5 * Time.deltaTime);
            transform.position = newPosition;
    }
}

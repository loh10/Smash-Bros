using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    void Start()
    {
        dataTransfer.maxJumpHeight = 5f;
        dataTransfer.maxJumpTime = 0.5f;
        dataTransfer.maxJumpsCount = 2;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCapper : MonoBehaviour
{
    public int targetFPS = 90;

    void Awake()
    {
        Application.targetFrameRate = targetFPS;
    }
}

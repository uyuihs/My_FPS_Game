using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryAfterATime : MonoBehaviour
{
    private const float dTime = 5f;

    private void Awake()
    {
        Destroy(gameObject, dTime);
    }
}

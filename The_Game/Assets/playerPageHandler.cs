using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPageHandler : MonoBehaviour
{

    public GameObject player;
    void Start()
    {
        Instantiate(player);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private static GameSettings _instance;
    public static GameSettings Instance { get { return _instance; } }

    public float speed { get; private set; }
    public int oob { get; private set; }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        speed = 8;
        oob = -16;
    }

    private void Update()
    {
        if (speed < 20)
            speed += 0.15f * Time.deltaTime;
    }
}

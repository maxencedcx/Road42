using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingPlane : MonoBehaviour
{
    [SerializeField] private int id = 0;

    private void Start()
    {
        Looper.Instance.RegisterPlane(this.id, this.transform);
        Destroy(this);
    }
}

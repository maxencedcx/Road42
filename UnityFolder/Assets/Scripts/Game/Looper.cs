using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looper : MonoBehaviour
{
    private static Looper _instance;
    public static Looper Instance { get { return _instance; } }

    private Dictionary<int, Transform> _planes = new Dictionary<int, Transform>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Update()
    {
        float modifier = Time.deltaTime;
        float value = 1 * modifier * GameSettings.Instance.speed;

        for (int i = 0; i < _planes.Count; ++i)
        {
            _planes[i].position = new Vector3(0, -0.2f, _planes[i].position.z - value);
            if (_planes[i].position.z < (GameSettings.Instance.oob))
                _planes[i].position = new Vector3(0, -0.2f, _planes[(i + 1) % _planes.Count].position.z +
                    ((i == (_planes.Count - 1)) ? (8) : (8 - value)));
        }
    }

    public void RegisterPlane(int i, Transform t)
    {
        _planes.Add(i, t);
    }
}

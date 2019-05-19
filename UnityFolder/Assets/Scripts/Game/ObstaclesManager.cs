using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ObstaclesManager : MonoBehaviour
{
    private class Obstacle
    {
        public int id { get; set; }
        public Vector3 initialPos { get; set; }
        public Transform transform { get; set; }

        public Obstacle(Vector3 p, Transform t)
        {
            id = ++obstacleId;
            initialPos = p;
            transform = t;
        }
    }

    private static ObstaclesManager _instance;
    public static ObstaclesManager Instance { get { return _instance; } }
    public static int obstacleId = 0;

    private static List<float> possibleXSpawns = new List<float> { 1.9f, 5.7f, -1.9f, -5.7f};
    [SerializeField] private GameObject obstaclePrefab = null;
    [SerializeField] private int numberOfObstacles = 4;

    private System.Random r;
    private List<Obstacle> _obstacles = new List<Obstacle>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        r = new System.Random();
        for (int i = 0; i < numberOfObstacles; ++i)
        {
            Vector3 pos = GetRandomAvailableSpawn(obstacleId);
            GameObject obj = Instantiate(obstaclePrefab, pos, Quaternion.identity, transform);
            _obstacles.Add(new Obstacle(pos, obj.transform));
        }
    }

    private void Update()
    {
        float modifier = Time.deltaTime;
        float value = 1 * modifier * GameSettings.Instance.speed;

        foreach (var obstacle in _obstacles)
        {
            obstacle.transform.position = new Vector3(obstacle.transform.position.x, 0.75f, obstacle.transform.position.z - value);
            if (obstacle.transform.position.z <= GameSettings.Instance.oob)
            {
                Vector3 pos = GetRandomAvailableSpawn(obstacle.id);
                obstacle.transform.position = pos;
                obstacle.initialPos = pos;
            }
        }
    }

    private Vector3 GetRandomAvailableSpawn(int id)
    {
        float Zpos = (_obstacles.Count == 0) ? (12) : (_obstacles.Max(x => x.transform.position.z) + 6);
        Vector3 pos = new Vector3(possibleXSpawns[r.Next(possibleXSpawns.Count)], 0.75f, Zpos);
        return pos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    [SerializeField] GameObject obstaclePrefab = null;

    private List<Obstacle> _obstacles = new List<Obstacle>();
    private List<Vector3> _availableSpawns = new List<Vector3>()
    {
        {new Vector3(-5.7f, 0.75f, 12)},
        {new Vector3(-1.9f, 0.75f, 12)},
        {new Vector3(1.9f, 0.75f, 12)},
        {new Vector3(5.7f, 0.75f, 12)},
        {new Vector3(-5.7f, 0.75f, 18)},
        {new Vector3(-1.9f, 0.75f, 18)},
        {new Vector3(1.9f, 0.75f, 18)},
        {new Vector3(5.7f, 0.75f, 18)}
    };

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 4; ++i)
        {
            Vector3 pos = GetRandomAvailableSpawn(obstacleId);
            GameObject obj = Instantiate(obstaclePrefab, pos, Quaternion.identity);
            _obstacles.Add(new Obstacle(pos, obj.transform));
        }

    }

    private void Update()
    {
        float modifier = Time.deltaTime;
        float value = 1 * modifier * GameSettings.speed;

        foreach (var obstacle in _obstacles)
        {
            obstacle.transform.position = new Vector3(obstacle.transform.position.x, 0.75f, obstacle.transform.position.z - value);
            if (obstacle.transform.position.z <= GameSettings.oob)
            {
                _availableSpawns.Add(obstacle.initialPos);
                Vector3 pos = GetRandomAvailableSpawn(obstacle.id);
                obstacle.transform.position = pos;
                obstacle.initialPos = pos;
            }
        }
    }

    private Vector3 GetRandomAvailableSpawn(int id)
    {
        List<Vector3> tmpList = _availableSpawns.Where(x => x.z == ((id < 3) ? (12) : (18))).ToList();
        Vector3 pos = tmpList[Random.Range(0, tmpList.Count - 1)];
        _availableSpawns.Remove(pos);
        return pos;
    }

}

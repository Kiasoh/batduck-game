using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
public class HighwayTrafficManager : MonoBehaviour
{
    [Header("Traffic Settings")]
    public GameObject[] carPrefabs;
    // public Transform player;
    public float spawnDistance = 100f;
    public float removeDistance = 50f;
    public int maxTrafficCars = 15;
    public const float minGap = 20f; // min distance from other cars

    // Lane centers in world X (tune these to match your road)
    public float[] laneX = new float[] { -80f, 0f, 80f };

    [Header("Road Setting")]
    public GameObject roadPrefab;
    public GameObject highway;

    private List<GameObject> activeCars = new List<GameObject>();

    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("player not found");
        }
        
    }

    private int last_segment = 0;
    void Update()
    {
        // 1. Remove cars behind player
        for (int i = activeCars.Count - 1; i >= 0; i--)
        {
            if (player.transform.position.z - activeCars[i].transform.position.z > removeDistance)
            {
                Destroy(activeCars[i]);
                activeCars.RemoveAt(i);
            }
        }

        // 2. Maintain traffic density
        if (activeCars.Count < maxTrafficCars)
        {
            SpawnTrafficForward();
        }

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb.position.z / 20f > last_segment + 1)
        {
            last_segment += 1;
            Instantiate(roadPrefab, new Vector3(0, 0 , ((int)(rb.position.z / 20))* 20 + 100), new Quaternion());
        }
    }

    void SpawnTrafficForward()
    {

        for (int attempts = 0; attempts < 5; attempts++)
        {
            int laneIndex = Random.Range(0, laneX.Length);
            float x = laneX[laneIndex];
            float extraZ = Random.Range(0f, 50f);
            float y = 0f;

            Vector3 spawnPos = new Vector3(x, y, player.transform.position.z + spawnDistance + extraZ);

            bool tooClose = false;
            foreach (var car in activeCars)
            {
                if (Vector3.Distance(car.transform.position, spawnPos) < minGap)
                {
                    tooClose = true;
                    break;
                }
            }

            if (tooClose)
                continue; // try another position

            GameObject prefab = carPrefabs[Random.Range(0, carPrefabs.Length)];
            GameObject newCar = Instantiate(prefab, spawnPos, Quaternion.identity);
            activeCars.Add(newCar);
            return;
        }
    }
}

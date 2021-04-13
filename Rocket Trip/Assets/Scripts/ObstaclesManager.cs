using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    public Transform[] edges;
    public float worldSpeed = 1f;

    [Header("Meteorites")]
    public GameObject meteoritePrefab;
    public float meteoritesPoolLength = 5f;
    private List<GameObject> meteoritesPool = new List<GameObject>();
    public Transform meteoritesPoolObject;

    // Start is called before the first frame update
    void Start()
    {
        CreateMeteoritesPool();
        InvokeRepeating("SpawnMeteorite", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        EdgesMovementLoop();
    }

    void LateUpdate()
    {
        ControlMeteorites();
    }

    void EdgesMovementLoop()
    {
        for(int i = 0; i < edges.Length; i++)
        {
            Vector3 newPos = edges[i].position;
            newPos.y -= worldSpeed * Time.deltaTime;
            edges[i].position = newPos;

            //Rellocate at position 10
            if(edges[i].position.y <= -10f)
            {
                Vector3 rellocatePos = edges[i].position;
                rellocatePos.y = 10f;
                edges[i].position = rellocatePos;
            }
        }
    }

    #region Meteorites System
    void CreateMeteoritesPool()
    {
        for(int i = 0; i < meteoritesPoolLength; i++)
        {
            GameObject meteorite = Instantiate(meteoritePrefab, new Vector3(0f,7f,0f), Quaternion.identity, meteoritesPoolObject);
            meteorite.SetActive(false);
            meteoritesPool.Add(meteorite);
        }
    }

    void SpawnMeteorite()
    {
        float meteoriteRandomX = Random.Range(-7f, 7f);
        Vector3 meteoritePos = new Vector3(meteoriteRandomX, 7f, 0f);
        Quaternion meteoriteRot = new Quaternion(0f, 0f, 0f, 0f);
        float meteoriteRandomYRot = Random.Range(0f, 360f);
        meteoriteRot.eulerAngles = new Vector3(0f, meteoriteRandomYRot, 0f);
        
        for(int i = 0; i < meteoritesPool.Count; i++)
        {
            if(!meteoritesPool[i].activeInHierarchy)
            {
                meteoritesPool[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                meteoritesPool[i].transform.position = meteoritePos;
                meteoritesPool[i].transform.rotation = meteoriteRot;
                meteoritesPool[i].SetActive(true);
                break;
            }
        }
    }

    void ControlMeteorites()
    {
        for (int i = 0; i < meteoritesPool.Count; i++)
        {
            if (meteoritesPool[i].activeInHierarchy && meteoritesPool[i].transform.position.y <= -7f)
            {
                meteoritesPool[i].SetActive(false);
            }
        }
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    public Transform[] edges;
    public float worldSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EdgesMovementLoop();
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
}

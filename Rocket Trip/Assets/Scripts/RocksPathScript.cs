using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocksPathScript : MonoBehaviour
{
    [SerializeField]
    private GameObject[] blockers;
    public float rocksSpeed = 5f;

    private void OnEnable()
    {
        int blockerIndex = Random.Range(0, 3); //0 = Left, 1 = Middle, 2 = Right
        InstructionsManager.instance.ActivateInstruction(1, blockerIndex, null);
        for(int i = 0; i < blockers.Length; i++)
        {
            blockers[i].SetActive(true);
        }
        Vector3 newPos = new Vector3(0f, 7f, 0f);
        transform.position = newPos;
        blockers[blockerIndex].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameController.instance.gameOver)
        {
            MoveRocks();
        }
    }

    void MoveRocks()
    {
        Vector3 mov = transform.position;
        mov.y -= rocksSpeed * Time.deltaTime;
        transform.position = mov;

        if(transform.position.y <= -20f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Rocket"))
        {
            //Add score
            Debug.Log("Rocks passed");
            GameController.instance.obstacles.ObstacleJustEnded();
        }
    }
}

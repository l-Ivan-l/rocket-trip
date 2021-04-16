using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsScript : MonoBehaviour
{
    [SerializeField]
    private GameObject[] stars;
    private string safeStar;
    public float starsSpeed = 5f;
    public float rotSpeed = 6f;
    private List<Vector3> starsPositions = new List<Vector3>();

    private void OnEnable()
    {
        int starIndex = Random.Range(0, 4); //0 = Yellow, 1 = Blue, 2 = Green, 3 = Orange
        safeStar = stars[starIndex].tag;
        InstructionsManager.instance.ActivateInstruction(4, 0, safeStar); 

        Vector3 newPos = new Vector3(0f, 7f, 0f);
        transform.position = newPos;
        RandomizeStars();
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(true);
        }
        Debug.Log("Safe star: " + safeStar);
        GameController.instance.currentSafeStar = safeStar;
    }

    private void Awake()
    {
        for(int i = 0; i < stars.Length; i++)
        {
            starsPositions.Add(stars[i].transform.localPosition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameController.instance.gameOver)
        {
            MoveStars();
        }
        RotateStars();
    }

    void RandomizeStars()
    {   
        for(int i = 0; i < stars.Length; i++)
        {
            int randomIndex = Random.Range(0, stars.Length);
            stars[i].transform.SetSiblingIndex(randomIndex);
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).localPosition = starsPositions[i];
        }
    }

    void MoveStars()
    {
        Vector3 mov = transform.position;
        mov.y -= starsSpeed * Time.deltaTime;
        transform.position = mov;

        if (transform.position.y <= -7f)
        {
            gameObject.SetActive(false);
        }
    }

    void RotateStars()
    {
        for(int i = 0; i < stars.Length; i++)
        {
            Vector3 rot = stars[i].transform.eulerAngles;
            rot.z += rotSpeed * Time.deltaTime;
            stars[i].transform.eulerAngles = rot;
        }
    }

    bool CheckIfRocketGrabbedStar()
    {
        int nStars = 0;
        for (int i = 0; i < stars.Length; i++)
        {
            if(stars[i].activeInHierarchy)
            {
                nStars++;
            }
        }

        return nStars < 4;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Rocket") && CheckIfRocketGrabbedStar())
        {
            //Add score
            Debug.Log("Stars passed");
            GameController.instance.obstacles.ObstacleJustEnded();
        } else
        {
            GameController.instance.obstacles.StarObstacleEndedWithoutScore();
        }
    }
}

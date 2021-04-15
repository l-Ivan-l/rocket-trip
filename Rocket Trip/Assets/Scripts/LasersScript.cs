using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LasersScript : MonoBehaviour
{
    [SerializeField]
    private GameObject[] lasers;
    public bool horizontal;

    private void OnEnable()
    {
        int laserIndex = Random.Range(0, 3); //0 = Left, 1 = Middle, 2 = Right
        if (horizontal) //0 = Top, 1 = Middle, 2 = Bottom
        {
            InstructionsManager.instance.ActivateInstruction(3, laserIndex, null);
        } else
        {
            InstructionsManager.instance.ActivateInstruction(2, laserIndex, null);
        }

        StartCoroutine(ActivateLasers(laserIndex));
    }

    IEnumerator ActivateLasers(int _laserIndex)
    {
        Debug.Log(_laserIndex);
        yield return new WaitForSecondsRealtime(1.5f);
        Debug.Log("Activate lasers");
        for (int i = 0; i < lasers.Length; i++)
        {
            lasers[i].SetActive(true);
        }
        lasers[_laserIndex].SetActive(false);

        StartCoroutine(DeactivateLasers());
    }

    IEnumerator DeactivateLasers()
    {
        yield return new WaitForSeconds(1f);
        //Add score if Rocket survived

        GameController.instance.obstacles.ObstacleJustEnded();
        for (int i = 0; i < lasers.Length; i++)
        {
            lasers[i].SetActive(false);
        }
        gameObject.SetActive(false);
    }
}

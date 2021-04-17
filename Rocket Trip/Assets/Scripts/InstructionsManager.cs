using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsManager : MonoBehaviour
{
    public static InstructionsManager instance;

    public Text textDisplay;
    private int index;
    public float typingSpeed = 0.1f;
    private string instruction = ". . .";
    private string[] verticalVariation = { "Left", "Middle", "Right" };
    private string[] horizontalVariation = { "Top", "Middle", "Bottom" };

    public GameObject postProcessingObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        textDisplay.text = ". . .";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateInstruction(int _obstacle, int _variation, string _color)
    {
        SoundManager.instance.PlayInstructionSound(1f);
        postProcessingObject.SetActive(true);
        Time.timeScale = 0.5f;
        switch(_obstacle)
        {
            case 1: //Rock paths
                instruction = "Go through the " + verticalVariation[_variation] + " path to avoid a crash.";
                break;
            case 2: //Vertical Lasers
                instruction = "Move your ship to the " + verticalVariation[_variation] + " to avoid the lasers.";
                break;
            case 3: //Horizontal Lasers
                instruction = "Move your ship to the " + horizontalVariation[_variation] + " to avoid the lasers.";
                break;
            case 4: //Stars
                instruction = "Collect the " + _color + " star.";
                break;
        }

        StartCoroutine(TypeEffect());
    }

    IEnumerator TypeEffect()
    {
        textDisplay.text = "";
        foreach (char letter in instruction.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        postProcessingObject.SetActive(false);
    }

    public void DeactivateInstruction()
    {
        textDisplay.text = ". . .";
    }
}

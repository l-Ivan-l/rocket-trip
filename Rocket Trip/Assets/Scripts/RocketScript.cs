using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    private InputMaster inputMaster;
    private Transform rocketMesh;
    private Renderer rocketRender;
    private float animSpeed = 100f;

    private Vector2 rocketDir;
    private Rigidbody rocketBody;
    public float rocketSpeed = 400f;

    private float steeringAngle = 20f;

    private bool dead;
    public ParticleSystem explosionVFX;

    void Awake()
    {
        rocketMesh = transform.GetChild(0);
        rocketRender = transform.GetChild(0).GetComponent<Renderer>();
        rocketBody = GetComponent<Rigidbody>();
        SetUpInputs();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void SetUpInputs()
    {
        inputMaster = new InputMaster();
    }

    void OnEnable()
    {
        inputMaster.Enable();
    }

    void OnDisable()
    {
        inputMaster.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        RocketAnimation();
        rocketDir = inputMaster.RocketActions.Move.ReadValue<Vector2>();
        RocketRotation(rocketDir.x);
    }


    void FixedUpdate()
    {
        RocketMove(rocketDir);
    }

    void LateUpdate()
    {
        if (!rocketRender.isVisible)
        {
            Debug.Log("Kill Rocket");
        }
    }

    void RocketAnimation()
    {
        rocketMesh.Rotate(0f, 0f, animSpeed * Time.deltaTime, Space.Self);
    }

    void RocketMove(Vector2 _direction)
    {
        rocketBody.velocity = _direction * rocketSpeed * Time.fixedDeltaTime;
    }

    void RocketRotation(float _direction)
    {
        Vector3 newRot = transform.eulerAngles;
        float angle = Mathf.LerpAngle(newRot.z, steeringAngle * -_direction, Time.deltaTime * 10f);
        newRot.z = angle;
        transform.eulerAngles = newRot;
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Kill Rocket");
        if(!dead)
        {
            KillRocket();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Laser") && !dead)
        {
            KillRocket();
        }
    }

    void KillRocket()
    {
        Time.timeScale = 1f;
        explosionVFX.transform.position = this.transform.position;
        explosionVFX.Play();
        dead = true;
        GameController.instance.GameOver();
        gameObject.SetActive(false);
    }
}

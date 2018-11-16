using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour {

    public PipeSystem pipeSystem;
    public Pipe currentPipe;
    public float distanceTraveled;
    private float deltaToRotation;
    private float systemRotation;
    private Transform world, rotater;
    private float worldRotation, avatarRotation;
    public float rotationVelocity;
    public MainMenu mainMenu;
    //public float startVelocity;
    //public float[] accelerations;
    public float acceleration, velocity;
    public float period = 0.0f;
    public Text scoreLabel;


    // Use this for initialization
    public void StartGame()
    {
        distanceTraveled = 0f;
        avatarRotation = 0f;
        systemRotation = 0f;
        worldRotation = 0f;
        velocity = 4f;
        currentPipe = pipeSystem.SetupFirstPipe();
        SetupCurrentPipe();
        pipeSystem.resetMaterial();
        gameObject.SetActive(true);       
        
    }

    private void Awake()
    {
        world = pipeSystem.transform.parent;
        rotater = transform.GetChild(0);
        gameObject.SetActive(false);      
    }

    private void Update() {
        float delta = velocity * Time.deltaTime;
        distanceTraveled += delta;
        systemRotation += delta * deltaToRotation;
        SpeedIncrease();
        pipeSystem.period = period;

        if (systemRotation >= currentPipe.CurveAngle)
        {
            delta = (systemRotation - currentPipe.CurveAngle) / deltaToRotation;
            currentPipe = pipeSystem.SetupNextPipe();
            SetupCurrentPipe();
            systemRotation = delta * deltaToRotation;
        }

        pipeSystem.transform.localRotation =
            Quaternion.Euler(0f, 0f, systemRotation);
        UpdateAvatarRotation();
    }

    public void SpeedIncrease()
    {
        if (period > 30f)
        {
            //Do Stuff
            velocity += 0.5f;
            period = 0;
        }
        period += Time.deltaTime;
    }

    private void UpdateAvatarRotation()
    {
        avatarRotation +=
            rotationVelocity * Time.deltaTime * Input.GetAxis("Horizontal");
        if (avatarRotation < 0f)
        {
            avatarRotation += 360f;
        }
        else if (avatarRotation >= 360f)
        {
            avatarRotation -= 360f;
        }
        rotater.localRotation = Quaternion.Euler(avatarRotation, 0f, 0f);
    }

    private void SetupCurrentPipe()
    {
        deltaToRotation = 360f / (2f * Mathf.PI * currentPipe.CurveRadius);
        worldRotation += currentPipe.RelativeRotation;
        if (worldRotation < 0f)
        {
            worldRotation += 360f;
        }
        else if (worldRotation >= 360f)
        {
            worldRotation -= 360f;
        }
        world.localRotation = Quaternion.Euler(worldRotation, 0f, 0f);
    }

    public void Die()
    {
        mainMenu.EndGame(distanceTraveled);
        period = 0;
        gameObject.SetActive(false);        
    }
}

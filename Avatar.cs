using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour {

    public ParticleSystem.EmissionModule trail;
    public float deathCountdown = -1f;
    public float speedtimer = 0f;
    public float slowtimer = 0f;
    private Player player;
    public bool speedtimerstart = false;
    public bool slowtimerstart = false;
    public float num = 0f;

    private void Awake()
    {
        player = transform.root.GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider collider)
    {

        if (collider.tag == "Obstacles")
        {
            if (deathCountdown < 0f)
            {
                //shape.enabled = false;
                //trail.enabled = false;
                //burst.Emit(burst.maxParticles);
                deathCountdown = 1f;//burst.startLifetime;
                Debug.Log("Working");
            }
        }

        if (collider.tag == "Speed Power Up" && speedtimer <= 2.5f)
        {
            speedtimer = 0f;
            speedtimerstart = true;
            num += 1f;          
            player.velocity += num;
            Destroy(collider.gameObject);


        } else if (collider.tag == "Slow Power Up" && slowtimer <= 2.5f)
        {
            slowtimer = 0f;           
            slowtimerstart = true;
            Time.timeScale = 0.5f;
            Destroy(collider.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    private void Update()
    {
        if (deathCountdown >= 0f)
        {
            deathCountdown -= 0.25f;//Time.deltaTime;
            if (deathCountdown <= 0f)
            {
                deathCountdown = -1f;
                player.Die();
                speedtimer = 0;
                slowtimer = 0;
                num = 0;
                speedtimerstart = false;
                slowtimerstart = false;
            }
        }

        SpeedUp();
        SlowDown();

    }

    private void SpeedUp()
    {
        if (speedtimer >= 10f && speedtimerstart == true)
        {
            speedtimer = 0f;
            speedtimerstart = false;
            player.velocity -= num;
            num = 0;
            
        }
        if (speedtimerstart == true)
        {
            speedtimer += Time.deltaTime;
        }
        else if (speedtimerstart == false)
        {
            //player.velocity -= 2;
        }
    }

    private void SlowDown()
    {
        if (slowtimer >= 10f && slowtimerstart == true)
        {
            slowtimer = 0f;
            slowtimerstart = false;
            Time.timeScale = 1f;           
        }
        if (slowtimerstart == true)
        {
            slowtimer += Time.deltaTime;
        }
        else if (slowtimerstart == false)
        {
            Time.timeScale = 1f;
        }
    }
}

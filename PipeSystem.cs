using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSystem : MonoBehaviour {

    public Pipe pipePrefab;
    public int pipeCount;
    private Player player;
    public GameObject playerobject;
    private Pipe[] pipes;
    public int emptyPipeCount;
    //public Material[] m_Material;
    public Texture[] m_textures;
    public int currentTexture;
    //public Renderer rend;
    public float period = 0.0f;

    private void Awake()
    {
        gameObject.SetActive(false);

        pipes = new Pipe[pipeCount];       
        for (int i = 0; i < pipes.Length; i++)
        {
            Pipe pipe = pipes[i] = Instantiate<Pipe>(pipePrefab);
            pipe.transform.SetParent(transform, false);           
        }
    }

    public Pipe SetupFirstPipe()
    {
        for (int i = 0; i < pipes.Length; i++)
        {
            Pipe pipe = pipes[i];
            pipe.Generate(i > emptyPipeCount);
            if (i > 0)
            {
                pipe.AlignWith(pipes[i - 1]);
            }
        }
        AlignNextPipeWithOrigin();
        resetMaterial();
        transform.localPosition = new Vector3(0f, -pipes[1].CurveRadius);
        return pipes[1];
    }

    public Pipe SetupNextPipe()
    {
        ShiftPipes();
        AlignNextPipeWithOrigin();
        pipes[pipes.Length - 1].Generate();
        pipes[pipes.Length - 1].AlignWith(pipes[pipes.Length - 2]);
        transform.localPosition = new Vector3(0f, -pipes[1].CurveRadius);
        return pipes[1];
    }

    private void ShiftPipes()
    {
        Pipe temp = pipes[0];
        for (int i = 1; i < pipes.Length; i++)
        {
            pipes[i - 1] = pipes[i];
        }
        pipes[pipes.Length - 1] = temp;
    }

    private void AlignNextPipeWithOrigin()
    {
        Transform transformToAlign = pipes[1].transform;
        for (int i = 0; i < pipes.Length; i++)
        {
            if (i != 1)
            {
                pipes[i].transform.SetParent(transformToAlign);
            }
        }

        transformToAlign.localPosition = Vector3.zero;
        transformToAlign.localRotation = Quaternion.identity;

        for (int i = 0; i < pipes.Length; i++)
        {
            if (i != 1)
            {
                pipes[i].transform.SetParent(transform);
            }
        }
    }

    public void SetMaterial()
    {
        if (period > 30f)
        {
            //Do Stuff       
            currentTexture++;
            currentTexture %= m_textures.Length;
            pipePrefab.GetComponent<Renderer>().sharedMaterial.mainTexture = m_textures[currentTexture];
        }                 
    }

    public void resetMaterial()
    {
        //Do Stuff
        pipePrefab.GetComponent<Renderer>().sharedMaterial.mainTexture = m_textures[0];
    }

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        SetMaterial();
        if (playerobject.activeSelf == false)
        {
            gameObject.SetActive(false);
        }
    }

    public void StartGame()
    {
        gameObject.SetActive(true);

    }

}

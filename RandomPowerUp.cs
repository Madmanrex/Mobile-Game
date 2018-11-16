using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPowerUp : PipeObstacleGenerator{

    public PipeObstacles[] ObstaclePrefabs;
    public int Arr;


    public override void GenerateObstacles(Pipe pipe)
    {
        float angleStep = pipe.CurveAngle / pipe.CurveSegmentCount;
        for (int i = 6; i < pipe.CurveSegmentCount; i++)
        {

            Arr = (Random.Range(0, 100) / 3) % 3;

            if (Arr == 0)
            {
                PipeObstacles obstacle = Instantiate<PipeObstacles>(
                ObstaclePrefabs[0]);
                float pipeRotation =
                (Random.Range(0, pipe.pipeSegmentCount) + 0.5f) *
                360f / pipe.pipeSegmentCount;
                obstacle.Position(pipe, 3 * angleStep, pipeRotation);
            }
            else if (Arr == 1)
            {
                PipeObstacles obstacle = Instantiate<PipeObstacles>(
                ObstaclePrefabs[1]);
                float pipeRotation =
                (Random.Range(0, pipe.pipeSegmentCount) + 0.5f) *
                360f / pipe.pipeSegmentCount;
                obstacle.Position(pipe, 2 * angleStep, pipeRotation);
            } else if (Arr == 3)
            {
                PipeObstacles obstacle = Instantiate<PipeObstacles>(ObstaclePrefabs[2]);
                float pipeRotation =
                (Random.Range(0, pipe.pipeSegmentCount) + 0.5f) *
                360f / pipe.pipeSegmentCount;
                obstacle.Position(pipe, i * angleStep, pipeRotation);
            }        
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

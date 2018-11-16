using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObstaclePlacer : PipeObstacleGenerator{

    public PipeObstacles[] ObstaclePrefabs;
    

    public override void GenerateObstacles(Pipe pipe)
    {
        float angleStep = pipe.CurveAngle / pipe.CurveSegmentCount;
        for (int i = 0; i < pipe.CurveSegmentCount; i++)
        {
            PipeObstacles obstacle = Instantiate<PipeObstacles>(
                ObstaclePrefabs[Random.Range(0, ObstaclePrefabs.Length)]);
            float pipeRotation =
                (Random.Range(0, pipe.pipeSegmentCount) + 0.5f) *
                360f / pipe.pipeSegmentCount;
            obstacle.Position(pipe, i * angleStep, pipeRotation);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBallSpawner : ABonusSpawner<HPBall>
{
    protected override void SetupBonus(HPBall bonus)
    {
        Vector3 startingPosition = transform.position;
        startingPosition.x = Random.Range(-45f, 45f);
        bonus.Reset(startingPosition);
    }

}

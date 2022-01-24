using UnityEngine;

public class WaspSpawner : AEnemySpawner<WaspMovement>
{
    protected override void SetupEnemy(WaspMovement enemy)
    {
        Vector3 startingPosition = transform.position;
        startingPosition.x = Random.Range(-30f, 30f);
        enemy.Reset(startingPosition);
        enemy.PointReward = m_Config.PointsReward;
    }
}

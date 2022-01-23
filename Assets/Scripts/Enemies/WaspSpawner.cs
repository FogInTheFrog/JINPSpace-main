
public class WaspSpawner : AEnemySpawner<WaspMovement>
{
    protected override void SetupEnemy(WaspMovement enemy)
    {
        enemy.Reset(transform.position);
    }
}

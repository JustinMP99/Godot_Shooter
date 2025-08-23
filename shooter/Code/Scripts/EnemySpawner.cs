using Godot;
using System;
using System.Collections.Generic;

public partial class EnemySpawner : Node
{
    
    [ExportCategory("Spawner Components")]
    [Export] private Timer spawnTimer;
    [Export] private PathFollow3D spawnPath;
    
    [ExportCategory("Enemy Data")] 
    [Export] private int desiredEnemies;
    [Export] private PackedScene enemyPrefab;
    [Export] private Node3D enemyContainer;
    [Export] private EnemyStats tankStats;
    [Export] private EnemyStats speedstrStats;
    [Export] private EnemyStats baseStats;
    
    //Pool Data
    private List<EnemyController> enemyPool;
    private List<EnemyController> activeEnemies;
    private int poolIter;
    private int poolIterMax;
    
    /// <summary>
    /// creates pool of enemies and generates enemy stats
    /// </summary>
    /// TODO: Maybe moving stat setting to when the enemy is spawned would allow for better difficulty control
    public void Startup()
    {
        enemyPool = new List<EnemyController>();
        activeEnemies = new List<EnemyController>();
        poolIter = 0;
        poolIterMax = desiredEnemies;

        for (int i = 0; i < desiredEnemies; i++)
        {
            EnemyController enemy = enemyPrefab.Instantiate() as EnemyController;

            int value = (int)GD.RandRange(1.0, 4.0);

            switch (value)
            {
                
                case 1:
                    
                    enemy.Stats = baseStats.Duplicate() as EnemyStats;

                    break;
                    
                case 2:

                    enemy.Stats = speedstrStats.Duplicate() as EnemyStats;
                    
                    break;
                
                case 3:

                    enemy.Stats = tankStats.Duplicate() as EnemyStats;
                    
                    break;
                
            }
            
            enemy.Disable();
            enemyPool.Add(enemy);
            AddChild(enemy);
        }
    }

    public override void _Process(double delta)
    {
        if (!Global.gamePaused)
        {
            MoveActiveEnemies(delta);
        }
    }

    private void MoveActiveEnemies(double delta)
    {

        for (int i = 0; i < activeEnemies.Count; i++)
        {
            activeEnemies[i].MoveEnemy(delta);
        }
        //Remove
        activeEnemies.RemoveAll(e => !e.GetIsActive());

    }
    
    public void DisableAllEnemies()
    {
        for (int i = 0; i < enemyPool.Count; i++)
        {
            enemyPool[i].Disable();
        }
    }
    
    private void OnTimerTimeout()
    {
        //TODO: Fix this! All timers should pause when game is paused. This check should be irrelevant/redundant!
        if (!Global.gamePaused)
        {
            if (desiredEnemies > 0) //TODO: Remove this check, it is a holdover from when enemies were spawned during runtime. Now it is always true
            {
                if (!enemyPool[poolIter].GetIsActive())
                {
                    enemyPool[poolIter].Enable();
                    enemyPool[poolIter].Position = new Vector3((float)GD.RandRange(-6.0, 6.0), 0.0f, -20.0f);
                    activeEnemies.Add(enemyPool[poolIter]);
                    
                    poolIter++;
                    if (poolIter >= poolIterMax)
                    {
                        poolIter = 0;
                    }
                }
            }
        }
    }

    public void StartTimer()
    {
        spawnTimer.Start();
    }
    public void StopTimer()
    {
        spawnTimer.Stop();
    }
    public void PauseTimer()
    {
        spawnTimer.Paused = true;
    }
    public void ResumeTimer()
    {
        spawnTimer.Paused = false;
    }
    
}
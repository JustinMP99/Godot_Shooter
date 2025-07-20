using Godot;
using System;
using System.Collections.Generic;

public partial class EnemySpawner : Node
{
    [Export] private Timer spawnTimer;
    [Export] private PackedScene enemyPrefab;
    [Export] private Node3D enemyContainer;
    [Export] private int desiredEnemies;
    private List<EnemyController> enemyList;

    [ExportCategory("Enemy Resources")] 
    [Export] private EnemyStats tankStats;
    [Export] private EnemyStats speedstrStats;
    [Export] private EnemyStats baseStats;
    
    private int currentListIter;
    private int maxListIter;

    public void Startup()
    {
        enemyList = new List<EnemyController>();
        currentListIter = 0;
        maxListIter = desiredEnemies;

        for (int i = 0; i < desiredEnemies; i++)
        {
            EnemyController enemy = enemyPrefab.Instantiate() as EnemyController;
            
            //Generate Class and stats
            enemy.Stats.Duplicate();

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
            
            enemy.DisableEnemy();
            enemyList.Add(enemy);
            AddChild(enemy);
        }
    }

    private void OnTimerTimeout()
    {
        if (!Global.gamePaused)
        {
            if (desiredEnemies > 0)
            {
                if (!enemyList[currentListIter].GetIsActive())
                {
                    enemyList[currentListIter].EnableEnemy();
                    enemyList[currentListIter].ResetHealth();
                    enemyList[currentListIter].Position = new Vector3((float)GD.RandRange(-6.0, 6.0), 0.0f, -20.0f);

                    currentListIter++;
                    if (currentListIter >= maxListIter)
                    {
                        currentListIter = 0;
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
}
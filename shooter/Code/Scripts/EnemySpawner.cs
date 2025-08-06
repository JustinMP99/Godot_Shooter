using Godot;
using System;
using System.Collections.Generic;

public partial class EnemySpawner : Node
{
    [Export] private Timer spawnTimer;
    [Export] private PackedScene enemyPrefab;
    [Export] private Node3D enemyContainer;
    [Export] private int desiredEnemies;
    private List<EnemyController> masterEnemyList;
    private List<EnemyController> activeEnemyList;
    
    [ExportCategory("Enemy Resources")] 
    [Export] private EnemyStats tankStats;
    [Export] private EnemyStats speedstrStats;
    [Export] private EnemyStats baseStats;
    
    private int currentListIter;
    private int maxListIter;

    public void Startup()
    {
        masterEnemyList = new List<EnemyController>();
        currentListIter = 0;
        maxListIter = desiredEnemies;

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
            masterEnemyList.Add(enemy);
            AddChild(enemy);
        }
    }

    public void DisableAllEnemies()
    {
        for (int i = 0; i < masterEnemyList.Count; i++)
        {
            masterEnemyList[i].Disable();
        }
    }
    
    private void OnTimerTimeout()
    {
        if (!Global.gamePaused)
        {
            if (desiredEnemies > 0)
            {
                if (!masterEnemyList[currentListIter].GetIsActive())
                {
                    masterEnemyList[currentListIter].Enable();
                    masterEnemyList[currentListIter].ResetHealth();
                    masterEnemyList[currentListIter].Position = new Vector3((float)GD.RandRange(-6.0, 6.0), 0.0f, -20.0f);

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
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

                    enemy.Stats.Class = EnemyClass.Base_Unit;
                    enemy.Stats.MaxHealth = 100;
                    enemy.Stats.CurrentHealth = 100;
                    enemy.Stats.Speed = 5.0f;

                    break;
                    
                case 2:

                    enemy.Stats.Class = EnemyClass.Speedstr_Unit;
                    enemy.Stats.MaxHealth = 50;
                    enemy.Stats.CurrentHealth = 50;
                    enemy.Stats.Speed = 8.0f;
                    
                    break;
                
                case 3:

                    enemy.Stats.Class = EnemyClass.Tank_Unit;
                    enemy.Stats.MaxHealth = 200;
                    enemy.Stats.CurrentHealth = 200;
                    enemy.Stats.Speed = 3.0f;
                    
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
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
            GD.Print("Instanced Enemy " + i);
            EnemyController enemy = enemyPrefab.Instantiate() as EnemyController;
            enemy.DisableEnemy();
            enemyList.Add(enemy);
            AddChild(enemy);
            //GetTree().Root.AddChild(enemy);
            //enemyContainer.AddChild(enemy);
        }
        
    }
    
    private void OnTimerTimeout()
    {
        
        if (desiredEnemies > 0)
        {
            
            if (!enemyList[currentListIter].GetIsActive())
            {
                enemyList[currentListIter].EnableEnemy();
                enemyList[currentListIter].Position = new Vector3((float)GD.RandRange(-6.0, 6.0), 0.0f, -20.0f);
                
                currentListIter++;
                if (currentListIter >= maxListIter)
                {
                    currentListIter = 0;
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

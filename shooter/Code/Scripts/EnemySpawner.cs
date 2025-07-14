using Godot;
using System;
using System.Collections.Generic;

public partial class EnemySpawner : Node
{

    [Export] private Timer spawnTimer;
    [Export] private PackedScene enemyPrefab;
    [Export] private Node3D enemyContainer;
    [Export] private int desiredEnemies;
    private List<RigidBody3D> enemyList;

    private int currentListIter;
    private int maxListIter;    

    public void Startup()
    {

        enemyList = new List<RigidBody3D>();
        currentListIter = 0;
        maxListIter = desiredEnemies;
        
        for (int i = 0; i < desiredEnemies; i++)
        {
            RigidBody3D enemy = enemyPrefab.Instantiate() as RigidBody3D;

            enemyList.Add(enemy);
            
            enemyContainer.AddChild(enemy);
        }
        
    }
    
    private void OnTimerTimeout()
    {

        GD.Print("Spawning Enemy!");
        
        //Setup the pooled enemy
        if (!enemyList[currentListIter].GetNode<EnemyController>("res://Code/Scripts/EnemyController.cs").GetIsActive())
        {
            GD.Print("Enemy Not Active!");
            //enemyList[currentListIter].SetProcess(true);
            //enemyList[currentListIter].Visible = true;
            //enemyList[currentListIter].Position = new Vector3((float)GD.RandRange(-6.0, 6.0), 0.0f, -20.0f);
        
        }
        
        currentListIter++;
        if (currentListIter <= maxListIter)
        {
            currentListIter = 0;
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

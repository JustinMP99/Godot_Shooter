using Godot;
using System;

public partial class EnemySpawner : Node
{

    [Export] private Timer spawnTimer;
    [Export] private PackedScene enemyPrefab;
    [Export] private Node3D enemyContainer;

    private void OnTimerTimeout()
    {

        GD.Print("Spawning Enemy!");
        RigidBody3D enemy = enemyPrefab.Instantiate() as RigidBody3D;

        enemyContainer.AddChild(enemy);
        enemy.Position = new Vector3((float)GD.RandRange(-6.0, 6.0), 0.0f, -20.0f);

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

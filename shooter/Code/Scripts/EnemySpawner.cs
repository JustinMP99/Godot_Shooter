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

	[ExportCategory("Enemy Materials")]
	[Export] private Material baseEnemyMaterial;

	[Export] private Material speedstrEnemyMaterial;
	[Export] private Material tankEnemyMaterial;

	//Pool Data
	private List<EnemyController> enemyPool;
	private List<EnemyController> activeEnemies;
	private int poolIter;
	private int poolIterMax;

	/// <summary>
	/// creates pool of enemies and generates enemy stats
	/// </summary>
	public void Startup()
	{
		enemyPool = new List<EnemyController>();
		activeEnemies = new List<EnemyController>();
		poolIter = 0;
		poolIterMax = desiredEnemies;

		for (int i = 0; i < desiredEnemies; i++)
		{
			EnemyController enemy = enemyPrefab.Instantiate() as EnemyController;
			enemy.Disable();
			enemyPool.Add(enemy);
			AddChild(enemy);
		}
	}

	public override void _Process(double delta)
	{
		if (!Global.GamePaused)
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
		EnemyController enemy = enemyPool[poolIter];

		if (!enemy.GetIsActive())
		{
			SpawnEnemy(enemy);
			poolIter++;
			if (poolIter >= poolIterMax)
			{
				poolIter = 0;
			}
		}
	}

	private void SpawnEnemy(EnemyController enemy)
	{
		//determine stats
		int value = 1;

		if (Global.Round > 5 && Global.Round <= 10)
		{
			value = (int)GD.RandRange(1.0, 2.0);
		}
		else if (Global.Round > 10 && Global.Round <= 20)
		{
			value = (int)GD.RandRange(1.0, 3.0);
		}

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

		enemy.Enable();
		enemy.Position = new Vector3((float)GD.RandRange(-6.0, 6.0), 0.0f, -20.0f);
		activeEnemies.Add(enemy);
	}

	public void SetTimerValue(double newWaitTime)
	{
		spawnTimer.WaitTime = newWaitTime;
	}

	public double GetTimerWaitTime()
	{
		return spawnTimer.WaitTime;
	}

	public void DecrementTimer()
	{
		if (spawnTimer.WaitTime > 0.25)
		{
			spawnTimer.WaitTime -= 0.25;
		}
	}

	public void ResetTimerValue()
	{
		spawnTimer.WaitTime = 2.0;
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

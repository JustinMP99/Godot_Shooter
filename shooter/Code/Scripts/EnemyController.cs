using Godot;
using System;


public partial class EnemyController : RigidBody3D
{
    private bool isActive;

    [ExportCategory("Enemy Stats")]
    [Export] public EnemyStats Stats;

    [ExportCategory("Enemy Components")]
    [Export] private Area3D area;

    [Export] private CollisionShape3D collider;

    #region Constructors

    #endregion

    public override void _Ready()
    {
    }

    public void _Process(double delta)
    {
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!Global.gamePaused)
        {
            MoveAndCollide(Transform.Basis.Z * (float)delta * Stats.Speed);
            if (Position.Z >= 5.0f)
            {
                Visible = false;
                isActive = false;
            }
        }
    }

    public void Release()
    {
    }

    public void EnableEnemy()
    {
        isActive = true;
        SetProcess(true);
        SetPhysicsProcess(true);
        Visible = true;
    }

    public void DisableEnemy()
    {
        isActive = false;
        SetProcess(false);
        SetPhysicsProcess(false);
        Visible = false;
        Position = new Vector3(10.0f, 10.0f, 10.0f);
    }

    
    /// <summary>
    /// Calculates the damage taken by the enemy when shot by a player bullet
    /// </summary>
    /// <param name="bulletDamage"></param>
    /// <returns>If the current health is less than or equal to zero, the function returns true</returns>
    public bool TakeDamage(int bulletDamage)
    {

        Stats.CurrentHealth -= bulletDamage;
        if (Stats.CurrentHealth <= 0)
        {
            return true;
        }
        
        return false;
    }

    public void ResetHealth()
    {
        Stats.CurrentHealth = Stats.MaxHealth;
    }
    
    
    #region Getter

    public bool GetIsActive()
    {
        return isActive;
    }

    // public int GetCurrentHealth()
    // {
    //     return currentHealth;
    // }
    //
    // public int GetMaxHealth()
    // {
    //     return maxHealth;
    // }

    #endregion

    #region Setter

    public void SetIsActive(bool state)
    {
        isActive = state;
    }

    // public void SetCurrentHealth(int newCurrent)
    // {
    //     currentHealth = newCurrent;
    // }
    //
    // public void SetMaxHealth(int newMax)
    // {
    //     maxHealth = newMax;
    // }

    #endregion
}
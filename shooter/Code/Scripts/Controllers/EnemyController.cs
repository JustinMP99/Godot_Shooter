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
    [Export] private MeshInstance3D meshRef;
    [Export] private Timer hitTimer;

    [ExportCategory("Materials")]
    [Export] private Material normalMaterial;
    [Export] private Material hitMaterial;
    
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

    public void Enable()
    {
        isActive = true;
        SetProcess(true);
        SetPhysicsProcess(true);
        Visible = true;
    }

    public void Disable()
    {
        isActive = false;
        SetProcess(false);
        SetPhysicsProcess(false);
        Visible = false;
        Position = new Vector3(10.0f, 10.0f, 10.0f);
    }
    
    public bool TakeDamage(int bulletDamage)
    {

        Stats.CurrentHealth -= bulletDamage;
        if (Stats.CurrentHealth <= 0)
        {
            return true;
        }

        meshRef.MaterialOverride = hitMaterial;
        hitTimer.Start();
        
        return false;
    }

    private void HitTimerEnd()
    {
        meshRef.MaterialOverride = normalMaterial;
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
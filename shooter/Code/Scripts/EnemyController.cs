using Godot;
using System;

public enum EnemyClass
{
    Base_Unit,
    Tank_Unit,
    Speedstr_Unit
}

public partial class EnemyController : RigidBody3D
{

    private bool isActive;
    
    [ExportCategory("Enemy Stats")]
    [Export] private EnemyClass enemyClass;
    [Export] private int currentHealth;
    [Export] private int maxHealth;
    [Export] private float speed;
    
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
            MoveAndCollide(Transform.Basis.Z * (float)delta * speed);
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

    public bool Damage()
    {


        return false;
    }
    
    #region Getter

    public bool GetIsActive()
    {
        return isActive;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    #endregion

    #region Setter

    public void SetIsActive(bool state)
    {
        isActive = state;
    }

    public void SetCurrentHealth(int newCurrent)
    {
        currentHealth = newCurrent;
    }

    public void SetMaxHealth(int newMax)
    {
        maxHealth = newMax;
    }

    #endregion
    
}

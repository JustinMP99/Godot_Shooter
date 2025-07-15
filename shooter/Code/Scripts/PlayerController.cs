using Godot;
using System;

public partial class PlayerController : CharacterBody3D
{
    [Signal] 
    public delegate void PauseSignalEventHandler();

    [Signal]
    public delegate void PlayerHitEventHandler();

    [Signal]
    public delegate void PlayerDiedEventHandler();
    
    [Signal]
    public delegate void EnemyDefeatedEventHandler();
    
    
    [Export] private float speed;
    [Export] private bool takingInput;
    [Export] private Node3D bulletPosition;
    [Export] private PackedScene bulletPrefab;

    [ExportCategory("Player Stats")] 
    [Export] private int currentHealth;
    [Export] private int maxHealth;
    
    private Vector3 targetVelocity = Vector3.Zero;
    private Vector3 direction;
    
    public override void _Ready()
    {
        
    }

    public override void _Process(double delta)
    {
        
    }

    public override void _PhysicsProcess(double delta)
    {

        if (takingInput)
        {
            
            CollectInput();
            
            targetVelocity.X = direction.X * speed;
            targetVelocity.Z = direction.Z * speed;

            Velocity = targetVelocity;
            
            MoveAndSlide();
            
        }
        
    }


    private void CollectInput()
    {
        direction = Vector3.Zero;
        
        if (Input.IsActionPressed("Move_Left"))
        {
            direction.X -= 1.0f;
        }
        if (Input.IsActionPressed("Move_Right"))
        {
            direction.X += 1.0f;
        }
        if (Input.IsActionJustPressed("Shoot"))
        {
            ShootFunction();
        }
        if (Input.IsActionJustPressed("Pause"))
        {
            PauseFunction();
        }
        if (direction !=  Vector3.Zero)
        {
            direction = direction.Normalized();
        }
    }
    
    private void PauseFunction()
    {
        
        GD.Print("Emitting Pause Signal");
        //Emit Signal
        EmitSignal(SignalName.PauseSignal);

    }

    private void ShootFunction()
    {
        
        GD.Print("Shooting");
        //Instantiate Bullet
        //RigidBody3D bullet = bulletPrefab.Instantiate() as RigidBody3D;
        Bullet bullet = bulletPrefab.Instantiate() as Bullet;
        bullet.FinalShot += EnemyDefeat;
        
        //Set Child
        
        //Set Position
        bullet.Position = bulletPosition.GlobalPosition;
        
        GetTree().Root.AddChild(bullet);

    }

    private void EnemyDefeat()
    {
        EmitSignal(SignalName.EnemyDefeated);
    }

    private void OnBodyEntered(Node3D body)
    {

        if (body is EnemyController enemy)
        {
            GD.Print("Collided with Enemy!");
            enemy.DisableEnemy();
            //enemy.Position = new Vector3(10.0f, 10.0f, 10.0f);
            
            //take damage
            currentHealth -= 10;
            
            //Check currentHealth
            if (currentHealth <= 0)
            {
                GD.Print("Player has died");
                takingInput = false;
            
                this.QueueFree();
                //Game Over
                EmitSignal(SignalName.PlayerDied);
            }
            else
            {
                GD.Print("Player has taken damage");
                //Update UI
                EmitSignal(SignalName.PlayerHit);

            }
        }
        
    }

    #region Getter
    
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

    public void SetCurrentHealth(int newCurrent)
    {
        currentHealth = newCurrent;
    }

    public void SetMaxHealth(int newMax)
    {
        maxHealth = newMax;
    }
    
    public void SetTakingInput(bool state)
    {
        takingInput = state;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    #endregion
    
}

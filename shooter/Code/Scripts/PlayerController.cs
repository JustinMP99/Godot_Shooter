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
    
    
    [Export] private float speed;
    [Export] private bool takingInput;
    [Export] private Node3D bulletPosition;
    [Export] private PackedScene bulletPrefab;

    [ExportCategory("Player Stats")] 
    [Export] private int currentHealth;
    [Export] private int maxHealth;
    
    
    private Vector3 targetVelocity = Vector3.Zero;
    
    
    public override void _Ready()
    {
        
    }

    public override void _Process(double delta)
    {
        if (takingInput)
        {
            if (Input.IsActionJustPressed("Pause"))
            {
            
                PauseFunction();
            
            }
        }
        
    }

    public override void _PhysicsProcess(double delta)
    {

        if (takingInput)
        {
            
            Vector3 direction = Vector3.Zero;

            // if (Input.IsActionPressed("Move_Forward"))
            // {
            //     GD.Print("Moving Forward");
            //     
            //     direction.Z -= 1.0f;
            // }
            // if (Input.IsActionPressed("Move_Down"))
            // {
            //     direction.Z += 1.0f;
            // }

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
            

            if (direction !=  Vector3.Zero)
            {
                
                direction = direction.Normalized();
                
            }
            
            targetVelocity.X = direction.X * speed;
            targetVelocity.Z = direction.Z * speed;

            Velocity = targetVelocity;
            
            MoveAndSlide();
            
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
        RigidBody3D bullet = bulletPrefab.Instantiate() as RigidBody3D;
        
        //Set Child
        
        //Set Position
        bullet.Position = bulletPosition.GlobalPosition;
        
        GetTree().Root.AddChild(bullet);

    }

    private void OnBodyEntered(Node3D body)
    {

        if (body.Name == "Back Wall")
        {
            GD.Print("Hit Back Wall");
        }
        
        //Destroy Enemy
        body.QueueFree();
        
        //take damage
        currentHealth -= 10;
        GD.Print("Current Health: " + currentHealth);
        
        //Check currentHealth
        if (currentHealth <= 0)
        {
            
            takingInput = false;
            
            this.QueueFree();
            //Game Over
            EmitSignal(SignalName.PlayerDied);
        }
        else
        {
            
            //Update UI
            EmitSignal(SignalName.PlayerHit);

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

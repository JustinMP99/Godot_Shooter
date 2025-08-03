using Godot;
using System;

public partial class PlayerController : CharacterBody3D
{
    public static PlayerController Instance { get; private set; }

    #region Signals

    [Signal]
    public delegate void PauseSignalEventHandler();

    [Signal]
    public delegate void PlayerHitEventHandler();

    [Signal]
    public delegate void PlayerDiedEventHandler();

    [Signal]
    public delegate void EnemyDefeatedEventHandler();

    #endregion

    [ExportCategory("Cheat Settings")]
    private bool Invincible { get; set; }

    [Export] private bool takingInput;
    public bool simpleShoot { get; set; }
    [Export] private Node3D bulletPosition;
    [Export] private PackedScene bulletPrefab;

    [ExportCategory("Shooting Variables")]
    [Export] private bool canShoot;
    [Export] private Timer shootTimer;
    [Export] public BulletManager bulletManager { get; set; }
    [Export] private Node3D reticle;
    
    
    [ExportCategory("Player Stats")] 
    [Export] public int Credits { get; set; }
    [Export] public Player_Stats Stats;
    [Export] private Gun playerGun;
    private Vector3 targetVelocity = Vector3.Zero;
    private Vector3 targetRotation = Vector3.Zero;
    private Vector3 direction;
    private Vector3 rotation;
    private float rotationSpeed = 1.5f;

    public override void _Ready()
    {
        Instance = this;
        canShoot = true;
    }

    public override void _Process(double delta)
    {
        
    }

    public override void _PhysicsProcess(double delta)
    {

        if (takingInput)
        {
            
            ReticleRaycast();
            
            CollectInput();
            //velocity calculation
            targetVelocity.X = direction.X * Stats.Speed;
            Velocity = Velocity.Lerp(targetVelocity, 1.0f - float.Exp(-20.0f * (float)GetProcessDeltaTime()));
            //rotation calculation
            targetRotation.Z = rotation.Z * rotationSpeed;
            Rotation = Rotation.Lerp(targetRotation, 1.0f - float.Exp(-20.0f * (float)GetProcessDeltaTime()));
            
            MoveAndSlide();
        }
    }

    private void ReticleRaycast()
    {
        
        var spaceState = GetWorld3D().DirectSpaceState;
        
        var origin = bulletPosition.GlobalPosition;
        var end = origin + new Vector3(0.0f, 0.0f, -100.0f);
        var query = PhysicsRayQueryParameters3D.Create(origin, end, collisionMask:2);
        
        var result = spaceState.IntersectRay(query);
        
        if (result.ContainsKey("position"))
        {
            
            GD.Print("Region Positon:" + result["position"].AsVector3());
            Vector3 newReticlePosition =  result["position"].AsVector3();
            newReticlePosition.Z += 0.1f;

            reticle.GlobalPosition = newReticlePosition;
        }
        else
        {
            reticle.Position = new Vector3(0.0f, 0.0f, -10.0f);
        }
    }
    
    private void CollectInput()
    {
        direction = Vector3.Zero;
        rotation = Vector3.Zero;
        
        if (Input.IsActionPressed("Move_Left"))
        {
            direction.X -= 1.0f;
            rotation.Z += 0.25f;
            
        }
        if (Input.IsActionPressed("Move_Right"))
        {
            direction.X += 1.0f;
            rotation.Z -= 0.25f;
        }

        if (Input.IsActionPressed("Shoot"))
        {
            ShootFunction();
        }
    
        if (Input.IsActionJustPressed("Pause"))
        {
            PauseFunction();
        }

        //
        
        if (direction != Vector3.Zero)
        {
            direction = direction.Normalized();
        }
    }

    private void PauseFunction()
    {
        //Emit Signal
        EmitSignal(SignalName.PauseSignal);
    }

    private void ShootFunction()
    {
        if (canShoot)
        {

            Bullet temp = bulletManager.RequestBullet();

            temp.Position = bulletPosition.GlobalPosition;
            temp.FinalShot += EnemyDefeat;
            
            temp.Enable();
            
            //Instantiate Bullet
            // Bullet bullet = bulletPrefab.Instantiate() as Bullet;
            // bullet.FinalShot += EnemyDefeat;
        
            AudioManager.Instance.PlayShootSound();
            //Set Child

            //Set Position
           // bullet.Position = bulletPosition.GlobalPosition;

            //Start Timer
            canShoot = false;
            shootTimer.Start();
            
            //GetTree().Root.AddChild(bullet);
        }
    }

    private void EnemyDefeat()
    {
        EmitSignal(SignalName.EnemyDefeated);
    }

    private void OnBodyEntered(Node3D body)
    {
        if (body is EnemyController enemy)
        {
            enemy.Disable();
            //enemy.Position = new Vector3(10.0f, 10.0f, 10.0f);

            //take damage
            Stats.CurrentHealth -= 10;

            //Check currentHealth
            if (Stats.CurrentHealth <= 0)
            {
                takingInput = false;
                this.Position = new Vector3(0.0f, 0.0f, 10.0f);
                //Game Over
                EmitSignal(SignalName.PlayerDied);
            }
            else
            {
                //Update UI
                EmitSignal(SignalName.PlayerHit);
            }
        }
    }

    private void ReadyToShoot()
    {
        canShoot = true;
        //GD.Print("shooting is available");
    }
    
    #region Getter

    public int GetCurrentHealth()
    {
        return Stats.CurrentHealth;
    }

    public int GetMaxHealth()
    {
        return Stats.MaxHealth;
    }

    #endregion

    #region Setter

    public void SetCurrentHealth(int newCurrent)
    {
        Stats.CurrentHealth = newCurrent;
    }

    public void SetMaxHealth(int newMax)
    {
        Stats.MaxHealth = newMax;
    }

    public void SetTakingInput(bool state)
    {
        takingInput = state;
    }

    public void SetSpeed(float newSpeed)
    {
        Stats.Speed = newSpeed;
    }

    #endregion
}
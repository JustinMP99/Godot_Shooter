using Godot;
using System;

public enum ShootType
{
    Single,
    Shotgun,
    Spread_Random
}

public enum InputState
{
    Game,
    Menu,
}

public partial class PlayerController : CharacterBody3D
{
    [Export] private bool debug = false;

    public static PlayerController Instance { get; private set; }

    #region Signals

    [Signal]
    public delegate void PauseSignalEventHandler();

    [Signal]
    public delegate void PlayerHitEventHandler();

    [Signal]
    public delegate void PlayerDiedEventHandler();

    [Signal]
    public delegate void PlayerHealedEventHandler();

    [Signal]
    public delegate void EnemyDefeatedEventHandler();

    [Signal]
    public delegate void ShootTypePowerUpEventHandler(PowerUpStats_ShootType shootStats);

    #endregion

    public Node3D startPosition;

    
    // TODO: REMOVE THIS SECTION
    [ExportCategory("Input Variables")]
    [Export] private InputState inputState;

    [Export] private int currentMenuIter = 0;
    [Export] private int maxMenuIter = 0;

    [ExportCategory("Player Components")]
    [Export] public InputComponent Input;

    [Export] public StatComponent Stat;

    //TODO: MOVE TO GUN COMPONENT
    private Node3D bulletLeftPosition;
    private Node3D bulletCenterPosition;
    private Node3D bulletRightPosition;
    private MeshInstance3D reticle;
    private MeshInstance3D playerMesh;
    public BulletManager bulletManager;

    [ExportCategory("Animators")]
    private AnimationTree shipAnimationTree;
    private AnimationPlayer shipAnimationPlayer;
    private AnimationTree reticleAnimationTree;
    private AnimationPlayer reticleAnimationPlayer;

    
    //TODO: MOVE MOST DATA HERE TO GUN COMPONENT
    [ExportCategory("Shooting Variables")]
    [Export] private ShootType shootType;

    [Export] private Timer shootTimer;
    [Export] private bool canShoot;
    [Export] private Texture reticleNormal;
    [Export] private Texture reticleLocked;
    private Bullet cachedBulletOne;
    private Bullet cachedBulletTwo;
    private Bullet cachedBulletThree;

    private EnemyController prevEnemy;

    //TODO: REMOVE VARIABLES THAT ARE PRESENT IN INPUT COMPONENT
    [ExportCategory("Player Stats")]
    [Export] private bool takingInput;

    //[Export] public int Credits;
    //[Export] public PlayerStats Stats;
    [Export] private Gun playerGun;
    private Vector3 targetVelocity = Vector3.Zero;

    private Vector3 targetRotation = Vector3.Zero;

    // private Vector3 direction;
    // private Vector3 rotation;
    private float rotationSpeed = 1.25f;

    [ExportCategory("Cheat Settings")]
    public bool Invincible = false;

    public bool simpleShoot;

    public override void _Ready()
    {
        Instance = this;
        canShoot = true;
        inputState = InputState.Game;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (takingInput)
        {
            ReticleRaycast();
            //velocity calculation
            targetVelocity.X = Input.direction.X * Stat.GetSpeed();
            Velocity = Velocity.Lerp(targetVelocity, 1.0f - float.Exp(-20.0f * (float)GetProcessDeltaTime()));
            //rotation calculation
            targetRotation.Z = Input.rotation.Z * rotationSpeed;
            playerMesh.Rotation =
                playerMesh.Rotation.Lerp(targetRotation, 1.0f - float.Exp(-20.0f * (float)GetProcessDeltaTime()));
            MoveAndSlide();
        }
    }

    /// <summary>
    /// Finds children nodes and assigns them to PlayerController variables
    /// </summary>
    public void FindNodes()
    {
        Input = GetNode<InputComponent>("Components/Input");
        Stat = GetNode<StatComponent>("Components/Stats");

        bulletLeftPosition = GetNode<Node3D>("BulletPositions/Bullet Left");
        bulletCenterPosition = GetNode<Node3D>("BulletPositions/Bullet Center");
        bulletRightPosition = GetNode<Node3D>("BulletPositions/Bullet Right");

        playerMesh = GetNode<MeshInstance3D>("Player Ship Mesh3D");
        reticle = GetNode<MeshInstance3D>("Reticle");

        shipAnimationTree = GetNode<AnimationTree>("Player Ship Mesh3D/AnimationTree");
        shipAnimationPlayer = GetNode<AnimationPlayer>("Player Ship Mesh3D/AnimationPlayer");

        reticleAnimationTree = GetNode<AnimationTree>("Reticle/AnimationTree");
        reticleAnimationPlayer = GetNode<AnimationPlayer>("Reticle/AnimationPlayer");
    }

    #region Input Functions

    private void CollectInput()
    {
        switch (inputState)
        {
            case InputState.Menu:

                break;
            case InputState.Game:
                GameInput();
                break;
        }
    }

    private void GameInput()
    {
    }

    private void MenuInput()
    {
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
            switch (shootType)
            {
                case ShootType.Single:
                    cachedBulletOne = bulletManager.RequestBullet();
                    cachedBulletOne.Position = bulletCenterPosition.GlobalPosition;
                    cachedBulletOne.Rotation = bulletCenterPosition.GlobalRotation;
                    cachedBulletOne.FinalShot += EnemyDefeat;
                    cachedBulletOne.Enable();
                    AudioManager.Instance.PlayShootSound();
                    //Start Timer
                    canShoot = false;
                    shootTimer.Start();
                    break;
                case ShootType.Shotgun:

                    cachedBulletOne = bulletManager.RequestBullet();
                    cachedBulletTwo = bulletManager.RequestBullet();
                    cachedBulletThree = bulletManager.RequestBullet();

                    cachedBulletOne.Position = bulletCenterPosition.GlobalPosition;
                    cachedBulletOne.Rotation = bulletLeftPosition.GlobalRotation;
                    cachedBulletOne.FinalShot += EnemyDefeat;

                    cachedBulletTwo.Position = bulletCenterPosition.GlobalPosition;
                    cachedBulletTwo.Rotation = bulletCenterPosition.GlobalRotation;
                    cachedBulletTwo.FinalShot += EnemyDefeat;

                    cachedBulletThree.Position = bulletCenterPosition.GlobalPosition;
                    cachedBulletThree.Rotation = bulletRightPosition.GlobalRotation;
                    cachedBulletThree.FinalShot += EnemyDefeat;

                    cachedBulletOne.Enable();
                    cachedBulletTwo.Enable();
                    cachedBulletThree.Enable();

                    AudioManager.Instance.PlayShootSound();
                    //Start Timer
                    canShoot = false;
                    shootTimer.Start();

                    break;
                case ShootType.Spread_Random:

                    cachedBulletOne = bulletManager.RequestBullet();

                    cachedBulletOne.Position = bulletCenterPosition.GlobalPosition;

                    cachedBulletOne.GlobalRotation = new Vector3(0.0f, (float)GD.RandRange(-1.0f, 1.0f), 0.0f);

                    cachedBulletOne.FinalShot += EnemyDefeat;

                    cachedBulletOne.Enable();

                    AudioManager.Instance.PlayShootSound();

                    break;
            }
        }
    }

    #endregion

    #region Additional Functions

    private void ReticleRaycast()
    {
        var spaceState = GetWorld3D().DirectSpaceState;

        var origin = bulletCenterPosition.GlobalPosition;
        var end = origin + new Vector3(0.0f, 0.0f, -100.0f);
        var query = PhysicsRayQueryParameters3D.Create(origin, end, collisionMask: 2);

        var result = spaceState.IntersectRay(query);
        if (result.ContainsKey("collider"))
        {
            EnemyController enemy = result["collider"].As<EnemyController>();
            if (prevEnemy != enemy || prevEnemy == null)
            {
                //reticle.GetSurfaceOverrideMaterial(0).Set("albedo_texture", reticleLocked);
                SetReticleReturnToIdleFalse();
                SetReticleLockOnTrue();
            }
        }
        else
        {
            //reticle.GetSurfaceOverrideMaterial(0).Set("albedo_texture", reticleNormal);
            prevEnemy = null;
            SetReticleLockOnFalse();
            SetReticleReturnToIdleTrue();
        }

        if (result.ContainsKey("position"))
        {
            Vector3 newReticlePosition = result["position"].AsVector3();
            newReticlePosition.Z += 0.1f;
            reticle.GlobalPosition = newReticlePosition;
        }
        else
        {
            reticle.Position = new Vector3(0.0f, 0.0f, -10.0f);
        }
    }

    private void OnBodyEntered(Node3D body)
    {
        if (body is EnemyController enemy)
        {
            enemy.Disable();

            //take damage
            if (!Invincible)
            {
                Stat.DecreaseHealth(10);
            }

            //Check currentHealth
            if (Stat.GetCurrentHealth() <= 0)
            {
                takingInput = false;
                this.Position = new Vector3(0.0f, 0.0f, 100.0f);
                //Game Over
                EmitSignal(SignalName.PlayerDied);
            }
            else
            {
                //Update UI
                EmitSignal(SignalName.PlayerHit);
            }
        }
        else if (body is PowerUp powerUp)
        {
            switch (powerUp.Stats.Type)
            {
                case PowerUpType.Health:
                    GD.Print("Restored Player Health!");
                    PowerUpStats_Health healthStats = powerUp.Stats as PowerUpStats_Health;
                    powerUp.Disable();
                    Heal(healthStats.healthRestoreAmount);
                    break;
                case PowerUpType.Shoot_Type:
                    GD.Print("Changing Shoot Type!");
                    powerUp.Disable();
                    PowerUpStats_ShootType shootStats = powerUp.Stats as PowerUpStats_ShootType;
                    //activate shoot timer
                    EmitSignal(SignalName.ShootTypePowerUp, shootStats);
                    break;
            }
        }
    }

    private void ReadyToShoot()
    {
        canShoot = true;
    }

    /// <summary>
    /// Resets all Player data (animations, shoot type, taking input state)
    /// </summary>
    public void Reset()
    {
        takingInput = false;
        shootType = ShootType.Single;
        Stat.SetCurrentHealth(Stat.GetMaxHealth());
        ResetShootTimer();
        SetReticleFlyInFalse();
        SetShipFlyInFalse();
        shipAnimationTree.Active = false;
        shipAnimationTree.Active = true;
    }

    /// <summary>
    /// Resets shoot timers Wait Time to the value stored in Stats.FireRate
    /// </summary>
    public void ResetShootTimer()
    {
        shootTimer.WaitTime = Stat.GetFireRate();
    }

    #endregion

    #region Signal Functions

    public void Heal(int healAmount)
    {
        GD.Print("player has been healed for " + healAmount + " points");
        Stat.SetCurrentHealth(Stat.GetCurrentHealth() + healAmount);
        if (Stat.GetCurrentHealth() > Stat.GetMaxHealth())
        {
            Stat.SetCurrentHealth(Stat.GetMaxHealth());
        }

        EmitSignal(SignalName.PlayerHealed);
    }

    public void SwitchShootType(ShootType newType)
    {
        shootType = newType;

        switch (shootType)
        {
            case ShootType.Single:
                GD.Print("Fire Rate: " + Stat.GetFireRate());
                shootTimer.WaitTime = Stat.GetFireRate();
                break;
            case ShootType.Shotgun:
                shootTimer.WaitTime = 0.75;
                break;
            case ShootType.Spread_Random:
                shootTimer.WaitTime = 0.1;
                break;
        }
    }

    private void EnemyDefeat()
    {
        EmitSignal(SignalName.EnemyDefeated);
    }

    #endregion

    #region Animation Functions

    public void SetShipFlyInTrue()
    {
        shipAnimationTree.Set("parameters/conditions/Fly In", true);
    }

    public void SetShipFlyInFalse()
    {
        shipAnimationTree.Set("parameters/conditions/Fly In", false);
    }

    public void SetReticleFlyInTrue()
    {
        reticleAnimationTree.Set("parameters/conditions/Fly In", true);
    }

    public void SetReticleFlyInFalse()
    {
        reticleAnimationTree.Set("parameters/conditions/Fly In", false);
    }

    public void SetReticleLockOnTrue()
    {
        reticleAnimationTree.Set("parameters/conditions/Lock On", true);
    }

    public void SetReticleLockOnFalse()
    {
        reticleAnimationTree.Set("parameters/conditions/Lock On", false);
    }

    public void SetReticleReturnToIdleTrue()
    {
        reticleAnimationTree.Set("parameters/conditions/Return To Idle", true);
    }

    public void SetReticleReturnToIdleFalse()
    {
        reticleAnimationTree.Set("parameters/conditions/Return To Idle", false);
    }

    #endregion

    #region Setter

    public bool SetInvincibleState()
    {
        Invincible = !Invincible;
        return Invincible;
    }

    #endregion
}
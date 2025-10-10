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
    public delegate void PlayerHitEventHandler();

    [Signal]
    public delegate void PlayerDiedEventHandler();

    [Signal]
    public delegate void PlayerHealedEventHandler();
    
    [Signal]
    public delegate void ShootTypePowerUpEventHandler(PowerUpStats_ShootType shootStats);

    #endregion

    public Node3D startPosition;
    public InputComponent Input;
    public StatComponent Stats;
    public GunComponent Gun;
    public AnimationControllerComponent animController;

    //TODO: MOVE TO GUN COMPONENT
    private MeshInstance3D reticle;
    private MeshInstance3D playerMesh;

    [ExportCategory("Animators")]
    private AnimationTree shipAnimationTree;
    private AnimationPlayer shipAnimationPlayer;
    private AnimationTree reticleAnimationTree;
    private AnimationPlayer reticleAnimationPlayer;
    
    //TODO: MOVE MOST DATA HERE TO GUN COMPONENT
    private EnemyController prevEnemy;

    //TODO: REMOVE VARIABLES THAT ARE PRESENT IN INPUT COMPONENT
    [ExportCategory("Player Stats")]
    private Vector3 targetVelocity = Vector3.Zero;
    private Vector3 targetRotation = Vector3.Zero;
    private float rotationSpeed = 1.25f;

    [ExportCategory("Cheat Settings")]
    public bool Invincible = false;
    public bool simpleShoot;

    public override void _Ready()
    {
        Instance = this;
        FindNodes();
    }

    public override void _PhysicsProcess(double delta)
    {
        ReticleRaycast();
        //velocity calculation
        targetVelocity.X = Input.direction.X * Stats.GetSpeed();
        Velocity = Velocity.Lerp(targetVelocity, 1.0f - float.Exp(-20.0f * (float)GetProcessDeltaTime()));
        //rotation calculation
        targetRotation.Z = Input.rotation.Z * rotationSpeed;
        playerMesh.Rotation = playerMesh.Rotation.Lerp(targetRotation, 1.0f - float.Exp(-20.0f * (float)GetProcessDeltaTime()));
        MoveAndSlide();
    }

    /// <summary>
    /// Finds children nodes and assigns them to PlayerController variables
    /// </summary>
    public void FindNodes()
    {
        Input = GetNode<InputComponent>("Components/Input");
        Stats = GetNode<StatComponent>("Components/Stats");
        Gun = GetNode<GunComponent>("Components/Gun");
        animController = GetNode<AnimationControllerComponent>("Components/Animation Controller");

        Gun.bulletCenterPosition = GetNode<Node3D>("Components/Gun/Bullet Center");
        Gun.bulletLeftPosition = GetNode<Node3D>("Components/Gun/Bullet Left");
        Gun.bulletRightPosition = GetNode<Node3D>("Components/Gun/Bullet Right");
        

        playerMesh = GetNode<MeshInstance3D>("Mesh/Player Ship Mesh3D");
        reticle = GetNode<MeshInstance3D>("Mesh/Reticle");

        shipAnimationTree = GetNode<AnimationTree>("Mesh/Player Ship Mesh3D/AnimationTree");
        shipAnimationPlayer = GetNode<AnimationPlayer>("Mesh/Player Ship Mesh3D/AnimationPlayer");

        reticleAnimationTree = GetNode<AnimationTree>("Mesh/Reticle/AnimationTree");
        reticleAnimationPlayer = GetNode<AnimationPlayer>("Mesh/Reticle/AnimationPlayer");
    }
    
    #region Additional Functions

    private void ReticleRaycast()
    {
        var spaceState = GetWorld3D().DirectSpaceState;

        var origin = Gun.GlobalPosition;
        var end = origin + new Vector3(0.0f, 0.0f, -100.0f);
        var query = PhysicsRayQueryParameters3D.Create(origin, end, collisionMask: 2);

        var result = spaceState.IntersectRay(query);
        if (result.ContainsKey("collider"))
        {
            EnemyController enemy = result["collider"].As<EnemyController>();
            if (prevEnemy != enemy || prevEnemy == null)
            {
                animController.SetReticleReturnToIdleFalse();
                animController.SetReticleLockOnTrue();
            }
        }
        else
        {
            prevEnemy = null;
            animController.SetReticleLockOnFalse();
            animController.SetReticleReturnToIdleTrue();
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
                Stats.DecreaseHealth(10);
            }

            //Check currentHealth
            if (Stats.GetCurrentHealth() <= 0)
            {
                //Input.SetTakingInput(false);
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
    
    /// <summary>
    /// Resets all Player data (animations, shoot type, taking input state)
    /// </summary>
    public void Reset()
    {
        Input.SetTakingInput(false);
        Gun.SetShootType(ShootType.Single);
        Stats.SetCurrentHealth(Stats.GetMaxHealth());
        Gun.UpdateShootTimer(Stats.GetFireRate());
        animController.SetReticleFlyInFalse();
        animController.SetShipFlyInFalse();
        shipAnimationTree.Active = false;
        shipAnimationTree.Active = true;
    }

    #endregion

    #region Signal Functions

    public void Heal(int healAmount)
    {
        GD.Print("player has been healed for " + healAmount + " points");
        Stats.SetCurrentHealth(Stats.GetCurrentHealth() + healAmount);
        if (Stats.GetCurrentHealth() > Stats.GetMaxHealth())
        {
            Stats.SetCurrentHealth(Stats.GetMaxHealth());
        }

        EmitSignal(SignalName.PlayerHealed);
    }

    public void SwitchShootType(ShootType newType)
    {
        Gun.SetShootType(newType);
        switch (Gun.GetShootType())
        {
            case ShootType.Single:
                Gun.UpdateShootTimer(Stats.GetFireRate());
                break;
            case ShootType.Shotgun:
                Gun.UpdateShootTimer(0.5);
                break;
            case ShootType.Spread_Random:
                Gun.UpdateShootTimer(0.1);
                break;
        }
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
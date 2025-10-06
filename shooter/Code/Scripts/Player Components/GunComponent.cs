using Godot;
using System;

public partial class GunComponent : Node3D
{

    [Signal]
    public delegate void EnemyDefeatedEventHandler();
    [Signal] public delegate Bullet RequestBulletEventHandler();
    
    [ExportCategory("Debug")]
    [Export] private bool debug = false;

    [ExportCategory("Shooting Variables")]
    [Export] private Timer shootTimer;
    [Export] private ShootType shootType;
    [Export] private bool canShoot = false;
    public Node3D bulletLeftPosition;
    public Node3D bulletCenterPosition;
    public Node3D bulletRightPosition;
    
    private Bullet cachedBulletOne;
    private Bullet cachedBulletTwo;
    private Bullet cachedBulletThree;
    public BulletManager bulletManager;

    public void Shoot()
    {
        if (canShoot)
        {
            switch (shootType)
            {
                case ShootType.Single:
                    FireSingle();
                    break;
                case ShootType.Shotgun:
                    FireShotgun();
                    break;
                case ShootType.Spread_Random:
                    FireRandom();
                    break;
            }
        }
    }

    private void ShootTimerTimeout()
    {
        canShoot = true;
    }

    public void UpdateShootTimer(double newWaitTime)
    {
        shootTimer.WaitTime = newWaitTime;
    }

    private void FireSingle()
    {

        //EmitSignal(SignalName.RequestBullet, cachedBulletOne);
        cachedBulletOne = bulletManager.RequestBullet();
        cachedBulletOne.Position = bulletCenterPosition.GlobalPosition;
        cachedBulletOne.Rotation = bulletCenterPosition.GlobalRotation;
        cachedBulletOne.FinalShot += EnemyDefeat;
        cachedBulletOne.Enable();
        AudioManager.Instance.PlayShootSound();
        //Start Timer
        canShoot = false;
        shootTimer.Start();
    }

    private void FireShotgun()
    {
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
    }

    private void FireRandom()
    {
        cachedBulletOne = bulletManager.RequestBullet();
        cachedBulletOne.Position = bulletCenterPosition.GlobalPosition;
        cachedBulletOne.GlobalRotation = new Vector3(0.0f, (float)GD.RandRange(-1.0f, 1.0f), 0.0f);
        cachedBulletOne.FinalShot += EnemyDefeat;
        cachedBulletOne.Enable();
        AudioManager.Instance.PlayShootSound();
    }

    private void EnemyDefeat()
    {
        EmitSignal(SignalName.EnemyDefeated);
    }
    
    #region Getter

    public bool GetCanShoot()
    {
        return canShoot;
    }

    public ShootType GetShootType()
    {
        return shootType;
    }

    #endregion

    #region Setter

    public void SetCanShoot(bool state)
    {
        canShoot = state;
    }

    public void SetShootType(ShootType newType)
    {
        shootType = newType;
    }

    #endregion

}

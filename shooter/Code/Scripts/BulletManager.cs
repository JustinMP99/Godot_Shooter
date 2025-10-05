using Godot;
using System;
using System.Collections.Generic;

public partial class BulletManager : Node
{
    [ExportCategory("Bullet Data")]
    [Export] private int desiredBullets;

    [Export] private PackedScene bulletPrefab;

    //Pool Data
    private List<Bullet> bulletPool;
    private List<Bullet> activeBullets;
    private int poolIter;
    private int poolIterMax;

    public void Startup()
    {
        bulletPool = new List<Bullet>();
        activeBullets = new List<Bullet>();
        poolIter = 0;
        poolIterMax = desiredBullets;

        for (int i = 0; i < desiredBullets; i++)
        {
            Bullet newBullet = bulletPrefab.Instantiate() as Bullet;
            bulletPool.Add(newBullet);
            newBullet.Disable();
            this.AddChild(newBullet);
        }
    }

    public override void _Process(double delta)
    {
        if (!Global.GamePaused)
        {
            //move all active bullets
            MoveActiveBullets(delta);
        }
    }

    private void MoveActiveBullets(double delta)
    {
        for (int i = 0; i < activeBullets.Count; i++)
        {
            activeBullets[i].MoveBullet(delta);
        }

        activeBullets.RemoveAll(e => !e.isActive);
    }

    public Bullet RequestBullet()
    {
        poolIter++;
        if (poolIter == poolIterMax)
        {
            poolIter = 0;
        }

        activeBullets.Add(bulletPool[poolIter]);

        return bulletPool[poolIter];
    }

    public bool SetBulletsInstaKillState()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            bulletPool[i].InstaKill = !bulletPool[i].InstaKill;
        }

        return bulletPool[0].InstaKill;
    }
}
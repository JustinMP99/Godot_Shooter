using Godot;
using System;
using System.Collections.Generic;

public partial class BulletManager : Node
{

    [Export] private int desiredBullets;
    [Export] private PackedScene bulletPrefab;
    private List<Bullet> bulletPool;
    private List<Bullet> activeBullets;
    private int bulletIter;
    private int bulletIterMax;
    
    public void Startup()
    {
        
        bulletPool = new List<Bullet>();
        activeBullets = new List<Bullet>();
        bulletIter = 0;
        bulletIterMax = desiredBullets;
        
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
        if (!Global.gamePaused)
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
        bulletIter++;
        if (bulletIter == bulletIterMax)
        {
            bulletIter = 0;
        }
        GD.Print("Bullet Iter: " + bulletIter);

        activeBullets.Add(bulletPool[bulletIter]);
        
        return bulletPool[bulletIter];
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

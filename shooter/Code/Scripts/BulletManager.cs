using Godot;
using System;
using System.Collections.Generic;

public partial class BulletManager : Node
{

    [Export] private int desiredBullets;
    [Export] private PackedScene bulletPrefab;
    private List<Bullet> masterList;
    private List<Bullet> activeList;
    private int bulletIter;
    private int bulletIterMax;
    
    public void Startup()
    {
        
        masterList = new List<Bullet>();
        activeList = new List<Bullet>();
        bulletIter = 0;
        bulletIterMax = desiredBullets;
        
        for (int i = 0; i < desiredBullets; i++)
        {
            Bullet newBullet = bulletPrefab.Instantiate() as Bullet;
            masterList.Add(newBullet);
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

        for (int i = 0; i < activeList.Count; i++)
        {
            activeList[i].MoveBullet(delta);
        }

        activeList.RemoveAll(e => !e.isActive);

    }
    

    public Bullet RequestBullet()
    {
        bulletIter++;
        if (bulletIter == bulletIterMax)
        {
            bulletIter = 0;
        }
        GD.Print("Bullet Iter: " + bulletIter);

        activeList.Add(masterList[bulletIter]);
        
        return masterList[bulletIter];
    }

    public bool SetBulletsInstaKillState()
    {
        for (int i = 0; i < masterList.Count; i++)
        {
            masterList[i].InstaKill = !masterList[i].InstaKill;
        }
        return masterList[0].InstaKill;

    }
    
}

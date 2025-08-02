using Godot;
using System;
using System.Collections.Generic;

public partial class BulletManager : Node
{

    [Export] private int desiredBullets;
    [Export] private PackedScene bulletPrefab;
    private List<Bullet> bulletList;
    private int bulletIter;
    private int bulletIterMax;
    

    public void Startup()
    {
        
        bulletList = new List<Bullet>();
        bulletIter = 0;
        bulletIterMax = desiredBullets;
        
        for (int i = 0; i < desiredBullets; i++)
        {
            Bullet newBullet = bulletPrefab.Instantiate() as Bullet;
            bulletList.Add(newBullet);
            newBullet.Disable();
            GD.Print("Added bullet to list");
            this.AddChild(newBullet);
            
        }
        
    }

    public Bullet RequestBullet()
    {
        bulletIter++;
        if (bulletIter == bulletIterMax)
        {
            bulletIter = 0;
        }

        return bulletList[bulletIter];
    }
    
}

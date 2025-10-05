using Godot;
using System;

public partial class Gun : Node3D
{
//
//     [Export] private PackedScene bulletPrefab;
//     [ExportCategory("Gun Settings")]
//     [Export] private Timer shootTimer;
//     [Export] private bool canShoot;
//
//
//     public void Shoot()
//     {
//         //Instantiate Bullet
//         Bullet bullet = bulletPrefab.Instantiate() as Bullet;
//         bullet.FinalShot += EnemyDefeat;
//         
//         //shootSound.Play();
//         AudioManager.Instance.PlayShootSound();
//         //Set Child
//
//         //Set Position
//         bullet.Position = bulletPosition.GlobalPosition;
//
//         GetTree().Root.AddChild(bullet);
//     }
//     
//     private void EnemyDefeat()
//     {
//         EmitSignal(SignalName.EnemyDefeated);
//     }
//     
}
using Godot;
using System;
using System.Collections.Generic;

public partial class ServerTest : Node3D
{
    private Mesh mesh;

    private List<Rid> instances;
    private float z;

    public override void _Ready()
    {
        instances = new List<Rid>();

        // //create visual instance
        // Rid instance = RenderingServer.InstanceCreate();
        //
        // //set the scenario from the world, ensures the instance appears with the other objects in the scene
        // Rid scenario = GetWorld3D().Scenario;
        // RenderingServer.InstanceSetScenario(instance, scenario);
        //
        // //set base mesh
        // mesh = new BoxMesh();
        // RenderingServer.InstanceSetBase(instance, mesh.GetRid());
        //
        // Transform3D xform = new Transform3D(Basis.Identity, new Vector3(0.0f, 0.0f, 0.0f));
        // RenderingServer.InstanceSetTransform(instance, xform);

        //Creates 10 instances
        for (int i = 0; i < 10; i++)
        {
            //create visual instance
            Rid instance = RenderingServer.InstanceCreate();

            //set the scenario from the world, ensures the instance appears with the other objects in the scene
            Rid scenario = GetWorld3D().Scenario;
            RenderingServer.InstanceSetScenario(instance, scenario);

            //set base mesh
            mesh = new BoxMesh();
            RenderingServer.InstanceSetBase(instance, mesh.GetRid());

            //set the instances transform/position
            Transform3D xform = new Transform3D(Basis.Identity, new Vector3(0.0f, 0.0f, -i));
            RenderingServer.InstanceSetTransform(instance, xform);
            instances.Add(instance);
        }
    }

    public override void _Process(double delta)
    {
        for (int i = 0; i < instances.Count; i++)
        {
            Transform3D xform = new Transform3D(Basis.Identity, new Vector3(0.0f, 0.0f, z));
            RenderingServer.InstanceSetTransform(instances[i], xform);
            z -= 0.01f;
        }
    }
}
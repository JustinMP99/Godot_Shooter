using Godot;
using System;

public partial class PhysicsTestSpawner : Node3D
{
    
    private int objectID;
    //private MeshInstance3D mesh;
    private Mesh mesh;
    
    public override void _Ready()
    {

        //Create Visual instance
        Rid instance = RenderingServer.InstanceCreate();

        //set the scenario from the world, ensures it appears with the same objects as the scene
        Rid scenario = GetWorld3D().Scenario;
        RenderingServer.InstanceSetScenario(instance, scenario);
        

        //Set mesh
        mesh = new BoxMesh();
        RenderingServer.InstanceSetBase(instance, mesh.GetRid());

        
        Transform3D xform = new Transform3D(Basis.Identity, new Vector3(0, 0, 0));
        RenderingServer.InstanceSetTransform(instance,xform);

        // BoxShape3D box_shape = new BoxShape3D();
        // BoxMesh box_mesh = new BoxMesh();
        //
        // PhysicsServer3D ps = new PhysicsServer3D;

        //PHYSICS SERVER
        //create body

        //set space

        //give it a shape

        //set initial transform

        //RENDERING SERVER
        //create instance

        //set its scenario

        //set its transform

        //update transform every frame


    }
}

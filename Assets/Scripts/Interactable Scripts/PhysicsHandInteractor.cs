using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class PhysicsHandInteractor : XRDirectInteractor
{
    public PhysicsHand physicsHand{get; set;}

    protected override void Start(){
        base.Start();
    }

}



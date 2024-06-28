using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandAnimationScript : MonoBehaviour
{
    [SerializeField] XRController controller;
    //[SerializeField] private SkinnedMeshRenderer mesh;
    private Animator handAnimatior;
    
    // Start is called before the first frame update
    void Start()
    {
        handAnimatior = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller == null || handAnimatior == null)
        {
            return;
        }
        InputHelpers.TryReadSingleValue(controller.inputDevice,InputHelpers.Button.Trigger,out float triggerValue);
        handAnimatior.SetFloat("Trigger",triggerValue);

        InputHelpers.TryReadSingleValue(controller.inputDevice,InputHelpers.Button.Grip,out float gripValue);
        handAnimatior.SetFloat("Grip",gripValue);
    }
}

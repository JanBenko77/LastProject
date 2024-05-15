using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimationScript : MonoBehaviour
{
    [SerializeField] private InputActionProperty pinchProperty;
    [SerializeField] private InputActionProperty gripProperty;
    //[SerializeField] private SkinnedMeshRenderer mesh;
    private Animator handAnimatior;
    private MeshCollider meshCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        handAnimatior = gameObject.GetComponent<Animator>();
        meshCollider = gameObject.GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchProperty.action.ReadValue<float>();
        handAnimatior.SetFloat("Trigger", triggerValue);

        float gripValue = gripProperty.action.ReadValue<float>();
        handAnimatior.SetFloat("Grip",gripValue);

        //mesh.
        meshCollider.enabled = false;
        meshCollider.enabled = true;
    }
}

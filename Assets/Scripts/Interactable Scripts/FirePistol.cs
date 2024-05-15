using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class FirePistol : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPpoint;
    [SerializeField] private float fireSpeed;
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable interactable = GetComponent<XRGrabInteractable>();
        interactable.activated.AddListener(FiringPistol);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FiringPistol(ActivateEventArgs args){
       GameObject gameObject = Instantiate(bullet);
       gameObject.transform.position = spawnPpoint.position;
       gameObject.GetComponent<Rigidbody>().velocity = fireSpeed * spawnPpoint.forward;
    }
}

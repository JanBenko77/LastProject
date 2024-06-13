using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{
    public GrassComputeScript grass;
    void Start(){
        grass = FindAnyObjectByType<GrassComputeScript>();
    }
    public void OnHover(){
        Debug.Log("Hover");
    }

    public void OnRestartButton(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

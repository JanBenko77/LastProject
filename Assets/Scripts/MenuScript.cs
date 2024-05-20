using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{
    public void OnHover(){
        Debug.Log("Hover");
    }

    public void OnRestartButton(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

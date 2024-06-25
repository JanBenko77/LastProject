using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitchingManager : GenericSingelton<SceneSwitchingManager>
{
    // Start is called before the first frame update
    byte tasks = 0;
    void Start()
    {
        EventBus<OnTeleporterEntered>.OnEvent += SwitchScene;
        EventBus<OnObjectiveComplete>.OnEvent += OnTaskComplete;
    }

    void SwitchScene(OnTeleporterEntered eventObj){
        SwitchScene();
    }
    public void SwitchScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        tasks = 0;
    }

    private void OnTaskComplete(OnObjectiveComplete eventObj){
        tasks++;
        if (tasks == 2){
            Invoke("SwitchScene", 1);
        }
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}

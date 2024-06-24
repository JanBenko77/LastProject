using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitchingManager : GenericSingelton<SceneSwitchingManager>
{
    // Start is called before the first frame update
    void Start()
    {
        EventBus<OnTeleporterEntered>.OnEvent += SwitchScene;
    }

    void SwitchScene(OnTeleporterEntered eventObj){
        SwitchScene();
    }
    public void SwitchScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}

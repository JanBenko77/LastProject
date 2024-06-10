using UnityEngine;
using TMPro;
public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fpsText;
    // Start is called before the first frame update
    void Start()
    {
        if(_fpsText == null)
            _fpsText = GetComponent<TextMeshProUGUI>();
        InvokeRepeating("GetFPS",1,.2f);
    }

    // Update is called once per frames
    void GetFPS()
    {
        _fpsText.text ="FPS: " + (1f/Time.deltaTime).ToString();
    }
}

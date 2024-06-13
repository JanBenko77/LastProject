using UnityEngine;
using TMPro;
public class FPSCounter : MonoBehaviour
{
    int timeout;
    int maxTime = 10;
    [SerializeField] private TextMeshProUGUI _fpsText;
    // Start is called before the first frame update
    void Start()
    {
        if(_fpsText == null)
            _fpsText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frames
    void Update()
    {
        switch (timeout)
        {
            case 0:{
                _fpsText.text ="FPS: " + Mathf.Floor(1f/Time.unscaledDeltaTime).ToString();
                timeout = maxTime;
            }
            break;

            default:
                timeout--;
            break;
        }
    }
}

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
    }

    // Update is called once per frames
    void Update()
    {
        _fpsText.text ="FPS: " + (1f/Time.unscaledDeltaTime).ToString();
    }
}

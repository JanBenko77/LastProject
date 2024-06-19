using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    public float speedScale = 1f;
    public Color fadeColor = Color.black;
    public AnimationCurve curve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(0.5f, 0.5f, -1.5f, -1.5f), new Keyframe(1, 0));

    public float alpha = 0f;
    private Texture2D texture;
    private int direction = 0;
    private float time = 0f;

    public Canvas canvas;

    private void Start()
    {
        texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, fadeColor);
        texture.Apply();
    }

    private void OnGUI()
    {

        if (alpha > 0f) GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
        if (direction != 0)
        {
            time += direction * Time.deltaTime * speedScale;
            alpha = curve.Evaluate(time);
            texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
            texture.Apply();
            if (alpha <= 0f)
            {
                direction = 0;
            }
            else if (alpha >= 1f)
            {
                direction = 0;
                Debug.Log("Animation finished");
                EventBus<OnAnimationComplete>.Invoke(new OnAnimationComplete());
                StartCoroutine(FadeCoroutine());
            }
        }
    }

    private void OnEnable()
    {
        EventBus<OnTeleporterEntered>.OnEvent += FadeOut;
    }

    private void OnDisable()
    {
        EventBus<OnTeleporterEntered>.OnEvent -= FadeOut;
    }

    private void FadeOut(OnTeleporterEntered pEvent)
    {
        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        if (direction == 0)
        {
            if (alpha >= 1f)
            {
                alpha = 1f;
                time = 0f;
                direction = 1;
            }
            else
            {
                alpha = 0f;
                time = 1f;
                direction = -1;
            }
        }
        yield return null;
    }
}

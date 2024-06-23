using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GayJesusScript : MonoBehaviour
{
    private void AnimationComplete()
    {
        Debug.Log("Animation Complete");
        EventBus<OnAnimationComplete>.Invoke(new OnAnimationComplete());
    }
}

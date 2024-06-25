using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour
{

    public void THROWTHEFUCKINGPLAYER()
    {
        EventBus<OnPlayerThrow>.Invoke(new OnPlayerThrow());
    }
}

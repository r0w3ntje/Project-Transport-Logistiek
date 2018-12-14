using System.Collections;
using System.Collections.Generic;
using Systems.Singleton;
using UnityEngine;
using UnityEngine.UI;

public class CursorHoverInfo : Singleton<CursorHoverInfo>
{
    public Text hoverText;

    private void Update()
    {
        if (hoverText.enabled)
        {
            transform.position = Input.mousePosition;
        }
    }
}
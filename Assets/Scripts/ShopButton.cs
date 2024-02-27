using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public Canvas canvasToToggle;

    void Start()
    {
        if (canvasToToggle != null)
            canvasToToggle.enabled = false;
    }

    public void OpenShop()
    {
        if (canvasToToggle != null)
            canvasToToggle.enabled = true;
    }

    public void CloseShop()
    {
        if (canvasToToggle != null)
            canvasToToggle.enabled = false;
    }
}

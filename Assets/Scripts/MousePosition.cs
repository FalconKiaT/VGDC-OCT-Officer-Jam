using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    // Local fields
    private RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();   
    }

    void Update()
    {
        rect.position = Input.mousePosition;
    }
}

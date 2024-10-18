using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    [Header("Mouse Transform")]
    [SerializeField] private float speed;
    [SerializeField] Vector3 offset;

    void Update()
    {
        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = speed;
        transform.position = Camera.main.ScreenToWorldPoint(mousePos) + offset;
    }
}

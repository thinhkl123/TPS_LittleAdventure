using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float inputHorizontal;
    public float inputVertical;
    public bool mouseLeftButtonDown;
    public bool mouseRightButtonDown;
    public bool mouseRightButtonUp;
    public bool isInputSlide;
    public bool spaceKeyDown;

    //Look
    public Vector2 look;
    public float sensitivityX = 0.05f;
    public float sensitivityY = 0.05f;

    public string currentControlScheme;

    private void Update()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        spaceKeyDown = Input.GetKeyDown(KeyCode.Space);
        mouseLeftButtonDown = Input.GetMouseButtonDown(0);
        mouseRightButtonDown = Input.GetMouseButton(1);
        mouseRightButtonUp = Input.GetMouseButtonUp(1);
        look = GetMouseDelta();
        isInputSlide = Input.GetKeyDown(KeyCode.LeftShift);
    }

    private Vector2 GetMouseDelta()
    {
        // Get the mouse delta (movement)
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");

        // Store the delta as a Vector2
        Vector2 ans = new Vector2(deltaX, deltaY);

        // Optionally invert the Y axis like in the image (if needed)
        ans.y = -ans.y;

        return ans;
    }
}

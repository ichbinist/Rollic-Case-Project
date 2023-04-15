using UnityEngine;

public class LevelEditorCameraController : MonoBehaviour
{
    public float ScrollSpeed = 10f;
    public float DragSpeed = 2f;
    public float ScreenEdgeBorderThickness = 10f;

    private void Update()
    {
        float mouseY = Input.mousePosition.y;

        if (mouseY < ScreenEdgeBorderThickness)
        {
            transform.position -= new Vector3(0f, 0f, DragSpeed * Time.deltaTime);
        }
        else if (mouseY >= Screen.height - ScreenEdgeBorderThickness)
        {
            transform.position += new Vector3(0f, 0f, DragSpeed * Time.deltaTime);
        }
    }
}
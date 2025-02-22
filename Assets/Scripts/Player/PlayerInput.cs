using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string MouseY = "Mouse Y";
    private const string MouseX = "Mouse X";

    public float HorizontalDirection { get; private set; }
    public float VerticalDirection { get; private set; }
    public float MouseYDirection { get;private set; }
    public float MouseXDirection { get; private set; }

    private void Update()
    {
        HorizontalDirection = Input.GetAxis(Horizontal);
        VerticalDirection = Input.GetAxis(Vertical);
        MouseYDirection = Input.GetAxis(MouseY);
        MouseXDirection = Input.GetAxis(MouseX);
    }
}
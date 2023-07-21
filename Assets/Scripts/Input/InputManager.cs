using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private PlayerControls controls;

    public delegate void Jump();

    public event Jump OnJump;

    public delegate void Vsync(bool increase);

    public event Vsync OnVsyncPressed;

    protected override void Awake()
    {
        base.Awake();
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Enable();

        controls.Land.Jump.performed += _ => { OnJump?.Invoke(); };
        controls.Land.VsyncUp.performed += _ => OnVsyncPressed?.Invoke(true);
        controls.Land.VsyncDown.performed += _ => OnVsyncPressed?.Invoke(false);

        Cursor.lockState = CursorLockMode.Confined;
    }

    public Vector2 GetMoveInput()
    {
        return controls.Land.Move.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
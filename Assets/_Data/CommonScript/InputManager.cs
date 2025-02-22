using UnityEngine;

public class InputManager : SaiSingleton<InputManager>
{
    protected virtual void Update()
    {
        this.ControlByKeyboard();
    }

    protected virtual void ControlByKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.I)) UIInventory.Instance.Toggle();
    }
}

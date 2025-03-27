using UnityEngine;

public class InputManager : MonoBehaviour
{

    public bool ChekThrust { get; private set; }
    public float TurnDirection { get; private set; }
    public bool CheckShooting { get; private set; }

    private void Update()
    {
        ChekThrust = Input.GetKey(KeyCode.W);
        TurnDirection = Input.GetAxis("Horizontal");
        CheckShooting = Input.GetMouseButtonDown(0);
    }
}

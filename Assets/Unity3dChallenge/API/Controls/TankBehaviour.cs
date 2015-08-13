using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class TankBehaviour : MonoBehaviour {

    public float Speed = 800;
    public float RotationSpeed = 200;
    public float TurretRotationSpeed = 100;
    public float Gravity = 20.0f;

    public Transform Turret;

    private CharacterController _characterController;

    // Use this for initialization
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateTurret();
        MoveBase();
    }

    private void RotateTurret()
    {
        if (Turret == null) return;

        if (Input.GetKey(KeyCode.Keypad1))
        {
            var rotation = TurretRotationSpeed * Time.deltaTime;
            Turret.Rotate(Vector3.back * rotation);
        }

        if (Input.GetKey(KeyCode.Keypad3))
        {
            var rotation = TurretRotationSpeed * Time.deltaTime;
            Turret.Rotate(Vector3.forward * rotation);
        }
    }

    private void MoveBase()
    {
        //if (!_characterController.isGrounded) return;
        var v_Axis = Input.GetAxis("Vertical");
        var h_Axis = Input.GetAxis("Horizontal");
        //if (v_Axis == 0 && h_Axis == 0) return;

        var translation = v_Axis * Speed * Time.deltaTime;
        var rotation = h_Axis * RotationSpeed * Time.deltaTime;
        var direction = transform.TransformDirection(Vector3.forward) * translation;
        direction.y -= Gravity * Time.deltaTime;
        _characterController.Move(direction);

        var eulerRotation = transform.eulerAngles;
        eulerRotation.y = eulerRotation.y + rotation;
        transform.eulerAngles = eulerRotation;
    }
}

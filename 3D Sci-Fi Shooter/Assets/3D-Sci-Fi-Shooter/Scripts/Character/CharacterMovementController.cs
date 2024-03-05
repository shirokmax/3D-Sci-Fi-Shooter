using UnityEngine;

namespace SciFiShooter
{
    public class CharacterMovementController : MonoBehaviour
    {
        [SerializeField] private CharacterMovement m_TargetCharacterMovement;
        [SerializeField] private ThirdPersonCamera m_TargetCamera;
        [SerializeField] private Vector3 m_AimingCameraOffset;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            m_TargetCharacterMovement.TargetDirectionControl = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            m_TargetCamera.RotationControl = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            if (m_TargetCharacterMovement.TargetDirectionControl != Vector3.zero || m_TargetCharacterMovement.IsAiming)
                m_TargetCamera.IsRotateTarget = true;
            else
                m_TargetCamera.IsRotateTarget = false;

            if (Input.GetButtonDown("Jump"))
                m_TargetCharacterMovement.Jump();

            if (Input.GetKeyDown(KeyCode.LeftControl))
                m_TargetCharacterMovement.Crouch();

            if (Input.GetKeyUp(KeyCode.LeftControl))
                m_TargetCharacterMovement.UnCrouch();

            if (Input.GetKeyDown(KeyCode.LeftShift))
                m_TargetCharacterMovement.Sprint();

            if (Input.GetKeyUp(KeyCode.LeftShift))
                m_TargetCharacterMovement.UnSprint();

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                m_TargetCharacterMovement.Aiming();
                m_TargetCamera.SetTargetOffset(m_AimingCameraOffset);
            }

            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                m_TargetCharacterMovement.UnAiming();
                m_TargetCamera.SetDefaultOffset();
            }
        }
    }
}

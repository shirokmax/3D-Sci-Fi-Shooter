using UnityEngine;

namespace SciFiShooter
{
    public class CharacterMovementController : MonoBehaviour
    {
        [SerializeField] private CharacterMovement m_TargetCharacterMovement;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            m_TargetCharacterMovement.TargetDirectionControl = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

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
                m_TargetCharacterMovement.Aiming();

            if (Input.GetKeyUp(KeyCode.Mouse1))
                m_TargetCharacterMovement.UnAiming();
        }
    }
}

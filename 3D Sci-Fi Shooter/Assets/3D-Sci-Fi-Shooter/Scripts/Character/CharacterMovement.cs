using UnityEngine;

namespace SciFiShooter
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] CharacterController m_CharacterController;

        [Header("Movement")]
        [SerializeField] private float m_RifleWalkSpeed;
        [SerializeField] private float m_RifleSprintSpeed;

        [Space]
        [SerializeField] private float m_AimingWalkSpeed;
        [SerializeField] private float m_AimingSprintSpeed;

        [Space]
        [SerializeField] private float m_CrouchSpeed;
        [SerializeField] private float m_JumpSpeed;

        [Space]
        [SerializeField] private float m_AccelerationRate;

        [Header("State")]
        [SerializeField] private float m_CrouchHeight;

        private float m_BaseCharacterHeight;
        private float m_BaseCharacterHeightOffset;

        private bool m_IsAiming;
        private bool m_IsJump;
        private bool m_IsCrouch;
        private bool m_IsSprint;
        public bool IsAiming => m_IsAiming;
        public bool IsCrouch => m_IsCrouch;
        public bool IsSprint => m_IsSprint;

        private float m_DistanceToGround;
        public float DistanceToGround => m_DistanceToGround;
        public bool IsGrounded => m_DistanceToGround < 0.01f;
        public Vector3 Velocity => m_CharacterController.velocity;

        private Vector3 m_MovementDirection;
        private Vector3 m_DirectionControl;

        public Vector3 TargetDirectionControl { get; set; }

        private void Start()
        {
            m_BaseCharacterHeight = m_CharacterController.height;
            m_BaseCharacterHeightOffset = m_CharacterController.center.y;
        }

        private void Update()
        {
            Move();
            UpdateDistanceToGround();
        }

        private void Move()
        {
            m_DirectionControl = Vector3.MoveTowards(m_DirectionControl, TargetDirectionControl, m_AccelerationRate * Time.deltaTime);

            if (IsGrounded)
            {
                m_MovementDirection = m_DirectionControl * GetCurrentSpeedByState();

                if (m_IsJump == true)
                {
                    m_MovementDirection.y = m_JumpSpeed;
                    m_IsJump = false;
                }

                m_MovementDirection = transform.TransformDirection(m_MovementDirection);
            }

            m_MovementDirection += Physics.gravity * Time.deltaTime;

            m_CharacterController.Move(m_MovementDirection * Time.deltaTime);
        }
        public float GetCurrentSpeedByState()
        {
            if (m_IsCrouch == true)
                return m_CrouchSpeed;

            if (m_IsAiming == true)
            {
                if (m_IsSprint == true)
                    return m_AimingSprintSpeed;
                else
                    return m_AimingWalkSpeed;
            }
            else
            {
                if (m_IsSprint == true)
                    return m_RifleSprintSpeed;
                else
                    return m_RifleWalkSpeed;
            }
        }

        public void Jump()
        {
            if (IsGrounded == false)
                return;

            if (m_IsAiming == true) return;
            if (m_IsCrouch == true) return;

            m_IsJump = true;
        }

        public void Crouch()
        {
            if (IsGrounded == false)
                return;

            if (m_IsSprint == true)
                return;

            m_IsCrouch = true;

            m_CharacterController.height = m_CrouchHeight;
            m_CharacterController.center = new Vector3(0, m_CrouchHeight/2, 0);
        }

        public void UnCrouch()
        {
            m_IsCrouch = false;

            m_CharacterController.height = m_BaseCharacterHeight;
            m_CharacterController.center = new Vector3(0, m_BaseCharacterHeightOffset, 0);
        }

        public void Sprint()
        {
            if (IsGrounded == false)
                return;

            if (m_IsCrouch == true)
                return;

            m_IsSprint = true;
        }

        public void UnSprint()
        {
            m_IsSprint = false;
        }

        public void Aiming()
        {
            m_IsAiming = true;
        }

        public void UnAiming()
        {
            m_IsAiming = false;
        }

        private void UpdateDistanceToGround()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
                m_DistanceToGround = hit.distance;    
        }
    }
}

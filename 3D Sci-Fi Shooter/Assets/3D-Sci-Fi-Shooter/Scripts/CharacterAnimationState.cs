using UnityEngine;

namespace SciFiShooter
{
    public class CharacterAnimationState : MonoBehaviour
    {
        [SerializeField] CharacterController m_CharacterController;
        [SerializeField] private CharacterMovement m_CharacterMovement;
        [SerializeField] private Animator m_Animator;

        private void Update()
        {
            Vector3 movementSpeed = m_CharacterController.velocity;

            m_Animator.SetFloat("NormalizeMovementX", movementSpeed.x / m_CharacterMovement.GetCurrentSpeedByState());
            m_Animator.SetFloat("NormalizeMovementZ", movementSpeed.z / m_CharacterMovement.GetCurrentSpeedByState());

            m_Animator.SetBool("IsCrouch", m_CharacterMovement.IsCrouch);
            m_Animator.SetBool("IsSprint", m_CharacterMovement.IsSprint);
            m_Animator.SetBool("IsAiming", m_CharacterMovement.IsAiming);
            m_Animator.SetBool("IsGrounded", m_CharacterMovement.IsGrounded);

            if (m_CharacterController.isGrounded == false)
                m_Animator.SetFloat("Jump", movementSpeed.y);

            m_Animator.SetFloat("DistanceToGround", m_CharacterMovement.DistanceToGround);
            m_Animator.SetFloat("GroundSpeed", new Vector3(movementSpeed.x, 0, movementSpeed.z).magnitude);
        }
    }
}

using UnityEngine;

namespace SciFiShooter
{
    public class CharacterAnimationState : MonoBehaviour
    {
        private const float INPUT_CONTROL_LERP_RATE = 10f;

        [SerializeField] private CharacterMovement m_CharacterMovement;
        [SerializeField] private Animator m_Animator;

        private Vector3 m_InputControl;

        private void Update()
        {
            Vector3 movementSpeed = transform.InverseTransformDirection(m_CharacterMovement.Velocity);

            m_InputControl = Vector3.MoveTowards(m_InputControl, m_CharacterMovement.TargetDirectionControl, Time.deltaTime * INPUT_CONTROL_LERP_RATE);

            m_Animator.SetFloat("NormalizeMovementX",m_InputControl.x);
            m_Animator.SetFloat("NormalizeMovementZ",m_InputControl.z);

            m_Animator.SetBool("IsCrouch", m_CharacterMovement.IsCrouch);
            m_Animator.SetBool("IsSprint", m_CharacterMovement.IsSprint);
            m_Animator.SetBool("IsAiming", m_CharacterMovement.IsAiming);
            m_Animator.SetBool("IsGrounded", m_CharacterMovement.IsGrounded);

            if (m_CharacterMovement.IsGrounded == false)
                m_Animator.SetFloat("Jump", movementSpeed.y);

            m_Animator.SetFloat("DistanceToGround", m_CharacterMovement.DistanceToGround);
            m_Animator.SetFloat("GroundSpeed", new Vector3(movementSpeed.x, 0, movementSpeed.z).magnitude);
        }
    }
}

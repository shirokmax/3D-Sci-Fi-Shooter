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
        }
    }
}

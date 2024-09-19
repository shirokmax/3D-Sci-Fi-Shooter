using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace SciFiShooter
{
    public class AimingRig : MonoBehaviour
    {
        [SerializeField] private CharacterMovement m_TargetCharacter;
        [SerializeField] private Rig[] m_Rigs;
        [SerializeField] private float m_ChangeWeightLerpRate;

        private float m_TargetWeight;

        private void Update()
        {
            if (m_TargetCharacter.IsAiming)
                m_TargetWeight = 1;
            else
                m_TargetWeight = 0;

            foreach (var rig in m_Rigs)
                rig.weight = Mathf.MoveTowards(rig.weight, m_TargetWeight, m_ChangeWeightLerpRate * Time.deltaTime);
        }
    }
}
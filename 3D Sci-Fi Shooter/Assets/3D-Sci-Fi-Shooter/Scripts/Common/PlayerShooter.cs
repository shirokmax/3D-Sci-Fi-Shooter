using UnityEngine;
using UnityEngine.UI;

namespace SciFiShooter
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private CharacterMovement m_CharacterMovement;
        [SerializeField] private Weapon m_Weapon;
        [SerializeField] private Camera m_Camera;
        [SerializeField] private RectTransform m_ImageSight;
        [SerializeField] private float m_FirePointDefaultDistance = 30;

        public void Shoot()
        {
            Ray ray = m_Camera.ScreenPointToRay(m_ImageSight.position);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000))
                m_Weapon.FirePointLookAt(hit.point);
            else
                m_Weapon.FirePointLookAt(ray.GetPoint(m_FirePointDefaultDistance));

            m_Weapon.Fire();
        }
    }
}

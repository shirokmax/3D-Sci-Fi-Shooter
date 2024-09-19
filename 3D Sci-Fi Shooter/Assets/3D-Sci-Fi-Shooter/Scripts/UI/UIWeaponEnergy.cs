using UnityEngine;
using UnityEngine.UI;

namespace SciFiShooter
{
    [RequireComponent(typeof(Slider))]
    public class UIWeaponEnergy : MonoBehaviour
    {
        [SerializeField] private Weapon m_TargetWeapon;

        [SerializeField] private Image m_Backgroundmage;
        [SerializeField] private Image m_FillImage;

        private Slider m_Slider;
        private Color m_EnergyColor;

        private void Awake()
        {
            m_Slider = GetComponent<Slider>();
            m_EnergyColor = m_FillImage.color;
        }

        private void Start()
        {
            m_Slider.maxValue = m_TargetWeapon.PrimaryMaxEnergy;
            m_Slider.value = m_Slider.maxValue;
        }

        private void Update()
        {
            m_Slider.value = m_TargetWeapon.PrimaryEnergy;

            if (m_TargetWeapon.EnergyRestoring == true)
                m_FillImage.color = new Color(1, 0, 0);
            else
                m_FillImage.color = m_EnergyColor;

            SetActiveImages(m_TargetWeapon.PrimaryEnergy != m_TargetWeapon.PrimaryMaxEnergy);
        }

        private void SetActiveImages(bool active)
        {
            m_Backgroundmage.enabled = active;
            m_FillImage.enabled = active;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

namespace SciFiShooter
{
    [RequireComponent(typeof(Image))]
    public class UISight : MonoBehaviour
    {
        [SerializeField] private CharacterMovement m_CharacterMovement;

        private Image m_SightImage;

        private void Awake()
        {
            m_SightImage = GetComponent<Image>();
        }

        private void Update()
        {
            m_SightImage.enabled = m_CharacterMovement.IsAiming;
        }
    }
}

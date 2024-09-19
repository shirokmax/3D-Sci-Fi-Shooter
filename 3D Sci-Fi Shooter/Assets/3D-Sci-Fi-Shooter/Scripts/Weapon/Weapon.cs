using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region Properties
    [SerializeField] private WeaponMode m_Mode;
    public WeaponMode Mode => m_Mode;

    [SerializeField] private WeaponProperties m_StartWeaponProperties;

    [SerializeField] private Transform m_FirePoint;

    [SerializeField] private float m_PrimaryMaxEnergy;
    public float PrimaryMaxEnergy => m_PrimaryMaxEnergy;

    private WeaponProperties m_WeaponProperties;
    public WeaponProperties TurretProperties => m_WeaponProperties;

    private float m_PrimaryEnergy;
    public float PrimaryEnergy => m_PrimaryEnergy;

    private bool m_EnergyRestoring;
    public bool EnergyRestoring => m_EnergyRestoring;

    private float m_RefireTimer;

    private float m_AssignLoadoutTimer;
    public float AssignLoadoutTimer => m_AssignLoadoutTimer;
    public float AssignLoadoutLastDurationTime { get; private set; }
    public bool CanFire => m_RefireTimer <= 0 && m_EnergyRestoring == false;
    public bool AssignLoadoutEnd => m_AssignLoadoutTimer <= 0;

    #endregion

    #region UnityEvents
    private void Start()
    {
        m_WeaponProperties = m_StartWeaponProperties;
        m_PrimaryEnergy = m_PrimaryMaxEnergy;
    }

    private void Update()
    {
        if (m_RefireTimer > 0)
            m_RefireTimer -= Time.deltaTime;

        if (m_AssignLoadoutTimer > 0)
            m_AssignLoadoutTimer -= Time.deltaTime;

        if (AssignLoadoutEnd == true && m_WeaponProperties != m_StartWeaponProperties)
            AssignLoadout(m_StartWeaponProperties, 0);

        UpdateEnergy();
    }

    #endregion

    #region Public API
    public void Fire()
    {
        if (m_WeaponProperties == null) return;

        if (CanFire == false) return;

        if (TryDrawEnergy(m_WeaponProperties.EnergyUsage) == false) return;

        Projectile projectile = Instantiate(m_WeaponProperties.ProjectilePrefab);
        projectile.transform.position = m_FirePoint.position;
        projectile.transform.forward = m_FirePoint.forward;

        m_RefireTimer = m_WeaponProperties.FireRate;
    }

    public void FirePointLookAt(Vector3 pos)
    {
        m_FirePoint.LookAt(pos);
    }

    public void AssignLoadout(WeaponProperties props, float assignTime)
    {
        if (props == null) return;
        if (m_Mode != props.Mode) return;

        m_RefireTimer = 0;
        m_AssignLoadoutTimer = assignTime;

        AssignLoadoutLastDurationTime = assignTime;

        m_WeaponProperties = props;
    }

    #endregion

    #region Private API
    private void UpdateEnergy()
    {
        m_PrimaryEnergy += m_WeaponProperties.EnergyRegenPerSecond * Time.deltaTime;
        m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_PrimaryMaxEnergy);

        if (m_PrimaryEnergy >= m_WeaponProperties.EnergyAmountToStartFire)
            m_EnergyRestoring = false;
    }

    private bool TryDrawEnergy(float value)
    {
        if (value == 0)
            return true;

        if (m_PrimaryEnergy >= value)
        {
            m_PrimaryEnergy -= value;
            return true;
        }

        m_EnergyRestoring = true;

        return false;
    }

    #endregion
}

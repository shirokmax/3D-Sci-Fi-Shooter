using UnityEngine;

[CreateAssetMenu]
public sealed class WeaponProperties : ScriptableObject
{
    [SerializeField] private WeaponMode m_Mode;
    public WeaponMode Mode => m_Mode;

    [SerializeField] private Projectile m_ProjectilePrefab;
    public Projectile ProjectilePrefab => m_ProjectilePrefab;

    [SerializeField] private float m_FireRate;
    public float FireRate => m_FireRate;

    [SerializeField] private float m_EnergyUsage;
    public float EnergyUsage => m_EnergyUsage;

    [SerializeField] private float m_EnergyRegenPerSecond;
    public float EnergyRegenPerSecond => m_EnergyRegenPerSecond;

    [SerializeField] private float m_EnergyAmountToStartFire;
    public float EnergyAmountToStartFire => m_EnergyAmountToStartFire;
}

using UnityEngine;

public class Projectile : Entity
{
    [SerializeField] protected float m_Speed;
    public float Speed => m_Speed;

    [SerializeField] protected float m_Lifetime;
    [SerializeField] protected int m_Damage;
    [SerializeField] protected ImpactEffect m_LaunchSFXPrefab;
    [SerializeField] protected ImpactEffect m_HitEffectPrefab;

    protected Destructible m_ParentDest;

    protected float m_Timer;

    private void Start()
    {
        if (m_LaunchSFXPrefab != null)
            Instantiate(m_LaunchSFXPrefab, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        ProjectileMovement();
    }

    protected virtual void ProjectileMovement()
    {
        float stepLength = m_Speed * Time.fixedDeltaTime;
        Vector3 step = transform.forward * stepLength;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.up,out hit, stepLength))
        {
            if (hit.collider.transform.root.TryGetComponent(out Destructible dest) && dest != m_ParentDest)
                dest.ApplyDamage(m_Damage);

            if (dest != m_ParentDest) OnProjectileHit(hit.collider, hit.point);
        }

        m_Timer += Time.fixedDeltaTime;

        if (m_Timer >= m_Lifetime)
            Destroy(gameObject);

        transform.position += new Vector3(step.x, step.y, step.z);
    }

    protected void OnProjectileHit(Collider collider, Vector3 pos)
    {
        Destroy(gameObject);

        if (m_HitEffectPrefab != null)
            Instantiate(m_HitEffectPrefab, pos, Quaternion.identity);
    }

    public void SetParentShooter(Destructible dest)
    {
        m_ParentDest = dest;
    }
}
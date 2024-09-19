using UnityEngine;

public class CubeArea : MonoBehaviour
{
    [SerializeField] private Vector3 m_AreaSize;
    [SerializeField] private ColorStyle m_ColorStyle;

    public Vector3 GetRandomInsideZone()
    {
        Vector3 result = transform.position;

        result.x += Random.Range(-m_AreaSize.x / 2, m_AreaSize.x / 2);
        result.y += Random.Range(-m_AreaSize.y / 2, m_AreaSize.y / 2);
        result.z += Random.Range(-m_AreaSize.z / 2, m_AreaSize.z / 2);

        return result;
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        switch (m_ColorStyle)
        {
            case ColorStyle.Danger:
                Gizmos.color = new Color(1, 0, 0, 0.1f);
                break;
            case ColorStyle.Neutral:
                Gizmos.color = new Color(1, 1, 0, 0.1f);
                break;
            case ColorStyle.Friendly:
                Gizmos.color = new Color(0, 1, 0, 0.1f);
                break;
            default:
                Gizmos.color = new Color(1, 1, 0, 0.1f);
                break;
        }

        Gizmos.DrawWireCube(transform.position, m_AreaSize);
    }

#endif

}
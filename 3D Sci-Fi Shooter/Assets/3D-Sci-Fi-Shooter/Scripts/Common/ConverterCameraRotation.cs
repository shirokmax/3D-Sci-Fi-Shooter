using UnityEngine;

public class ConverterCameraRotation : MonoBehaviour
{
    [SerializeField] private Transform m_Camera;
    [SerializeField] private Transform m_CameraLook;
    [SerializeField] private Vector3 m_LookOffset;

    [SerializeField] private float m_TopAngleLimit;
    [SerializeField] private float m_BottomAngleLimit;

    void Update()
    {
        Vector3 angles = Vector3.zero;

        angles.z = m_Camera.eulerAngles.x;

        if (angles.z >= m_TopAngleLimit || angles.z <= m_BottomAngleLimit)
        {
            transform.LookAt(m_CameraLook.position + m_LookOffset);

            angles.x = transform.eulerAngles.x;
            angles.y = transform.eulerAngles.y;

            transform.eulerAngles = angles;
        }
    }
    
}

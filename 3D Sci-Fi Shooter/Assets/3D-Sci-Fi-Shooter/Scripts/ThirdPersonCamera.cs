using TMPro;

using UnityEngine;

namespace SciFiShooter
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        [SerializeField] private Transform m_Target;
        [SerializeField] private Vector3 m_Offset;
        [SerializeField] private float m_ChangeOffsetRate;
        [SerializeField] private float m_Sensitive;

        [SerializeField] private float m_RotateTargetLerpRate;

        [Header("Rotation Limit")]
        [SerializeField] private float MaxLimitY;
        [SerializeField] private float MinLimitY;

        [Header("Distance")]
        [SerializeField] private float m_Distance;
        [SerializeField] private float m_MinDistance;
        [SerializeField] private float m_DistanceLerpRate;
        [SerializeField] private float m_DistanceOffsetFromCollisionHit;

        public bool IsRotateTarget { get; set; }
        public Vector2 RotationControl { get; set; }

        private float m_DeltaRotationX;
        private float m_DeltaRotationY;

        private float m_CurrentDistance;

        private Vector3 m_DefaultOffset;
        private Vector3 m_TargetOffset;

        private void Start()
        {
            m_DefaultOffset = m_Offset;
            m_TargetOffset = m_Offset;
        }

        private void Update()
        {
            // Calculate rotation and translation
            m_DeltaRotationX += RotationControl.x * m_Sensitive;
            m_DeltaRotationY += RotationControl.y * -m_Sensitive;

            m_DeltaRotationY = ClampAngle(m_DeltaRotationY, MinLimitY, MaxLimitY);

            m_Offset = Vector3.MoveTowards(m_Offset, m_TargetOffset, m_ChangeOffsetRate * Time.deltaTime);

            Quaternion finalRotation = Quaternion.Euler(m_DeltaRotationY, m_DeltaRotationX, 0);
            Vector3 finalPosition = m_Target.position - (finalRotation * Vector3.forward * m_Distance);
            finalPosition = AddLocalOffset(finalPosition);

            // Calculate current distance
            float targetDistance = m_Distance;

            if (Physics.Linecast(m_Target.position + new Vector3(0, m_Offset.y, 0), finalPosition, out RaycastHit hit))
            {
                float distanceToHit = Vector3.Distance(m_Target.position + new Vector3(0, m_Offset.y, 0), hit.point);

                if (hit.transform != m_Target)
                {
                    if (distanceToHit < m_Distance)
                        targetDistance = distanceToHit - m_DistanceOffsetFromCollisionHit;
                }
            }

            m_CurrentDistance = Mathf.MoveTowards(m_CurrentDistance, targetDistance, Time.deltaTime * m_DistanceLerpRate);
            m_CurrentDistance = Mathf.Clamp(m_CurrentDistance, m_MinDistance, m_Distance);

            // Correct camera position
            finalPosition = m_Target.position - (finalRotation * Vector3.forward * m_CurrentDistance);

            // Apply transform
            transform.rotation = finalRotation;
            transform.position = finalPosition;
            transform.position = AddLocalOffset(transform.position);

            // Rotation target
            if (IsRotateTarget == true)
            {
                Quaternion targetRotation = Quaternion.Euler(transform.rotation.x, transform.eulerAngles.y, transform.eulerAngles.z);
                m_Target.rotation = Quaternion.RotateTowards(m_Target.rotation, targetRotation, Time.deltaTime * m_RotateTargetLerpRate);
            }
        }

        private Vector3 AddLocalOffset(Vector3 position)
        {
            Vector3 resultPosition = position;

            resultPosition += new Vector3(0, m_Offset.y, 0);
            resultPosition += transform.right * m_Offset.x;
            resultPosition += transform.forward * m_Offset.z;

            return resultPosition;
        }

        private float ClampAngle(float angle, float min, float max)
        {
            if (angle > 360) angle -= 360;
            if (angle < -360) angle += 360;

            return Mathf.Clamp(angle, min, max);
        }

        public void SetTargetOffset(Vector3 offset)
        {
            m_TargetOffset = offset;
        }

        public void SetDefaultOffset()
        {
            m_TargetOffset = m_DefaultOffset;
        }
    }
}

using UnityEngine;

#if UNITY_EDITOR

namespace Editor
{
    using UnityEditor;

    [CustomEditor(typeof(Monster.FieldOfView))]
    public class FieldOfView : Editor
    {
        private void OnSceneGUI()
        {
            var fov = (Monster.FieldOfView)target;
            Handles.color = Color.white;
            Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360f, fov.Radius);

            Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.Angle / 2f);
            Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.Angle / 2f);
            
            Handles.color = Color.yellow;
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.Radius);
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.Radius);

            if (fov.CanSeePlayer)
            {
                Handles.color = Color.green;
                Handles.DrawLine(fov.transform.position, fov.PlayerGameObject.transform.position);
            }
        }

        private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0f,
                Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}

#endif
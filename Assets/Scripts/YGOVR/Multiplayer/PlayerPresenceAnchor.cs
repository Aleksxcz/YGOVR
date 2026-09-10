using UnityEngine;

namespace YGOVR.Multiplayer
{
    public class PlayerPresenceAnchor : MonoBehaviour
    {
        [SerializeField] private string playerId;
        [SerializeField] private Transform headTransform;
        [SerializeField] private Transform leftHandTransform;
        [SerializeField] private Transform rightHandTransform;

        public string PlayerId => playerId;

        public void AssignPlayer(string id)
        {
            playerId = id;
        }

        public void ApplyPose(Vector3 headPosition, Quaternion headRotation, Vector3 leftHandPosition, Quaternion leftHandRotation, Vector3 rightHandPosition, Quaternion rightHandRotation)
        {
            ApplyTransform(headTransform, headPosition, headRotation);
            ApplyTransform(leftHandTransform, leftHandPosition, leftHandRotation);
            ApplyTransform(rightHandTransform, rightHandPosition, rightHandRotation);
        }

        private static void ApplyTransform(Transform target, Vector3 worldPosition, Quaternion worldRotation)
        {
            if (target == null)
            {
                return;
            }

            target.SetPositionAndRotation(worldPosition, worldRotation);
        }
    }
}

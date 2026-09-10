using System;
using UnityEngine;

namespace YGOVR.Gameplay
{
    public enum DuelSide
    {
        Near,
        Far
    }

    public enum PlaymatZoneType
    {
        Monster,
        SpellTrap,
        Field,
        Deck,
        Graveyard,
        Banished,
        ExtraDeck
    }

    [CreateAssetMenu(fileName = "DuelPlaymatLayout", menuName = "YGOVR/Gameplay/Duel Playmat Layout")]
    public class DuelPlaymatLayout : ScriptableObject
    {
        [Serializable]
        private class ZoneLayout
        {
            [SerializeField] private PlaymatZoneType zoneType;
            [SerializeField] private int slotCount = 1;
            [SerializeField] private Vector3 rowCenter;
            [SerializeField] private Vector3 slotSpacing = new(0.2f, 0f, 0f);

            public PlaymatZoneType ZoneType => zoneType;
            public int SlotCount => Mathf.Max(1, slotCount);
            public Vector3 RowCenter => rowCenter;
            public Vector3 SlotSpacing => slotSpacing;
        }

        [Header("Physical scale (meters)")]
        [SerializeField] private float playmatWidth = 3.0f;
        [SerializeField] private float playmatLength = 2.0f;
        [SerializeField] private ZoneLayout[] zoneLayouts =
        {
            new ZoneLayout(),
        };

        public float PlaymatWidth => playmatWidth;
        public float PlaymatLength => playmatLength;

        public bool TryGetZonePosition(DuelSide side, PlaymatZoneType zoneType, int slotIndex, out Vector3 localPosition)
        {
            foreach (var zoneLayout in zoneLayouts)
            {
                if (zoneLayout == null || zoneLayout.ZoneType != zoneType)
                {
                    continue;
                }

                var clampedIndex = Mathf.Clamp(slotIndex, 0, zoneLayout.SlotCount - 1);
                var middleIndex = (zoneLayout.SlotCount - 1) * 0.5f;
                var indexOffset = clampedIndex - middleIndex;
                var sideMirror = side == DuelSide.Far ? 1f : -1f;

                localPosition = zoneLayout.RowCenter;
                localPosition.x += zoneLayout.SlotSpacing.x * indexOffset;
                localPosition.y += zoneLayout.SlotSpacing.y * indexOffset;
                localPosition.z = sideMirror * localPosition.z + zoneLayout.SlotSpacing.z * indexOffset;
                return true;
            }

            localPosition = Vector3.zero;
            return false;
        }
    }
}

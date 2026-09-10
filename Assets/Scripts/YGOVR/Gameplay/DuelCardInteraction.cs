using UnityEngine;

namespace YGOVR.Gameplay
{
    public class DuelCardInteraction : MonoBehaviour
    {
        [Header("Highlight")]
        [SerializeField] private Renderer targetRenderer;
        [SerializeField] private Color highlightColor = Color.cyan;
        [SerializeField] private float highlightIntensity = 1.5f;

        [Header("Zoom")]
        [SerializeField] private Transform zoomTarget;
        [SerializeField] private float zoomScaleMultiplier = 1.3f;

        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
        private Color originalEmissionColor;
        private Vector3 originalScale;
        private bool initialized;

        private void Awake()
        {
            if (zoomTarget == null)
            {
                zoomTarget = transform;
            }

            originalScale = zoomTarget.localScale;

            if (targetRenderer == null)
            {
                targetRenderer = GetComponentInChildren<Renderer>();
            }

            if (targetRenderer != null && targetRenderer.material.HasProperty(EmissionColor))
            {
                originalEmissionColor = targetRenderer.material.GetColor(EmissionColor);
                initialized = true;
            }
        }

        public void SetHovered(bool isHovered)
        {
            if (zoomTarget != null)
            {
                zoomTarget.localScale = isHovered ? originalScale * zoomScaleMultiplier : originalScale;
            }

            if (!initialized || targetRenderer == null)
            {
                return;
            }

            targetRenderer.material.EnableKeyword("_EMISSION");
            targetRenderer.material.SetColor(EmissionColor, isHovered ? highlightColor * highlightIntensity : originalEmissionColor);
        }
    }
}

using UnityEngine;

namespace YGOVR.Demo
{
    public static class PlayableDemoBootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void EnsureDemoController()
        {
            if (Object.FindObjectOfType<PlayableDemoController>() != null)
            {
                return;
            }

            var root = new GameObject("YGOVR Playable Demo");
            root.AddComponent<PlayableDemoController>();
        }
    }
}

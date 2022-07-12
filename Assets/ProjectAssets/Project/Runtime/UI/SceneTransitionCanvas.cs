using System.Collections;
using UnityEngine;

namespace ProjectAssets.Project.Runtime.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class SceneTransitionCanvas : MonoBehaviour
    {
        private static CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public static IEnumerator FadeOutCoroutine(float duration)
        {
            while (_canvasGroup.alpha < 1)
            {
                _canvasGroup.alpha += Time.deltaTime / duration;
                yield return null;
            }
        }

        public static IEnumerator FadeInCoroutine(float duration)
        {
            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= Time.deltaTime / duration;
                yield return null;
            }
        }
    }
}

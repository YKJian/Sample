using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Infrastructure
{
    public class Loading : MonoBehaviour
    {
        [SerializeField] private Image m_loading;

        private string _sceneName;
        private static Loading m_instance;

        private void Awake()
        {
            if (m_instance != null)
            {
                Destroy(m_instance.gameObject);
                m_instance = null;
            }

            m_instance = this;
            DontDestroyOnLoad(target: this);
            gameObject.SetActive(false);
        }

        public void LoadScene(string sceneName)
        {
            gameObject.SetActive(true);
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            m_loading.fillAmount = 0f;

            const int steps = 10;
            const float maxProgress = 0.5f;

            for (int i = 0; i < steps; i++)
            {
                yield return new WaitForSecondsRealtime(maxProgress);
                m_loading.fillAmount += maxProgress * 2 / steps;
            }
            
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            
            yield return operation;
            yield return new WaitForEndOfFrame();

            m_loading.fillAmount = 1f;
            gameObject.SetActive(false);
        }
    }
}
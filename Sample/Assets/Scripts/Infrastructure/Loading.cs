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
                if (m_instance.GetEntityId() != GetEntityId())
                {
                    Destroy(gameObject);
                }
                return;
            }

            m_instance = this;
            DontDestroyOnLoad(this);
            gameObject.SetActive(false);

            ServiceLocator.Register(this);
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

            //m_loading.fillAmount = maxProgress;

            var delta = 1 - m_loading.fillAmount;

            for (int i = 0; i < steps; i++)
            {
                yield return new WaitForSeconds(maxProgress);
                m_loading.fillAmount += delta / steps;
            }

            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            yield return operation;

            yield return new WaitForEndOfFrame();

            m_loading.fillAmount = 1f;
            gameObject.SetActive(false);
        }
    }
}
using UnityEngine;

namespace View
{
    public class BackgroundMusic : MonoBehaviour
    {
        private static BackgroundMusic m_instance;

        private void Awake()
        {
            if (m_instance)
            {
                if (m_instance == this)
                {
                    return;
                }

                Destroy(gameObject);
                return;
            }

            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}


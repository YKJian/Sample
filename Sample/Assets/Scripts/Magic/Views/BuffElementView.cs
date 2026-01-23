using Magic.Buffs;
using UnityEngine;
using UnityEngine.UI;

namespace Magic.Views
{
    public class BuffElementView : MonoBehaviour
    {
        [SerializeField] private Image m_timerImage;
        [SerializeField] private Image m_iconImage;

        private IBuff m_buff;

        public void Initialize(IBuff buff)
        {
            m_buff = buff;
            gameObject.SetActive(true);
            m_timerImage.fillAmount = 1;
            m_iconImage.sprite = buff.icon;
        }

        public void Deinitiallize()
        {
            m_buff = null;
            m_timerImage.fillAmount = 0;
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (m_buff is ITimedBuff timedBuff)
            {
                m_timerImage.fillAmount = timedBuff.timer / timedBuff.duration;
            }
        }
    }
}
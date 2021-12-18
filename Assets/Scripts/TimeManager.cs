using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
    public class TimeManager : MonoBehaviour
    {
        static TimeManager instance = default;
        [SerializeField]
        GameObject _timeEffect = default;
        bool _slow = false;
        float timer = 0;
        public static TimeManager Instance
        {
            get
            {
                if (instance == null)
                {
                    var obj = new GameObject("TimeManager");
                    var manager = obj.AddComponent<TimeManager>();
                    instance = manager;
                }
                return instance;
            }
        }
        private void Awake()
        {
            instance = this;
        }
        public void HitStop()
        {
            timer = 0.05f;
            if (!_slow)
            {
                _slow = true;
                if (_timeEffect != null)
                {
                    _timeEffect.SetActive(true);
                }
                StartCoroutine(SlowTime());
            }
        }
        IEnumerator SlowTime()
        {
            Time.timeScale = 0.1f;
            while (timer > 0)
            {
                timer -= Time.unscaledDeltaTime;
                yield return null;
            }
            Time.timeScale = 1f;
            _slow = false; 
            if (_timeEffect != null)
            {
                _timeEffect.SetActive(false);
            }
        }
    }
}

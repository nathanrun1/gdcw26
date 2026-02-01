using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game
{
    public class LevelTransitionScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Image _background;
        [SerializeField] private float _displayDuration = 1f;
        [SerializeField] private float _fadeDuration = 1f;

        private float _timer;
        private bool _fading;

        private void Start()
        {
            if (PlayerPrefs.GetInt("ShowLevelTransition", 0) == 1)
            {
                PlayerPrefs.SetInt("ShowLevelTransition", 0);
                if (_background == null) _background = GetComponent<Image>();
                Color bgColor = _background.color;
                bgColor.a = 0.8f;  
                _background.color = bgColor;
                string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
                _levelText.text = sceneName;
                _timer = _displayDuration;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0f && !_fading)
            {
                _fading = true;
                _timer = _fadeDuration;
            }

            if (_fading)
            {
                float alpha = Mathf.Clamp(_timer / _fadeDuration, 0f, 1f);
                
                Color bgColor = _background.color;
                bgColor.a = alpha * 0.8f;
                _background.color = bgColor;
                
                Color textColor = _levelText.color;
                textColor.a = alpha;
                _levelText.color = textColor;
                
                if (_timer <= 0)
                {
                    gameObject.SetActive(false);
                }
                
            }
        }
        
        public void SetLevelName(string levelName)
        {
            _levelText.text = levelName;
        }
    }
}
using TMPro;
using UnityEngine;

namespace Game
{
    public class BlinkingText : MonoBehaviour
    {
        [SerializeField] private float _blinkSpeed = 1f;
        private TextMeshProUGUI _text;
        private float _timer;
    
        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }
    
        private void Update()
        {
            _timer += Time.deltaTime * _blinkSpeed;
            Color color = _text.color;
            color.a = Mathf.PingPong(_timer, 1f);
            _text.color = color;
        }
    }
}
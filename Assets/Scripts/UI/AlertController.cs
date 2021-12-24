using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace UI {
    public class AlertController : MonoBehaviour {
        [SerializeField] private GameObject alertTitle;
        [SerializeField] private GameObject alertBody;
        [SerializeField] private GameObject alertAction;
        
        private TextMeshProUGUI _alertTitleTmp;
        private TextMeshProUGUI _alertBodyTmp;
        private TextMeshProUGUI _alertActionTmp;

        private bool _configured = false;

        private void OnEnable() {
            if (!_configured) {
                if (_alertActionTmp == null) _alertActionTmp = alertAction.GetComponent<TextMeshProUGUI>();
                if (_alertBodyTmp == null) _alertBodyTmp = alertBody.GetComponent<TextMeshProUGUI>();
                if (_alertTitleTmp == null) _alertTitleTmp = alertTitle.GetComponent<TextMeshProUGUI>();

                alertAction.GetComponent<Button>().onClick.AddListener(dismiss);
                
                _configured = true;
            }
        }

        private void dismiss() {
            gameObject.SetActive(false);
        }

        public void alert(string title, string body, string action) {
            _alertTitleTmp.text = title;
            _alertBodyTmp.text = body;
            _alertActionTmp.text = action;
            gameObject.SetActive(true);
        }
    }
}

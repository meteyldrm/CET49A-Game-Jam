using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            configure();
        }

        private void dismiss() {
            gameObject.SetActive(false);
            SceneManager.LoadScene("MainScene");
        }

        private void configure() {
            if (!_configured) {
                if (_alertActionTmp == null) _alertActionTmp = alertAction.GetComponent<TextMeshProUGUI>();
                if (_alertBodyTmp == null) _alertBodyTmp = alertBody.GetComponent<TextMeshProUGUI>();
                if (_alertTitleTmp == null) _alertTitleTmp = alertTitle.GetComponent<TextMeshProUGUI>();

                alertAction.GetComponentInParent<Button>().onClick.AddListener(dismiss);
                
                _configured = true;
            }
        }

        public void alert(string title, string body, string action) {
            gameObject.SetActive(true);
            configure();
            _alertTitleTmp.text = title;
            _alertBodyTmp.text = body;
            _alertActionTmp.text = action;
        }
    }
}

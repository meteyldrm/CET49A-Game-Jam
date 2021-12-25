using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class LightChoiceController : MonoBehaviour {

        [SerializeField] private GameObject car;
        [SerializeField] private GameObject brakeButton;
        [SerializeField] private GameObject gasButton;

        private CarController carController;

        private float buttonCooldown;

        private bool _configured = false;

        private void OnEnable() {
            if (!_configured) {
                carController = car.GetComponent<CarController>();
                
                brakeButton.GetComponent<Button>().onClick.AddListener(brake);
                gasButton.GetComponent<Button>().onClick.AddListener(gas);
                
                _configured = true;
            }

            buttonCooldown = 0f;
        }

        public void brake() {
            if (buttonCooldown == 0) {
                StartCoroutine(buttonCooldownCoroutine(0.5f));
                var correct = carController.activeObstacle.makeChoice(0);
                carController.setVelocity(0f);
                carController.madeChoice();
                if (!correct) {
                    carController.takeDamage();
                }
            }
        }

        public void gas() {
            if (buttonCooldown == 0) {
                StartCoroutine(buttonCooldownCoroutine(0.5f));
                var correct = carController.activeObstacle.makeChoice(1);
                carController.setVelocity(carController.carSpeed);
                carController.madeChoice();
                if (!correct) {
                    carController.takeDamage();
                }
            }
        }

        private IEnumerator buttonCooldownCoroutine(float time) {
            buttonCooldown = time;
            yield return new WaitForSeconds(time);
            buttonCooldown = 0;
        }
    }
}

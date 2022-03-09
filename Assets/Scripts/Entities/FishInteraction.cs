using UnityEngine;

namespace Entities
{
    public class FishInteraction : MonoBehaviour
    {
        private bool _onSelected;
        private HealthFish _healthFish;
        private Fish _fish;

        public float MaxTimeToRemove { get; private set; } = 2;
        public float CurrentTimeToRemove { get; private set; }


        public delegate void InteractionHandler(GameObject fishObject);
        public event InteractionHandler RemovedFromList;

        private void Awake()
        {
            _fish = GetComponent<Fish>();
            _healthFish = GetComponent<HealthFish>();
            CurrentTimeToRemove = 0;
        }

        private void SetCooldownToRemove()
        {
            if (CurrentTimeToRemove < MaxTimeToRemove && _onSelected)
            {
                CurrentTimeToRemove+= Time.deltaTime;
                if (CurrentTimeToRemove >= MaxTimeToRemove && _onSelected)
                {
                    CurrentTimeToRemove = MaxTimeToRemove;

                }
            }
            else if (CurrentTimeToRemove < MaxTimeToRemove && !_onSelected)
            {
                CurrentTimeToRemove = 0;
            }
        }

        private void OnMouseDown()
        {
            _onSelected = true;
        }

        private void OnMouseDrag()
        {
            if (_onSelected)
            {
                var sprite = GetComponent<SpriteRenderer>();
                sprite.sortingOrder = 5;
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                transform.Translate(mousePosition);
            }
        }

        private void OnMouseUp()
        {
            var sprite = GetComponent<SpriteRenderer>();
            sprite.sortingOrder = 1;
            _onSelected = false;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Ocean") && _onSelected && CurrentTimeToRemove <= 0)
            {
                InvokeRepeating("SetCooldownToRemove", 1f,0.1f);
            }

            if (collision.gameObject.CompareTag("Ocean") && _onSelected && CurrentTimeToRemove >= MaxTimeToRemove)
            {
                RemovedFromList?.Invoke(gameObject);
            }

            if (collision.gameObject.CompareTag("SnackNormalFood") && !_onSelected && _fish.CurrentHealthStatus != enums.HealthStatus.Sick)
            {
                _healthFish.CurrentHungry += 50;
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.CompareTag("SnackAverageFood") && !_onSelected && _fish.CurrentHealthStatus != enums.HealthStatus.Sick)
            {
                _healthFish.CurrentHungry += 250;
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.CompareTag("SnackSuperFood") && !_onSelected && _fish.CurrentHealthStatus != enums.HealthStatus.Sick)
            {
                _healthFish.CurrentHungry += 1000;
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.CompareTag("Remedy") && !_onSelected && _fish.CurrentHealthStatus == enums.HealthStatus.Sick)
            {
                _fish.CurrentHealthStatus = enums.HealthStatus.Hungry;
                _healthFish.CurrentHungry += _healthFish.MaxHungry * 0.5f;
                Destroy(collision.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Ocean") && _onSelected && CurrentTimeToRemove > 0)
            {
                CancelInvoke("SetCooldownToRemove");
                CurrentTimeToRemove = 0;
            }

        }
    }
}
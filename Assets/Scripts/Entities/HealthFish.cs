using UnityEngine;

namespace Entities
{
    public class HealthFish : MonoBehaviour
    {
        private Fish _fish;

        public float MaxHungry { get; internal set; }
        public float CurrentHungry { get; internal set; }

        private void Start()
        {
            _fish = GetComponent<Fish>();
            MaxHungry = _fish.HungryAmount;
            CurrentHungry = MaxHungry;
            InvokeRepeating("CooldownHungry", 1f, 1f);
        }

        private void Update()
        {
            if (CurrentHungry >= MaxHungry)
            {
                CurrentHungry = MaxHungry;
            }
        }

        private void CooldownHungry()
        {
            var hungryMoment = MaxHungry * 0.5f;
            var sickMoment = MaxHungry * 0.2f;

            if (CurrentHungry > 0)
            {
                CurrentHungry--;
            }
            else
            {
                CurrentHungry = 0;
            }

            if (CurrentHungry > hungryMoment)
            {
                _fish.CurrentHealthStatus = enums.HealthStatus.Healthful;
            }
            else if (CurrentHungry > sickMoment && CurrentHungry <= hungryMoment)
            {
                _fish.CurrentHealthStatus = enums.HealthStatus.Hungry;
            }
            else if(CurrentHungry <= sickMoment)
            {
                _fish.CurrentHealthStatus = enums.HealthStatus.Sick;
            }

            //if (CurrentHungry <= hungryMoment)
            //{
            //    _fish.CurrentHealthStatus = enums.HealthStatus.Hungry;
            //}
            //else if (CurrentHungry <= sickMoment)
            //{
            //    _fish.CurrentHealthStatus = enums.HealthStatus.Sick;
            //}
            //else
            //{
            //    _fish.CurrentHealthStatus = enums.HealthStatus.Healthful;
            //}

        }


    }
}
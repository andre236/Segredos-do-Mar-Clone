using UnityEngine;
using Entities.enums;

namespace Entities
{
    public class StatusGenerate : MonoBehaviour
    {
        private Fish _fish;

        public delegate void GeneratedStatusHandler(int amount);
        public event GeneratedStatusHandler GeneratedGold;
        public event GeneratedStatusHandler GeneratedExperience;

        void Awake()
        {
            _fish = GetComponent<Fish>();
            InvokeRepeating("GenerateStatus", _fish.DelayGold, _fish.DelayGold);
        }


        private void GenerateStatus()
        {
            // If not in Old Lifestage and Not Sick
            if (_fish.CurrentLifeStage != LifeStage.Old && _fish.CurrentHealthStatus == HealthStatus.Healthful)
            {
                GeneratedGold(_fish.GoldRevenue);
                GeneratedExperience(_fish.ExperienceRevenue);
            }
            // If only Sick
            else if (_fish.CurrentHealthStatus == HealthStatus.Sick && _fish.CurrentLifeStage != LifeStage.Old)
            {
                GeneratedGold(_fish.GoldRevenue / 4);
                GeneratedExperience(0);
            }
            // If only Hungry
            else if (_fish.CurrentLifeStage != LifeStage.Old && _fish.CurrentHealthStatus == HealthStatus.Hungry )
            {
                GeneratedGold(_fish.GoldRevenue / 2);
                GeneratedExperience(_fish.ExperienceRevenue);
            }
        }


    }
}
using System.Collections;
using UnityEngine;

namespace Entities
{
    public class LifeSpan : MonoBehaviour
    {
        [SerializeField]
        private Fish _fish;
        public float CurrentLifeTime { get; private set; } = 0;
        public float MaxLifeTime { get; private set; }


        void Start()
        {
            _fish = GetComponent<Fish>();
            MaxLifeTime = (int)_fish.MaxLifeTime;
            //StartCoroutine(Age());

            InvokeRepeating("CountdownLifeTime", 1f, 1f);
            //StartCoroutine(CountdownLifeTime());
        }

        private void CountdownLifeTime()
        {
            if (CurrentLifeTime < MaxLifeTime)
                CurrentLifeTime+= 90;
            else
                CurrentLifeTime = MaxLifeTime;

            Age();
        }

        private void Age()
        {
            float yuongStage = Mathf.Abs(MaxLifeTime / 2);
            float adultStage = MaxLifeTime * 0.75f;
            float oldStage = MaxLifeTime;

            if (CurrentLifeTime >= yuongStage && CurrentLifeTime < adultStage)
                _fish.CurrentLifeStage = enums.LifeStage.Young;
            else if (CurrentLifeTime >= adultStage && CurrentLifeTime < oldStage)
                _fish.CurrentLifeStage = enums.LifeStage.Adult;
            else if (CurrentLifeTime >= oldStage)
                _fish.CurrentLifeStage = enums.LifeStage.Old;
        }
    }
}
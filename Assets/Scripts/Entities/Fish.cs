using UnityEngine;
using Entities.enums;

namespace Entities
{
    public class Fish : MonoBehaviour
    {
        [SerializeField]
        private FishObject _fishSettings;

        public string NameFish { get; private set; }
        public float MaxLifeTime { get; private set; }
        public float DelayGold { get; private set; }
        public float HungryAmount { get; private set; }

        public int PriceFish { get; private set; }
        public int GoldRevenue { get; private set; }
        public int ExperienceRevenue { get; private set; }
        public int RequiredLevel { get; private set; }

        public LifeStage CurrentLifeStage { get; protected internal set; }
        public HealthStatus CurrentHealthStatus { get; protected internal set; }

        public SpriteRenderer SpriteFish { get; private set; }
        public Animator AnimatorFish { get; private set; }

        void Awake()
        {
            SpriteFish = GetComponent<SpriteRenderer>();
            AnimatorFish = GetComponent<Animator>();
            NameFish = _fishSettings.NameFish;
            MaxLifeTime = _fishSettings.MaxLifeTime;
            DelayGold = _fishSettings.DelayGold;
            HungryAmount = _fishSettings.HungryAmount;
            PriceFish = _fishSettings.PriceFish;
            GoldRevenue = _fishSettings.GoldRevenue;
            ExperienceRevenue = _fishSettings.ExperienceRevenue;
            RequiredLevel = _fishSettings.RequiredLevel;
            SpriteFish.sprite = _fishSettings.SpriteFish;
            AnimatorFish.runtimeAnimatorController = _fishSettings.ControllerAnimeSprite;
        }


    }
}
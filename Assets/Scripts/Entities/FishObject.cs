using System.Collections;
using UnityEngine;
using Entities.enums;
using System;

namespace Entities
{
    [CreateAssetMenu(fileName ="Fish", menuName ="Create Fish")]
    public class FishObject : ScriptableObject
    {
        [field:SerializeField]
        public string NameFish { get; private set; }
        [Header("Por Segundo.")]
        [Range(900, 10000)]
        public float MaxLifeTime;
        [Range(15, 60)]
        public float DelayGold;
        [Range(15, 15000)]
        public int GoldRevenue;
        [Range(100, 10000)]
        public float HungryAmount;

        [Range(250, 10000)]
        public int PriceFish;
        public int ExperienceRevenue;
        [Range(0, 10)]
        public int RequiredLevel;

        public LifeStage CurrentLifeStage;
        public HealthStatus CurrentHealthStatus;

        public Sprite SpriteFish;
        public RuntimeAnimatorController ControllerAnimeSprite, ControllerAnimImage;
        public GameObject PrefabFish;






    }
}
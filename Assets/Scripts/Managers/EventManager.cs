using System.Collections.Generic;
using UnityEngine;
using Objects;
using Entities;

namespace Manager
{
    public class EventManager : MonoBehaviour
    {
        private UIManager _uiManager;
        private DataPlayerManager _dataPlayerManager;
        private ItemsManager _itemsManager;
        private GoldChest _goldChest;
        private AchievementManager _achievementManager;

        public List<Fish> FishList = new List<Fish>();

        private void Awake()
        {
            // -- Reference -- //
            _uiManager = FindObjectOfType<UIManager>();
            _dataPlayerManager = FindObjectOfType<DataPlayerManager>();
            _goldChest = FindObjectOfType<GoldChest>();
            _itemsManager = FindObjectOfType<ItemsManager>();
            _achievementManager = FindObjectOfType<AchievementManager>();
            FishList.AddRange(FindObjectsOfType<Fish>());

            // -- Adding and removing Fishes on Aquarium -- //
            foreach (Fish f in FishList)
            {
                if (f != null)
                {
                    f.GetComponent<StatusGenerate>().GeneratedGold += _goldChest.OnGeneratedGold;
                    f.GetComponent<StatusGenerate>().GeneratedExperience += _dataPlayerManager.OnGeneratedExperience;
                    f.GetComponent<FishInteraction>().RemovedFromList += OnRemovedFromFishList;
                }
            }

            // -- Data Player -- //
            _dataPlayerManager.PlayerGoldEarned += _uiManager.OnPlayerGoldEarned;
            _goldChest.GoldCollected += _dataPlayerManager.OnCollectGold;

            // -- Selecting Foods -- //
            _itemsManager.NormalFoodSelected += _uiManager.OnNormalFoodSelected;
            _itemsManager.AverageFoodSelected += _uiManager.OnAverageFoodSelected;
            _itemsManager.SuperFoodSelected += _uiManager.OnSuperFoodSelected;
            _itemsManager.UnselectedAllItems += _uiManager.OnUnselectedAllItems;

            // -- Using Foods -- //
            _itemsManager.UsedNormalFood += _dataPlayerManager.OnUsedNormalFood;
            _itemsManager.UsedNormalFood += _uiManager.OnUsedNormalFood;

            _itemsManager.UsedAverageFood += _dataPlayerManager.OnUsedAverageFood;
            _itemsManager.UsedAverageFood += _uiManager.OnUsedAverageFood;

            _itemsManager.UsedSuperFood += _dataPlayerManager.OnUsedSuperFood;
            _itemsManager.UsedSuperFood += _uiManager.OnUsedSuperFood;

            // -- Using Remedy -- //
            _itemsManager.RemedySelected += _uiManager.OnRemedySelected;

            _itemsManager.UsedRemedy += _dataPlayerManager.OnUsedRemedy;
            _itemsManager.UsedRemedy += _uiManager.OnUsedRemedy;

            // -- Showing UI data player -- //
            _uiManager.OnFishesAquarium(FishList.Count.ToString(), _dataPlayerManager.MaxSizeAquarium.ToString());
            _uiManager.OnEarnedExperience(_dataPlayerManager.CurrentExperience, _dataPlayerManager.ExperienceNextLevelUp, _dataPlayerManager.CurrentLevel);

            // -- Chest -- //
            _dataPlayerManager.StartedMaxGoldChest += _goldChest.OnStartedChestGold;
            _goldChest.ShowCurrentChestGold += _uiManager.OnShowCurrentChestGold;

        }



        private void Update()
        {
            _uiManager.CheckActivedFoods();
            _uiManager.CheckActivedRemedy();
            _uiManager.OnPlayerGoldEarned(_dataPlayerManager.BankGold);
            _uiManager.OnEarnedExperience(_dataPlayerManager.CurrentExperience, _dataPlayerManager.ExperienceNextLevelUp, _dataPlayerManager.CurrentLevel);
        }

        internal void OnBoughtAquariumExpansion()
        {
            _uiManager.OnFishesAquarium(FishList.Count.ToString(), _dataPlayerManager.MaxSizeAquarium.ToString());
        }


        // ----- Fish List ----- //

        private void OnRemovedFromFishList(GameObject fishObject)
        {
            FishList.Remove(fishObject.GetComponent<Fish>());
            Destroy(fishObject);
            _uiManager.OnFishesAquarium(FishList.Count.ToString(), _dataPlayerManager.MaxSizeAquarium.ToString());
            
            Debug.Log("Removido");
        }

        internal void OnAddedFishList()
        {
            FishList.Clear();
            FishList.AddRange(FindObjectsOfType<Fish>());

            // Adicionando na Lista e Removendo.
            foreach (Fish f in FishList)
            {
                if (f != null)
                {
                    f.GetComponent<StatusGenerate>().GeneratedGold -= _goldChest.OnGeneratedGold;
                    f.GetComponent<StatusGenerate>().GeneratedExperience -= _dataPlayerManager.OnGeneratedExperience;
                    f.GetComponent<FishInteraction>().RemovedFromList -= OnRemovedFromFishList;
                    f.GetComponent<FishInteraction>().RemovedFromList -= _achievementManager.OnRemovedList_BreedAchievement;

                    f.GetComponent<StatusGenerate>().GeneratedGold += _goldChest.OnGeneratedGold;
                    f.GetComponent<StatusGenerate>().GeneratedExperience += _dataPlayerManager.OnGeneratedExperience;
                    f.GetComponent<FishInteraction>().RemovedFromList += OnRemovedFromFishList;
                    f.GetComponent<FishInteraction>().RemovedFromList += _achievementManager.OnRemovedList_BreedAchievement;
                }
            }
            _uiManager.OnFishesAquarium(FishList.Count.ToString(), _dataPlayerManager.MaxSizeAquarium.ToString());
        }



    }
}
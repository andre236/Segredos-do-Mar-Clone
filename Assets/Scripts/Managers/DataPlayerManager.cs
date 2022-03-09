using System;
using UnityEngine;


namespace Manager
{
    public class DataPlayerManager : MonoBehaviour
    {
        // -- Remedies -- //
        [SerializeField]
        private GameObject _dropRemedyPrefab;
        public int NormalRemedy { get; private set; } = 0;

        // -- Food -- //
        [SerializeField]
        private GameObject _normalFoodPrefab;
        [SerializeField]
        private GameObject _averageFoodPrefab;
        [SerializeField]
        private GameObject _superFoodPrefab;

        public int NormalFood { get; private set; } = 0;
        public int AverageFood { get; private set; } = 0;
        public int SuperFood { get; private set; } = 0;

        //public List<Food> PlayerListFood = new List<Food>();

        // -- Status -- //
        public int CurrentLevel { get; private set; }
        public int CurrentExperience { get; private set; }
        public int ExperienceNextLevelUp { get; private set; }
        public int BankGold { get; private set; }

        // -- Chest -- //
        public int MaxGoldChest { get; private set; }
        public int NumberOfUpgradesChest { get; private set; } = 0;

        [field:SerializeField]
        public int[] CurrentMaxGoldUpgrade { get; private set; }

        // -- Aquarium -- //
        [field:SerializeField]
        public int MaxSizeAquarium { get; private set; } = 3;
        public int NumberOfUpgradesAquarium { get; private set; } 

        public delegate void ShopProductHandler(int amount);

        public Action<int> PlayerGoldEarned;
        
        public Action<int> StartedMaxGoldChest;

        public Action<int> UpdateMaxGoldChest;

        private void Start()
        {
            CurrentLevel = 1;
            CurrentExperience = 0;
            ExperienceNextLevelUp = 100;
            BankGold = 5000;




        }

        // -- Data -- //

        private void LoadPlayerData()
        {

            // -- Foods -- //
            NormalFood = PlayerPrefs.GetInt(NormalFood.ToString(), 0);
            AverageFood = PlayerPrefs.GetInt(AverageFood.ToString(), 0);
            SuperFood = PlayerPrefs.GetInt(SuperFood.ToString(), 0);


            // -- Stats -- //
            CurrentExperience = PlayerPrefs.GetInt(CurrentExperience.ToString(), 0);
            CurrentLevel = PlayerPrefs.GetInt(CurrentLevel.ToString(), 1);
            ExperienceNextLevelUp = PlayerPrefs.GetInt(ExperienceNextLevelUp.ToString(), 100);
            BankGold = PlayerPrefs.GetInt(BankGold.ToString(), 500);

            // -- Chest -- //
            NumberOfUpgradesChest = PlayerPrefs.GetInt(NumberOfUpgradesChest.ToString(), 0);
            MaxGoldChest = CurrentMaxGoldUpgrade[NumberOfUpgradesChest];

            // -- Aquarium -- //
            MaxSizeAquarium = PlayerPrefs.GetInt(MaxSizeAquarium.ToString(), 3);
            NumberOfUpgradesAquarium = PlayerPrefs.GetInt(NumberOfUpgradesAquarium.ToString(), 0);


        }


        // -- Experience and Level -- //
        private void LevelUp()
        {
            if (CurrentExperience >= ExperienceNextLevelUp)
            {
                CurrentExperience -= ExperienceNextLevelUp;
                if (CurrentLevel <= 0)
                {
                    CurrentLevel = 0;
                }
                CurrentLevel++;
                ExperienceNextLevelUp += 50 + 100 * CurrentLevel;
            }
        }

        // -- About Gold -- //

        public void OnSpentGold(int amount)
        {
            BankGold -= amount;
            PlayerGoldEarned?.Invoke(BankGold);
            
        }

        public void OnCollectGold(int amount)
        {
            BankGold += amount;
            PlayerGoldEarned?.Invoke(BankGold);
        }

        // -- Experience -- //
        public void OnGeneratedExperience(int amount)
        {
            CurrentExperience += amount;
            LevelUp();
        }

        // -- Food -- //

        //public void OnBoughtFood(Food food)
        //{
        //    var playerFoodListQuantity = PlayerListFood.Where(f => f.Name == food.Name).Sum(f => f.Quantity);

        //    if (!PlayerListFood.Contains(food))
        //    {
        //        PlayerListFood.Add(new Food(food.Name, food.Description, food.Price, food.Quantity));
        //        Debug.Log("Não havia este item, então foi adicionado.");
        //    }
        //    else
        //    {
        //        playerFoodListQuantity += food.Quantity;
        //        Debug.Log(playerFoodListQuantity);
        //    }
        //}

        public void OnBoughtItem(string itemName, int amount)
        {
            switch (itemName)
            {
                case "NormalFood":
                    NormalFood += amount;
                    break;
                case "AverageFood":
                    AverageFood += amount;
                    break;
                case "SuperFood":
                    SuperFood += amount;
                    break;
                case "NormalRemedy":
                    NormalRemedy += amount;
                    break;

            }
        }

        public void OnUsedFood(Food food)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            if (food.Quantity > 0)
            {
                food.Quantity--;
                Instantiate(_normalFoodPrefab, new Vector3(mousePosition.x, mousePosition.y, 0f), Quaternion.identity);

            }
            else
            {
                Debug.Log("Normal Food Empty: " + NormalFood.ToString());
            }
        }

        public void OnUsedNormalFood()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            if (NormalFood > 0)
            {
                NormalFood--;
                Instantiate(_normalFoodPrefab, new Vector3(mousePosition.x, mousePosition.y, 0f), Quaternion.identity);

            }
            else
            {
                Debug.Log("Normal Food Empty: " + NormalFood.ToString());
            }
        }

        public void OnUsedAverageFood()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            if (AverageFood > 0)
            {
                AverageFood--;
                Instantiate(_averageFoodPrefab, new Vector3(mousePosition.x, mousePosition.y, 0f), Quaternion.identity);

            }
            else
            {
                Debug.Log("Average Food Empty: " + AverageFood.ToString());
            }
        }

        public void OnUsedSuperFood()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            if (SuperFood > 0)
            {
                SuperFood--;
                Instantiate(_superFoodPrefab, new Vector3(mousePosition.x, mousePosition.y, 0f), Quaternion.identity);

            }
            else
            {
                Debug.Log("Super Food Empty: " + SuperFood.ToString());
            }
        }

        // --  Remedy -- //

        public void OnUsedRemedy()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            if (NormalRemedy > 0)
            {
                NormalRemedy--;
                Instantiate(_dropRemedyPrefab, new Vector3(mousePosition.x, mousePosition.y, 0f), Quaternion.identity);
            }
            else
            {
                Debug.Log("Remedy Empty: " + SuperFood.ToString());
            }
        }

        // -- Aquarium -- //
        public void OnBoughtAquariumExpansion()
        {
            NumberOfUpgradesAquarium++;
            MaxSizeAquarium += 3;
        }

        // -- Chest -- //

        public int OnBoughtChestExpansion(int newMaxGold)
        {
            NumberOfUpgradesChest++;
            return CurrentMaxGoldUpgrade[NumberOfUpgradesChest];
        }

        public int OnUpdateMaxGoldChest(int maxGold)
        {
            return MaxGoldChest = maxGold;
        }



    }
}
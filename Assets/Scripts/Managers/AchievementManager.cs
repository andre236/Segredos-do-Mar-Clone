using System;
using UnityEngine;
using Entities;

namespace Manager
{
    public class AchievementManager : MonoBehaviour
    {

        private void Start()
        {
            PlayerPrefs.DeleteAll();
        }

        public void OnRemovedList_BreedAchievement(GameObject fish)
        {
            string fishName = fish.GetComponent<Fish>().NameFish;
            bool fishLifeStageOld = fish.GetComponent<Fish>().CurrentLifeStage == Entities.enums.LifeStage.Old;
            /*
             The achievements are:

            Breed 5 Betta, Angel, Butterfly, Cardinal, Goby, Sea Horse, Shrimp, Dolphin, Tang, Moorish Idol

            */

            if (PlayerPrefs.HasKey("BREED_" + fishName) && fishLifeStageOld)
                PlayerPrefs.SetInt("BREED_" + fishName, PlayerPrefs.GetInt("BREED_" + fishName) + 1);
            else if (!PlayerPrefs.HasKey("BREED_" + fishName) && fishLifeStageOld)
                PlayerPrefs.SetInt("BREED_" + fishName, 1);

            if (PlayerPrefs.GetInt("BREED_" + fishName) >= 5)
                Debug.Log("Conquista liberada! Voc� criou " + fish + " mais de 5 vezes!");


        }

        public void OnBoughtFish(GameObject fish)
        {
            string fishName = fish.GetComponent<Fish>().NameFish;

            if (PlayerPrefs.HasKey("BOUGHT_" + fishName))
                PlayerPrefs.SetInt("BOUGHT_" + fishName, PlayerPrefs.GetInt("BOUGHT_" + fishName) + 1);
            else
                PlayerPrefs.SetInt("BOUGHT_" + fishName, 1);
        }

    }
}
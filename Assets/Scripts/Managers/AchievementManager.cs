using System;
using UnityEngine;
using Entities;

namespace Manager
{
    public class AchievementManager : MonoBehaviour
    {
        public Action<string> AchievementReached { get; internal set; }

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
            else
                PlayerPrefs.SetInt("BREED_" + fishName, 1);

            if (PlayerPrefs.GetInt("BREED_" + fishName) >= 5)
                Debug.Log("Conquista liberada! Você criou " + fish + " mais de 5 vezes!");


        }

    }
}
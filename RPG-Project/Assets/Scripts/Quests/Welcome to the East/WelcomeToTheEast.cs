using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WelcomeToTheEast  : QuestBase
{
    private bool isCompleted;

   public  WelcomeToTheEast()
    {
        this.QuestName = "Welcome to Gensokyo";
        this.QuestSteps = 3;
        this.currentStep = 0;
        this.QuestDescriptions.Add(" I should just keep walking for now.");
        this.QuestDescriptions.Add("I need to check my phone, maybe I can contact someone.");
        this.QuestDescriptions.Add("I'll just rest for now...");

    }

    public override void checkCompletion()
    {
        if (currentStep >= QuestSteps)
        {
            //if the unique main quest system is used, maybe we can use this funcion to add the next quest and set some flags
            isCompleted = true;
            PlayerData.SetCurrentPlayerMainQuest(null);

        }
    }

        
    

    public override void Update()
    {
        //check here if quests really need updating if it is a generic quest that will be called everytime
        currentStep++;
        checkCompletion();
    }





}

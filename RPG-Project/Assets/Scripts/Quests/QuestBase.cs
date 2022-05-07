using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Wont be scriptable objects this, as each main quest will have its own script for handling the updates and progression, maybe on secondary it would be interesting
//Note that, the main quest will be handled individualy, as well as key items; -> look at submenus for key itens later with and editor script
public class QuestBase
{
    [Range(1, 3)] //Max of quest steps for completion, no more than this or it wont fit the planned UI
    protected int QuestSteps;
    protected string QuestName;
    protected int currentStep;
    protected List<string> QuestDescriptions = new List<string>();


    public QuestBase()
    {
        QuestName = "WARNING, NO QUEST CREATED";
    }
    public virtual void checkCompletion()
    {
        Debug.Log("Not Overwritten -  Completion");
    }
    public virtual void Update()
    {
        Debug.Log("Not Overwritten - Update");
    }


    //needs get and setters 

    public int getCurrentStep()
    {
        return currentStep;
    }

    public string getQuestName() { return QuestName; }

    public List<string> getQuestDescriptionList() {return QuestDescriptions;}








}

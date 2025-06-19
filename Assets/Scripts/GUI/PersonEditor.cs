using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using WorldEcon.Entities;
using WorldEcon.Actions;

[CustomEditor(typeof(PersonVisual))]
[CanEditMultipleObjects]
public class PersonEditor : Editor 
{
    void OnEnable()
    {

    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();
        PersonVisual person = (PersonVisual) target;
        GUILayout.Label("Name: " + person.name);
        GUILayout.Label("Current Action: " + person.gameObject.GetComponent<Person>().currentAction);
        GUILayout.Label("Actions: ");
        foreach (AbstractAction action in person.gameObject.GetComponent<Person>().actions)
        {
            string pre = "";
            string eff = "";

            foreach (KeyValuePair<string, int> p in action.preconditions)
                pre += p.Key + ", ";
            foreach (KeyValuePair<string, int> e in action.effects)
                eff += e.Key + ", ";

            GUILayout.Label("====  " + action.actionName + "(" + pre + ")(" + eff + ")");
        }
        GUILayout.Label("Goals: ");
        foreach (KeyValuePair<SubGoal, int> goal in person.gameObject.GetComponent<Person>().goals)
        {
            GUILayout.Label("---: ");
            foreach (KeyValuePair<string, int> subGoal in goal.Key.subGoals)
                GUILayout.Label("=====  " + subGoal.Key);
        }
        GUILayout.Label("Beliefs: ");
        foreach (KeyValuePair<string, int> subGoal in person.gameObject.GetComponent<Person>().beliefs.GetWorldStates())
        {
                GUILayout.Label("=====  " + subGoal.Key);
        }

        GUILayout.Label("Inventory: ");
        foreach (GameObject item in person.gameObject.GetComponent<Person>().inventory.GetItems())
        {
            GUILayout.Label("====  " + item.tag);
        }


        serializedObject.ApplyModifiedProperties();
    }
}
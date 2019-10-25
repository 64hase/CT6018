using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StackManager : MonoBehaviour
{
    public GameObject stackBase;
    public Stack stackster;
    int stackPos = 0;

    //Debug - remove this later
    private int spawnedElements = 0;

    /*[SerializeField]
    private StackElement elementToInstantiate;*/

    [SerializeField]
    private RectTransform content;

        //(RectTransform)gameObject.transform;

        //

    private void Awake()
    {
        stackster = new Stack();
        stackster.Push(stackBase);
    }
    // Start is called before the first frame update
    private void Start()
    {


        /*
        for (int i = stackster.Count; i < 4; i++)
        {
            SpawnElement();
        }*/
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void PushButton()
    {
        SpawnElement();
        Debug.Log("I pushed an item on the stack");
    }

    public void PopButton()
    {
        GameObject temp;
        temp = (GameObject)stackster.Pop();
        GameObject.Destroy(temp);

    }

    private void SpawnElement()
    {
        if (spawnedElements > 15)
        {
            Debug.Log("Stack limit has been reached!");
            return;
        }
        else
        {
            spawnedElements++;
            GameObject instantiatedStackElement = Instantiate(stackBase, content);
            instantiatedStackElement.name = "SpawnedElement_" + spawnedElements;
            ((RectTransform)instantiatedStackElement.transform).SetAsFirstSibling();
            stackster.Push(instantiatedStackElement);
        }
    }

    //Task element
    //Handle input field clicking etc
    //Handle button clicks
    //Add task element script to task element prefab

    //Task manager class
    //PlayerPrefs.SetFloat();
    //PlayerPrefs.SetString("stringYouWantToSet", "string");
    //PlayerPrefs.GetString("stringYouWantToReceive");
    //Retrieve tasks
    //PlayerPrefs.GetString("task_1");
    //PlayerPrefs.GetInt(amountOfTasks);
    //for (int i = 0; i < amountofTasks; i++)
    //{
    //  PlayerPrefs.GetString("task_" + i);
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class StackManager : MonoBehaviour
{
    [SerializeField] private GameObject stackBase;
    [SerializeField] private Stack stackster;
    [SerializeField] private Image BottomShadow;
    [SerializeField] private Button TaskButton;
    private int stackPos = 0;

    //Debug - remove this later
    public int spawnedElements = 0;

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
        //TaskButton.onClick.AddListener(ShowShadow);
        ShowShadow();
        for (int i = spawnedElements; i < 2; i++)
        {
            SpawnElement();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void PushButton()
    {
        SpawnElement();
        Debug.Log("I pushed an item on the stack");
        ShowShadow();
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
    public void ShowShadow()
    {
        if (spawnedElements > 3)
        {
            BottomShadow.color = new Vector4(1, 1, 1, 0.5F);
        }
        else
        {
            BottomShadow.color = new Vector4(1, 1, 1, 0);
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

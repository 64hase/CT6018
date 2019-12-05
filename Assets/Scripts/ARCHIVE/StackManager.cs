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
    [SerializeField] private RectTransform content;
    private int stackPos = 0;
    public int spawnedElements = 0;


    private void Awake()
    {
        stackster = new Stack();
        stackster.Push(stackBase);
    }
    // Start is called before the first frame update
    private void Start()
    {
        ShowShadow();
        for (int i = spawnedElements; i < 2; i++)
        {
            SpawnElement();
        }
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
}

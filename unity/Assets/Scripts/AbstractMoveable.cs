using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AbstractMoveable : MonoBehaviour, IPointerClickHandler
{
    [Header("Settings")]
    [SerializeField] protected float snapRadius = 70f;
    [SerializeField] protected bool inFrontofOther = false;

    [Header("Control")]
    public bool isActive;
    public bool isMoveing;

    protected Transform mainPanel;
    protected ComponentReference componentReference;

    protected Button button;

    protected float offsetX;
    protected float offsetY;

    protected Color mainColor;

    protected void Start()
    {
        mainColor = GetComponent<Image>().color;
        if (transform.parent != mainPanel) Init();
    }

    protected void Init()
    {
        componentReference = Camera.main.GetComponent<ComponentReference>();
        mainPanel = componentReference._mainPanel.transform;

        button = GetComponent<Button>();
        button.onClick.AddListener(() => 
        {
            if (componentReference.objectMoving == null || componentReference.objectMoving == gameObject)
            {
                if(Input.GetMouseButtonDown(1))
                {
                    Debug.Log("LEFT");
                }
                else if (!isActive)
                {
                    SetMoveComponent(false);
                    GetComponent<Image>().color = mainColor;
                    if (transform.parent == mainPanel)
                    {
                        isActive = true;
                        button.interactable = true;
                    }
                    CreateNewMoveableObject();
                }
                else
                {
                    isActive = false;
                    SetMoveComponent(true);
                }
            }
        });

        isActive = false;
        SetMoveComponent(false);
    }

    private void CreateNewMoveableObject()
    {
        GameObject component = Instantiate(gameObject, mainPanel);
        int index = component.name.IndexOf("(Clone)");
        component.name = (index < 0) ? component.name : component.name.Remove(index, "(Clone)".Length);
        component.name = component.name.Replace(" ", "");
        component.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        if(inFrontofOther)
            component.transform.SetAsLastSibling();
        else
            component.transform.SetAsFirstSibling();

        AbstractMoveable moveComponent = component.GetComponent<AbstractMoveable>();
        moveComponent.Init();
        moveComponent.SetMoveComponent(true);
    }

    public void SetMoveComponent(bool value)
    {
        isMoveing = value;
        componentReference.objectMoving = value ? gameObject : null;
        button.interactable = !value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (componentReference.curComponentMenu != null)
                DestroyImmediate(componentReference.curComponentMenu);
        
            OpenComponentMenu();
        }
    }

    protected virtual void OpenComponentMenu() => throw new NotImplementedException();

    protected void Update()
    {
        if (isMoveing)
        {
            CalcOffsetToClosestConnectionPoint();
            transform.position = Input.mousePosition + new Vector3(offsetX, offsetY);

            if (Input.GetMouseButtonDown(0)) // 0 = left mouse button
            {
                button.onClick.Invoke();
            }

            if (Input.GetMouseButtonDown(1)) // 1 = right mouse button
            {
                isMoveing = false;
                DestroyImmediate(gameObject);
            }

            if (Input.mouseScrollDelta.y != 0)
            {
                transform.Rotate(Vector3.forward, Input.mouseScrollDelta.y > 0 ? 90f : -90f);
            }
        }
    }

    protected virtual void CalcOffsetToClosestConnectionPoint() => throw new NotImplementedException();
}

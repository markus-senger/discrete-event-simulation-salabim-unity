using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentReference : MonoBehaviour
{
    [SerializeField] GameObject sceneHandler;
    [SerializeField] GameObject mainPanel;

    public GameObject _sceneHandler { get; private set; }
    public GameObject _mainPanel { get; private set; }

    public GameObject objectMoving { get; set; }

    public GameObject curComponentMenu { get; set; }

    private void Awake()
    {
        _sceneHandler = sceneHandler;
        _mainPanel = mainPanel;
        objectMoving = null;
        curComponentMenu = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteAll : MonoBehaviour
{
    [SerializeField] private Transform mainPanel;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            foreach(Transform entry in mainPanel)
            {
                Destroy(entry.gameObject);
            }
        });
    }
}

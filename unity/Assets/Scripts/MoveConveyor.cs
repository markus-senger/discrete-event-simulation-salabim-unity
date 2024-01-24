using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MoveConveyor : AbstractMoveable
{
    protected override void CalcOffsetToClosestConnectionPoint()
    {
        
        float minDistance = float.MaxValue;
        GameObject nearestConnectionPoint = null;
        foreach(Transform activeComponent in mainPanel.transform)
        {
            if(activeComponent.tag == "Conveyor" && activeComponent.GetComponent<AbstractMoveable>().isActive && activeComponent.gameObject != gameObject)
            {
                for(int i = 0; i < activeComponent.childCount; i++)
                {
                    GameObject connectionPoint = activeComponent.GetChild(i).gameObject;
                    GetComponent<Image>().color = Color.white;
                    float curDistance = Vector3.Distance(Input.mousePosition, connectionPoint.transform.position);
                    if(curDistance < minDistance)
                    {
                        minDistance = curDistance;
                        nearestConnectionPoint = connectionPoint;
                    }
                }
            }
        }

        if(minDistance < snapRadius)
        {
            GetComponent<Image>().color = Color.green;
            float distanceY = nearestConnectionPoint.transform.position.y - Input.mousePosition.y;
            float distanceX = nearestConnectionPoint.transform.position.x - Input.mousePosition.x;
            float tmpX = nearestConnectionPoint.transform.position.x + ((GetComponent<RectTransform>().rect.width / 2) - (nearestConnectionPoint.GetComponent<RectTransform>().rect.width / 2)) * transform.localScale.x
                * (distanceX > 0 ? -1 : 1) - Input.mousePosition.x; 
            float tmpY = distanceY;

             float tmpX2 = distanceX;
             float tmpY2 = nearestConnectionPoint.transform.position.y + ((GetComponent<RectTransform>().rect.width / 2) - (nearestConnectionPoint.GetComponent<RectTransform>().rect.height / 2)) * transform.localScale.y
                * (distanceY > 0 ? -1 : 1) - Input.mousePosition.y;


            offsetX = (transform.rotation.eulerAngles.z == 0 || transform.rotation.eulerAngles.z == 180) ? tmpX : tmpX2;
            offsetY = (transform.rotation.eulerAngles.z == 0 || transform.rotation.eulerAngles.z == 180) ? tmpY : tmpY2;
        }
        if (Mathf.Abs(offsetX) > 50 || Mathf.Abs(offsetY) > 50 || minDistance >= snapRadius)
        {
            offsetX = 0; offsetY = 0;
        }
    }
}

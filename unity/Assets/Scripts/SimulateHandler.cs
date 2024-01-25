using Assets.Scripts.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class SimulateHandler : MonoBehaviour
{
    [SerializeField] private Transform mainPanel;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            PrepareSimulation();
        });
    }

    private void PrepareSimulation()
    {  
        ComponentsDTO componentsDTO = new ComponentsDTO();
        componentsDTO.conveyorDTOs = new Dictionary<float, ConveyorDTO>();
        componentsDTO.connectionPointDTOs = new Dictionary<float, ConnectionPointDTO>();
        componentsDTO.WTDTOs = new Dictionary<float, WTDTO>();
        componentsDTO.SpawnerDTOs = new Dictionary<float, SpawnerDTO>();
        componentsDTO.RemovalDTOs = new Dictionary<float, RemovalDTO>();
        componentsDTO.WaitDTOs = new Dictionary<float, WaitDTO>();

        foreach (Transform entry in mainPanel)
        {
            float id = entry.position.x * 10f + entry.position.y;
            if (entry.tag == "Conveyor")
            {
                componentsDTO.conveyorDTOs.Add(id, new ConveyorDTO(entry.position.x, entry.position.y, entry.rotation.eulerAngles.z));
                CreateConnectionPoints(entry, ref componentsDTO);
            }
            else if (entry.tag == "WT")
            {
                float idConveyor = FindConveyor(entry);
                if (idConveyor != -1)
                {
                    componentsDTO.WTDTOs.Add(id, new WTDTO(entry.position.x, entry.position.y, entry.rotation.eulerAngles.z, idConveyor));           
                }
            }
            else if (entry.tag == "Spawner")
            {
                float idConveyor = FindConveyor(entry);
                if (idConveyor != -1)
                {
                    componentsDTO.SpawnerDTOs.Add(id, new SpawnerDTO(entry.position.x, entry.position.y, entry.rotation.eulerAngles.z, idConveyor, entry.GetComponent<MoveSpawner>().spawnFreq));
                }
            }
            else if (entry.tag == "Removal")
            {
                float idConveyor = FindConveyor(entry);
                if (idConveyor != -1)
                {
                    componentsDTO.RemovalDTOs.Add(id, new RemovalDTO(entry.position.x, entry.position.y, entry.rotation.eulerAngles.z, idConveyor, entry.GetComponent<MoveRemoval>().removalDuration));
                }
            }
            else if (entry.tag == "Wait")
            {
                float idConveyor = FindConveyor(entry);
                if (idConveyor != -1)
                {
                    componentsDTO.WaitDTOs.Add(id, new WaitDTO(entry.position.x, entry.position.y, entry.rotation.eulerAngles.z, idConveyor, entry.GetComponent<MoveWait>().waitDuration));
                }
            }
        }
        
        string json = JsonConvert.SerializeObject(componentsDTO, Newtonsoft.Json.Formatting.Indented);

        File.WriteAllText("./simulation.json", json);
    }

    private void CreateConnectionPoints(Transform entry, ref ComponentsDTO componentsDTO)
    {
        foreach (Transform child in entry)
        {
            Vector3 posConnectionPoint = child.position;
            foreach (Transform entry2 in mainPanel)
            {
                if (entry2.tag == "Conveyor" && entry2 != entry)
                {
                    foreach (Transform connectionPoint in entry2)
                    {
                        if (connectionPoint.position == posConnectionPoint && !componentsDTO.connectionPointDTOs.ContainsKey(connectionPoint.position.x * 10 + connectionPoint.position.y))
                        {
                            componentsDTO.connectionPointDTOs.Add(connectionPoint.position.x * 10 + connectionPoint.position.y,
                                new ConnectionPointDTO(posConnectionPoint.x, posConnectionPoint.y, child.eulerAngles.z,
                                entry.position.x * 10f + entry.position.y, entry2.position.x * 10f + entry2.position.y));

                            return;
                        }
                    }
                }
            }
        }
    }

    private float FindConveyor(Transform entry)
    {
        foreach (Transform entry2 in mainPanel)
        {
            if (entry2.tag == "Conveyor" && entry2 != entry)
            {
                if (entry2.position == entry.position)
                {
                    return entry2.position.x * 10 + entry2.position.y;
                }
            }
        }
        return -1f;
    }
}

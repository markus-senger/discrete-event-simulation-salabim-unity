using Assets.Scripts.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class SimulateHandler : MonoBehaviour
{
    [SerializeField] private Transform mainPanel;
    [SerializeField] private TMP_Text statusText;

    [SerializeField] private TMP_InputField simSpeedInputField;
    [SerializeField] private TMP_InputField simDurationInputField;

    private int result = -1;
    private int wtCount = 0;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            wtCount = 0;
            PrepareSimulation();
            StartSimulation();
        });
    }

    private void StartSimulation()
    {
        statusText.text = "Sim: calc";
        result = -1;
        Thread simulationThread = new Thread(() => {
                result = StartSimulationProcess("SalabimSim.exe", simSpeedInputField.text + " " + simDurationInputField.text); 
            });

        simulationThread.Start();
    }

    private int StartSimulationProcess(string executablePath, string parameters)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = executablePath,
            Arguments = parameters,
            UseShellExecute = true,
            CreateNoWindow = false 
        };

        // Start the process
        using (Process process = new Process { StartInfo = startInfo })
        {
            process.Start();
            process.WaitForExit();
            return process.ExitCode;
        }

    }

    private void PrepareSimulation()
    {  
        ComponentsDTO componentsDTO = new ComponentsDTO();
        componentsDTO.conveyorDTOs = new Dictionary<string, ConveyorDTO>();
        componentsDTO.connectionPointDTOs = new Dictionary<string, ConnectionPointDTO>();
        componentsDTO.WTDTOs = new Dictionary<string, WTDTO>();
        componentsDTO.SpawnerDTOs = new Dictionary<string, SpawnerDTO>();
        componentsDTO.RemovalDTOs = new Dictionary<string, RemovalDTO>();
        componentsDTO.WaitDTOs = new Dictionary<string, WaitDTO>();

        foreach (Transform entry in mainPanel)
        {
            float id = entry.position.x * 10f + entry.position.y;
            if (entry.tag == "Conveyor")
            {
                componentsDTO.conveyorDTOs.Add(id.ToString("F0"), new ConveyorDTO(entry.position.x, entry.position.y, entry.rotation.eulerAngles.z));
                CreateConnectionPoints(entry, ref componentsDTO);
            }
            else if (entry.tag == "WT")
            {
                float idConveyor = FindConveyor(entry);
                if (idConveyor != -1)
                {
                    wtCount++;
                    componentsDTO.WTDTOs.Add(id.ToString("F0"), new WTDTO(entry.position.x, entry.position.y, entry.rotation.eulerAngles.z, idConveyor.ToString("F0")));           
                }
            }
            else if (entry.tag == "Spawner")
            {
                float idConveyor = FindConveyor(entry);
                if (idConveyor != -1)
                {
                    componentsDTO.SpawnerDTOs.Add(id.ToString("F0"), new SpawnerDTO(entry.position.x, entry.position.y, entry.rotation.eulerAngles.z, idConveyor.ToString("F0"), entry.GetComponent<MoveSpawner>().spawnFreq));
                }
            }
            else if (entry.tag == "Removal")
            {
                float idConveyor = FindConveyor(entry);
                if (idConveyor != -1)
                {
                    componentsDTO.RemovalDTOs.Add(id.ToString("F0"), new RemovalDTO(entry.position.x, entry.position.y, entry.rotation.eulerAngles.z, idConveyor.ToString("F0"), entry.GetComponent<MoveRemoval>().removalDuration));
                }
            }
            else if (entry.tag == "Wait")
            {
                float idConveyor = FindConveyor(entry);
                if (idConveyor != -1)
                {
                    componentsDTO.WaitDTOs.Add(id.ToString("F0"), new WaitDTO(entry.position.x, entry.position.y, entry.rotation.eulerAngles.z, idConveyor.ToString("F0"), entry.GetComponent<MoveWait>().waitDuration));
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
                        if (connectionPoint.position == posConnectionPoint && !componentsDTO.connectionPointDTOs.ContainsKey((connectionPoint.position.x * 10 + connectionPoint.position.y).ToString()))
                        {
                            componentsDTO.connectionPointDTOs.Add((connectionPoint.position.x * 10 + connectionPoint.position.y).ToString("F0"),
                                new ConnectionPointDTO(posConnectionPoint.x, posConnectionPoint.y, child.eulerAngles.z,
                                (entry.position.x * 10 + entry.position.y).ToString("F0"), (entry2.position.x * 10 + entry2.position.y).ToString("F0")));

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

    private void Update()
    {
        if (result != -1)
        {
            statusText.text = "Current Output: " + result.ToString() + " | WTs: " + wtCount;
            result = -1;
        }
    }
}

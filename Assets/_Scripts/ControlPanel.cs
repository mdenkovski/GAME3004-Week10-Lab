using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ControlPanel : MonoBehaviour
{

    public List<NavMeshAgent> agents;
    public List<MonoBehaviour> scripts;

    public PlayerBehaviour player;

    public bool isGamePaused = false;

    public GameObject PauseLebelPanel;

    public PlayerDataSO playerData;

    // Start is called before the first frame update
    void Start()
    {
        agents = FindObjectsOfType<NavMeshAgent>().ToList();
        player = FindObjectOfType<PlayerBehaviour>();

        foreach (var enemy in FindObjectsOfType<EnemyBehaviour>())
        {
            scripts.Add(enemy);
        }

        scripts.Add(player);

        scripts.Add(FindObjectOfType<CameraController>());

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnLoadButtonPressed()
    {
        player.controller.enabled = false;

        player.transform.position = playerData.playerPosition;
        player.transform.rotation = playerData.playerRotation;
        player.health = playerData.playerHealth;


        player.controller.enabled = true;
    }

    public void OnSaveButtonPressed()
    {
        playerData.playerPosition = player.transform.position;
        playerData.playerRotation = player.transform.rotation;
        playerData.playerHealth = player.health;

    }

    public void OnPauseButtonToggle()
    {
        isGamePaused = !isGamePaused;

        PauseLebelPanel.SetActive(isGamePaused);

        foreach (var agent in agents)
        {
            agent.enabled = !isGamePaused;
        }

        foreach (var script in scripts)
        {
            script.enabled = !isGamePaused;
        }
    }

}

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

        LoadFromPlayerPreferences();

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

        SaveToPlayerPreferences();

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

    public void LoadFromPlayerPreferences()
    {




        playerData.playerPosition.x = PlayerPrefs.GetFloat("playerPositionX");
        playerData.playerPosition.y = PlayerPrefs.GetFloat("playerPositionY");
        playerData.playerPosition.z = PlayerPrefs.GetFloat("playerPositionZ");
                                                                             
                                                                             
        playerData.playerRotation.x = PlayerPrefs.GetFloat("playerRotationX");
        playerData.playerRotation.y = PlayerPrefs.GetFloat("playerRotationY");
        playerData.playerRotation.z = PlayerPrefs.GetFloat("playerRotationZ");
        playerData.playerRotation.w = PlayerPrefs.GetFloat("playerRotationW");

        playerData.playerHealth = PlayerPrefs.GetInt("playerHealth");

        Debug.Log("Game Loaded");

    }

    public void SaveToPlayerPreferences()
    {
        //Debug.Log(playerData.playerPosition.ToString());


        PlayerPrefs.SetFloat("playerPositionX", playerData.playerPosition.x);
        PlayerPrefs.SetFloat("playerPositionY", playerData.playerPosition.y);
        PlayerPrefs.SetFloat("playerPositionZ", playerData.playerPosition.z);

        PlayerPrefs.SetFloat("playerRotationX", playerData.playerRotation.x);
        PlayerPrefs.SetFloat("playerRotationY", playerData.playerRotation.y);
        PlayerPrefs.SetFloat("playerRotationZ", playerData.playerRotation.z);
        PlayerPrefs.SetFloat("playerRotationW", playerData.playerRotation.w);


        PlayerPrefs.SetInt("playerHealth", playerData.playerHealth);

        PlayerPrefs.Save();

        Debug.Log("Game Saved");
    }

    private void OnApplicationQuit()
    {
        SaveToPlayerPreferences();
    }
}

using System;
using UnityEngine;


[Serializable]
public class PlayerManager
{
    // This class is to manage various settings on a tank.
    // It works with the GameManager class to control how the tanks behave
    // and whether or not players have control of their tank in the 
    // different phases of the game.

    public Transform spawnPoint;                          // The position and direction the tank will have when it spawns.
    [HideInInspector]
    public int playerNumber;            // This specifies which player this the manager for.
    [HideInInspector]    
    public GameObject instance;         // A reference to the instance of the tank when it is created.
    [HideInInspector]
    public int score;

    private PlayerMovement movement;                        // Reference to tank's movement script, used to disable and enable control.
    private PlayerShooting shooting;                        // Reference to tank's shooting script, used to disable and enable control.
  

    public void Setup()
    {
        // Get references to the components.
        movement = instance.GetComponent<PlayerMovement>();
        //m_Shooting = m_Instance.GetComponent<PlayerShooting>();

        // Set the player numbers to be consistent across the scripts.
        movement.playerNumber = playerNumber;
        //m_Shooting.m_PlayerNumber = m_PlayerNumber;
    }


    // Used during the phases of the game where the player shouldn't be able to control their tank.
    public void DisableControl()
    {
        movement.enabled = false;
        //m_Shooting.enabled = false;   
    }


    // Used during the phases of the game where the player should be able to control their tank.
    public void EnableControl()
    {
        movement.enabled = true;
        //m_Shooting.enabled = true;
    }


    // Used at the start of each round to put the tank into it's default state.
    public void Reset()
    {
        instance.transform.position = spawnPoint.position;
        instance.transform.rotation = spawnPoint.rotation;

        instance.SetActive(false);
        instance.SetActive(true);
    }
}

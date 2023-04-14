using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartPositions : MonoBehaviour
{
    [SerializeField]
    private List<PlayerInput> players = new List<PlayerInput>();
    [SerializeField]
    private List<Transform> startingPoints;
    [SerializeField]
    private List<LayerMask> playerLayers;

    private PlayerInputManager playerInputManager;

    private void Awake()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
        playerInputManager.onPlayerJoined += AddPlayer;

        // Add any players that were added before this script was enabled
        foreach (var player in PlayerInput.all)
        {
            AddPlayer(player);
        }
    }




    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }


    private void AddPlayer(PlayerInput player)
    {
        
        players.Add(player);
        Debug.Log("Player Joined");
        
        player.transform.position = startingPoints[players.Count - 1].position;

        //set the layer
        player.gameObject.layer = 3;
        
    }
}
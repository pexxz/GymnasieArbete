using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField]private Transform player;
    [SerializeField] private Transform respawnPoint;


    //If the player collides with Respawn trigger, teleports player to respawnPoint
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.transform.position = respawnPoint.transform.position;
    }

}

using Photon.Pun;
using System.IO;
using UnityEngine;

public class GameSetupController : MonoBehaviour
{
    void Start()
    {
        CreatePlayer();
    }
    void CreatePlayer()
    {
        int spawnPicker = Random.Range(0, GameSetup.GS.spawnPoints.Length);
        //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"), Vector3.zero, Quaternion.identity);

        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"),
                GameSetup.GS.spawnPoints[spawnPicker].position, GameSetup.GS.spawnPoints[spawnPicker].rotation, 0);
    }
    public void RepositionPlayer(Transform player)
    {
        int spawnPicker = Random.Range(0, GameSetup.GS.spawnPoints.Length);
        Transform spawnPoint = GameSetup.GS.spawnPoints[spawnPicker];

        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;
    }
}

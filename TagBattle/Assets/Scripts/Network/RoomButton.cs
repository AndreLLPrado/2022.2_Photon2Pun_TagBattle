using Photon.Pun;
using UnityEngine.UI;
using UnityEngine;

public class RoomButton : MonoBehaviour
{
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text sizeText;

    private string roomName;
    private int roomSize;
    private int playerCount;

    public void JoinOnClick()
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void SetRoom(string nameInput, int sizeInput, int coutInput)
    {
        roomName = nameInput;
        roomSize = sizeInput;
        playerCount = coutInput;
        nameText.text = nameInput;
        sizeText.text = coutInput + "/" + sizeInput;
    }
}

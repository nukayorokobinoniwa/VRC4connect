
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;
using TMPro;

public class PlayerNameButton : UdonSharpBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI playertext;
    string playerdata;

    public void Init(){
        playerdata = "player";
        playertext.text = playerdata;
    }

    void Start()
    {
        Init();


    }

    public void Pressed(){
        Debug.Log("押したことは反応しています。");
        VRCPlayerApi player = Networking.LocalPlayer;
        if(player != null){
            playerdata = player.displayName;
            Debug.Log(playerdata);
            playertext.text = playerdata;
        }
    }

    public void ChangeOwner()
    {
        if (!Networking.IsOwner(Networking.LocalPlayer, this.gameObject)) Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
    }
}


using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using TMPro;

public class Reset : UdonSharpBehaviour
{
    [SerializeField]private CheckWin checkwin;
    [SerializeField]private PushCounter pushcounter;
    [SerializeField]private PopBlock popblock1;
    [SerializeField]private PopBlock popblock2;
    [SerializeField]private PopBlock popblock3;
    [SerializeField]private PopBlock popblock4;
    [SerializeField]private PopBlock popblock5;
    [SerializeField]private PopBlock popblock6;
    [SerializeField]private PopBlock popblock7;
    [SerializeField]private PlayerNameButton pn1;
    [SerializeField]private PlayerNameButton pn2;
    [SerializeField] TextMeshProUGUI turn;
    [SerializeField] TextMeshProUGUI number;
    [SerializeField] TextMeshProUGUI winner;
    
    [UdonSynced] private bool resetFlag;

    public override void Interact()
    {
        resetFlag = true; // リセットフラグを立てる
        RequestSerialization(); // 状態を同期
        PerformReset();
    }

    public void PerformReset(){
        checkwin.Init();
        pushcounter.Init();
        popblock1.Init();
        popblock2.Init();
        popblock3.Init();
        popblock4.Init();
        popblock5.Init();
        popblock6.Init();
        popblock7.Init();
        pn1.Init();
        pn2.Init();
        turn.text = "現在は\n青の番です。";
        number.text = "現在0個の\nブロックが\n置かれている。";
        winner.text = "勝敗";
    }

    public override void OnDeserialization()
    {
        if (resetFlag)
        {
            PerformReset(); // 他プレイヤーがリセットを行った場合もリセットを実行
            resetFlag = false; // リセットフラグを解除
        }
    }

}

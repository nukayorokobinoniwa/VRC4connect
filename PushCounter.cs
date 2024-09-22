using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using TMPro;

public class PushCounter : UdonSharpBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    public int pushtimes;

    public void Init(){
        pushtimes = 0;
    }

    void Start()
    {
        Init();
    }

    public void IncreaseCount(){
        pushtimes += 1;
        Debug.Log(pushtimes);
        text.text = "現在"+pushtimes+"個の\nブロックが\n置かれている。";
    }

    public int PlayerCheck(){
        if(pushtimes %2 == 0){
            return 1;//偶数回のプレイヤー
        }else{
            return 2;//奇数回のプレイヤー
        }
    }

    public int returnPT(){
        return pushtimes;
    }
}

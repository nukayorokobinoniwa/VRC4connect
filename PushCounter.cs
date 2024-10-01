using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using TMPro;

public class PushCounter : UdonSharpBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [UdonSynced]public int pushtimes;

    public TextureChange textureSwitcher;
    public TextureChange textureSwitcher1;
    public TextureChange textureSwitcher2;
    public TextureChange textureSwitcher3;
    public TextureChange textureSwitcher4;
    public TextureChange textureSwitcher5;
    public TextureChange textureSwitcher6;

    public void Init(){
        pushtimes = 0;
        RequestSerialization();
        textureSwitcher.UpdateTexture();
        textureSwitcher1.UpdateTexture();
        textureSwitcher2.UpdateTexture();
        textureSwitcher3.UpdateTexture();
        textureSwitcher4.UpdateTexture();
        textureSwitcher5.UpdateTexture();
        textureSwitcher6.UpdateTexture();
    }

    void Start()
    {
        Init();
    }

    public void IncreaseCount(){
        pushtimes += 1;
        Debug.Log(pushtimes);
        text.text = "現在"+pushtimes+"個の\nブロックが\n置かれている。";
        RequestSerialization();

        textureSwitcher.UpdateTexture();
        textureSwitcher1.UpdateTexture();
        textureSwitcher2.UpdateTexture();
        textureSwitcher3.UpdateTexture();
        textureSwitcher4.UpdateTexture();
        textureSwitcher5.UpdateTexture();
        textureSwitcher6.UpdateTexture();
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
    
    public override void OnDeserialization(){
        Debug.Log("これは呼び出されている。");
        text.text = "現在" + pushtimes + "個の\nブロックが\n置かれている。";
    }
}

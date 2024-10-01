using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class TextureChange : UdonSharpBehaviour
{
    public PushCounter pushCounter;

    public Material textureblue;
    public Material texturered;

    // オブジェクトのRenderer
    public Renderer objectRenderer;

    

    // 現在のテクスチャのインデックス

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

    if (objectRenderer == null)
    {
        Debug.LogError("Rendererが見つかりません。TextureChangeスクリプトはRendererを持つオブジェクトにアタッチしてください。");
    }

    if (textureblue == null || texturered == null)
    {
        Debug.LogError("テクスチャが設定されていません。インスペクターでtextureblueとtextureredを設定してください。");
    }

    UpdateTexture(); // 初期状態のテクスチャを設定
    Debug.Log("Renderer と マテリアルの設定が正しく行われました。");
    }

    // タップ（インタラクション）時に呼ばれる関数
    public void UpdateTexture()
    {
         if (pushCounter == null)
    {
        Debug.LogError("PushCounterが設定されていません。インスペクターでpushCounterを設定してください。");
        return; // スクリプトの実行を停止
    }

    Debug.Log("UpdateTextureは呼び出されています。");
    // PushCounterのPlayerCheck()を使ってテクスチャを切り替える
    if (objectRenderer != null) // Rendererがnullでないことを確認
        {
            // PushCounterのPlayerCheck()を使ってテクスチャを切り替える
            if (pushCounter.PlayerCheck() == 1)
            {
                objectRenderer.material = textureblue;
            }
            else
            {
                objectRenderer.material = texturered;
            }
        }
        else
        {
            Debug.LogError("Rendererがnullです。");
        }
    }
}

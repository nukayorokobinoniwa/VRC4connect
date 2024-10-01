
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class syncBlock : UdonSharpBehaviour
{
    public GameObject prefab;  // 同期するPrefab
    private VRCPlayerApi localPlayer;

    void Start()
    {
        localPlayer = Networking.LocalPlayer;
    }

    public void SpawnPrefab(Vector3 position, Quaternion rotation)
    {
        if (Networking.IsOwner(localPlayer, gameObject))
        {
            GameObject spawnedPrefab = Object.Instantiate(prefab);  // Prefabをインスタンス化
            spawnedPrefab.transform.position = position;
            spawnedPrefab.transform.rotation = rotation;

            Networking.SetOwner(localPlayer, spawnedPrefab);  // オーナーシップを設定
            RequestSerialization();  // オブジェクトの状態を同期
        }
    }

    public override void OnDeserialization()
    {
        // Prefabの状態が同期されると呼ばれるメソッド
    }
}


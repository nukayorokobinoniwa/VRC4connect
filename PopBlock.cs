using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using TMPro;


public class PopBlock : UdonSharpBehaviour
{
    [SerializeField]private PushCounter pushCounter;
    [SerializeField]private CheckWin checkwin;
    [SerializeField] private GameObject blue;//[SerializeField] でunityでもいじれるように設定。ここでGameObjectのblueを設定。 
    [SerializeField] private GameObject red;//[SerializeField] でunityでもいじれるように設定。ここでGameObjectのredを設定。 
    [SerializeField] TextMeshProUGUI text;
    private int mypushtimes = 0;
    GameObject go;
    private GameObject[] popedObjects = new GameObject[6]; // 初期サイズを設定

    public void Init(){
        Destroygo();
        mypushtimes = 0;
    }


    public override void Interact()// overrideは親クラスのコードを上書きするもの。実質的にはpublic void Interact()となる。
    {
        if(mypushtimes < 6 && checkwin.Cwinner() == 0){
            int x;
            int y;
            int nowcheck = pushCounter.PlayerCheck();
            Vector3 newposition = new Vector3(transform.position.x,5,3);//1以上5未満の整数を生成
            if(nowcheck==1){
                go = Instantiate(blue,newposition,Quaternion.identity);//rotationまで書かないといけない。
                text.text = "現在は\n赤の番です。";
            }else{
                go = Instantiate(red,newposition,Quaternion.identity);
                text.text = "現在は\n青の番です。";
            }
            popedObjects[mypushtimes] = go;
            x =(int)transform.position.x+1;
            y = mypushtimes;
            checkwin.PutBoard(x,y,nowcheck);
            mypushtimes += 1;
            if (pushCounter != null){
                pushCounter.IncreaseCount();
            }
            else{
                Debug.LogError("PushCounter is not assigned. Please assign it in the Inspector.");
            }
            //勝敗の確認
            if(checkwin.CheckBoard(x,y) == 1){
                checkwin.Dwinner(1);
                Debug.Log("青の勝ち");
                checkwin.bluewin();
            }else if(checkwin.CheckBoard(x,y) == 2){
                checkwin.Dwinner(2);
                Debug.Log("赤の勝ち");
                checkwin.redwin();
            }else if(pushCounter.returnPT() >= 42){
                checkwin.drow();
            }else{
                Debug.Log("続行");
            }
        }else{
            Debug.Log("The maximum number of presses has already been reached");
        }
    }
    public void Destroygo(){
        for(int i = 0; i<mypushtimes; i++){
            if(popedObjects[i]!=null){
                Destroy(popedObjects[i]);
            }
        }
    }
}


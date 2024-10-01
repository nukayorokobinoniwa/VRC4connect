//CheckWin関数では架空配列にて勝者の判定を行うためのコード。
//具体的には架空配列の初期化、架空配列にプレイヤーの数字を追加、架空配列における勝者判定。
//追加してTMPの勝者の部分を変更するためのコードもこちらに記載している、

using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using TMPro;//TextMeshProを使用するために使用する名前空間

public class CheckWin : UdonSharpBehaviour
{
    [SerializeField] TextMeshProUGUI text;//[SerializeField]はインスペクターウィンドウにてオブジェクトをアタッチするために必要。
    [UdonSynced] public int[] board;//[UdonSynced]はプレイヤー間で同期させることのできる変数。インスタンスに後から入ってきたプレイヤーにも同期させるために必要なもの。
    [UdonSynced] public int winner;

    //初期化関数。Resetボタンを押したときにも使えるようにStartではなくInitとして書く。
    public void Init(){
        winner = 0;//winnerは1か2で表記。0は未決着のときのもの。
        board = new int[42];//コネクト4は7×6マスのため配列は42。UdonSharpは2次元配列が使用不可能なため仕方なく1次元配列を使用。
        //配列の初期化
        for (int i = 0; i < board.Length ; i++){
            board[i] = 0;
            Debug.Log(board[i]);
        }
    }
    //初期化関数。これは自動で呼び出される。
    void Start(){
        Init();
    }

    //Debug.Logでいまの配列の様子を見るためのコード \nは改行を意味する。
    public void SeeBoard(){
        Debug.Log("6行目"+board[35]+","+board[36]+","+board[37]+","+board[38]+","+board[39]+","+board[40]+","+board[41]+"\n5行目"+board[28]+","+board[29]+","+board[30]+","+board[31]+","+board[32]+","+board[33]+","+board[34]+"\n4行目"+board[21]+","+board[22]+","+board[23]+","+board[24]+","+board[25]+","+board[26]+","+board[27]+"\n3行目"+board[14]+","+board[15]+","+board[16]+","+board[17]+","+board[18]+","+board[19]+","+board[20]+"\n2行目"+board[7]+","+board[8]+","+board[9]+","+board[10]+","+board[11]+","+board[12]+","+board[13]+"\n1行目"+board[0]+","+board[1]+","+board[2]+","+board[3]+","+board[4]+","+board[5]+","+board[6]);
    }

    //プレイヤーがボタンを押したときに架空配列内に同じ挙動を反映させるためのコード。
    public void PutBoard(int x,int y,int playercheck){
        if(playercheck == 1){
            board[x+7*y] = 1;
        }else{
            board[x+7*y] = 2;
        }
        SeeBoard();
        RequestSerialization();//https://qiita.com/Dakumasu/items/357d9bacda428fb15596に詳細は書いてある。そのコードを行ったときに他のプレイヤーにも同期変数を同期させるためのコード。
    }

    //勝者がいるかどうかを確認するための関数。チェックするのは最後に追加した部分において4つつながってる部分があるかどうかの話、
    public int CheckBoard(int x,int y){
        int j;
        int k;
        int player = board[x+7*y];//
        Debug.Log("playerは"+player);
        //まずは縦をチェックする。縦は重力の関係上yが4以上の時のみ発動可能。
        if(y >=3){
            if(board[x+7*(y-1)]==player &&board[x+7*(y-2)]==player&&board[x+7*(y-3)]==player){
                return player;
            }
        }
        Debug.Log("縦のチェックが終了");
        //次に横をチェックする。
        int yoko = 1;
            j = x;
            while(j-1>=0 && board[j-1+7*y]==player){
                Debug.Log("横＋１");
                yoko = yoko + 1;
                j--;
            }
            k = x;
            while(k+1<=6 && board[k+1+7*y]==player){
                Debug.Log("横＋１");
                yoko = yoko + 1;
                k++;
            }
            if(yoko >=4 ){
                return player;
            }
            Debug.Log("横のチェック終了");
        //次に左ななめをチェックする。
        int Lnaname = 1;
        j = x;
        k = y;
        while(j-1 >=0 && k+1 <= 5 && board[j-1+7*(k+1)]==player){
            Debug.Log("左斜め＋１");
            Lnaname = Lnaname + 1;
            j--;
            k++;
        }
        j = x;
        k = y;
        while(j+1 <=6 && k-1 >= 0 && board[j+1+7*(k-1)]==player){
            Debug.Log("左斜め＋１");
            Lnaname = Lnaname + 1;
            j++;
            k--;
        }
        if(Lnaname >=4){
            return player;
        }
        Debug.Log(Lnaname+"より左斜めのチェック終了");
        //最後に右斜めをチェックする。
        int Rnaname = 1;
        j = x;
        k = y;
        while(j-1 >=0 && k-1 >= 0 && board[j-1+7*(k-1)]==player){
            Debug.Log("右斜め＋１");
            Rnaname = Rnaname + 1;
            j--;
            k--;
        }
        j = x;
        k = y;
        while(j+1 <=6 && k+1 <= 5 && board[j+1+7*(k+1)]==player){
            Debug.Log("右斜め＋１");
            Rnaname = Rnaname + 1;
            j++;
            k++;
        }
        if(Rnaname >=4){
            return player;
        }
        Debug.Log(Rnaname+"より右斜めのチェック終了");
        //４か所連続がないとき0を返す。
        return 0;
    }

    // Dwinnerはwinnerを決定する。　Decide winnerの略。
    public void Dwinner(int i){
        winner = i;
        RequestSerialization();
    }
    //CwinnerはCheckwinneの略
    public int Cwinner(){
        return winner;
    }
    //青陣営(player1)勝利時にTMPを変更するコード
    public void bluewin(){
        text.text = "青陣営の勝利";
    }
    //赤陣営(player2)勝利時にTMPを変更するコード
    public void redwin(){
        text.text = "赤陣営の勝利";
    }
    //引き分けが確定したときにTMPを変更するコード
    public void drow(){
        text.text = "引き分け";
    }

    //同期変数が変更されたときに呼び出されるコード。結局使わなかったため一時的にコメントアウトしている。
    /*public override void OnDeserialization(){
        SeeBoard();
    }*/
}

/*
 * 파일명    : GameMgr.cs
 * 작성자    : 방준선
 * 목적      : 게임 매니저(턴 관리)
*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class GameMgr : MonoBehaviour
{
    public Text TextTurn; //현재 턴을 보여주기 위한 텍스트
    public Text TextShow; //승자를 보여주기 위한 텍스트
    public Text Player1HPText; //P1의 체력 텍스트
    public Text Player2HPText; //P2의 체력 텍스트    
    public PlayerInfo player1, player2;
    public int showturn; //턴
    turnend turnEnd; //턴종료 스크립트
    public GameObject turnButton; //턴종료버튼
    public GameObject turnButtonSpawnPoint; //턴종료버튼 생성위치
    //public static List<GameObject> fieldCard;
    bool mousemove; //마우스 드래그 확인용

    CubeScript test; //필드카드


    // 게임 진행 상황.
    private enum GameProgress
    {
        None = 0,       // 시합 시작 전.
        Start,          // 시합 시작.
        Turn,           // 시합 중.
        Result,         // 결과 표시.
        GameOver,       // 게임 종료.
        Disconnect,     // 연결 끊기. 서버연동시
    };

    // 턴 종류.
    private enum Turn
    {
        MyTurn = 0,        // 자신의 턴.
        YourTurn,       // 상대의 턴.
    };

    // 마크.
    private enum Mark
    {
        Player1 = 0,
        Player2,
    };

    // 시합 결과.
    private enum Winner
    {
        None = 0,       // 시합 중.
        Player1,         // P1승리.
        Player2          // P2승리.
    };

    // 시합 시작 전의 대기 시간.
    private const float waitTime = 5.0f;

    // 진행 상황.
    private GameProgress progress;

    // 현재의 턴.
    private Mark turn;

    // 로컬 기호.
    private Mark myTurn;

    // 리모트 기호.
    private Mark enemyTurn;

    // 승자.
    private Winner winner;

    // 게임 종료 플래그.
    private bool isGameOver;

    // 대기 시간.
    private float currentTime;
    RaycastHit hit;


    Ray ray;

    void Awake()
    {
        player1 = GameObject.Find("P1").GetComponent<PlayerInfo>();
        player2 = GameObject.Find("P2").GetComponent<PlayerInfo>();
    }
    
    // Use this for initialization
    void Start()
    {
        // 게임을 초기화합니다.
        Reset();
        isGameOver = false;
        GameStart();

        //fieldCard = new List<GameObject>(5);
    }

    // Update is called once per frame
    void Update()
    {
        switch (progress)
        {
            case GameProgress.Start:
                //Debug.Log("업데이트 레디");
                UpdateStart();
                break;

            case GameProgress.Turn:
                //Debug.Log("업데이트 턴");
                UpdateTurn();
                break;

            case GameProgress.GameOver:
                UpdateGameOver();
                break;
        }
    }

    // 
    void OnGUI()
    {
        switch (progress)
        {
            case GameProgress.Start:
                //();
                break;

            case GameProgress.Turn:
                // 남은 시간을 그립니다.

                /*if (turn == myTurn)
                {
                    GUISkin skin = GUI.skin;
                    GUIStyle style = new GUIStyle(GUI.skin.GetStyle("button"));
                    style.normal.textColor = Color.white;
                    style.fontSize = 25;

                    if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 100), "턴종료", style))
                    {
                        bool setMark = false;
                        setMark = DoOwnTurn();
                    }
                }*/
                break;

            case GameProgress.Result:
                // 승자를 표시합니다.
                DrawWinner();
                break;

            case GameProgress.GameOver:
                // 승자를 표시합니다.
                DrawWinner();
                break;

            default:
                break;
        }

    }

    void UpdateStart()
    {
        // 시합 시작 신호 표시를 기다립니다.
        TextShow.text = "마음의 준비를 하는 중";
        currentTime += Time.deltaTime;

        if (currentTime > waitTime)
        {
            // 표시가 끝나면 게임 시작입니다.
            progress = GameProgress.Turn;
            currentTime = 0f;
            TextShow.text = "";
            GameObject temp = Instantiate(turnButton, turnButtonSpawnPoint.transform.position, turnButtonSpawnPoint.transform.rotation) as GameObject;
            turnEnd = temp.GetComponent<turnend>();

        }

    }

    void UpdateTurn()
    {

        if (turn == myTurn) //내턴일 경우
        {
            DoOwnTurn();// setMark = DoOwnTurn();
        }
        if (turn != myTurn) //상대방 턴
        {
            DoOppnentTurn();
        }

        // 승자 체크
        winner = CheckHP();
        if (winner != Winner.None)
        {
            // 게임 종료.
            progress = GameProgress.Result;
        }

    }

    // 게임 종료 처리
    void UpdateGameOver()
    {
        Reset();
        isGameOver = true;
    }

    // 자신의 턴일 때의 처리.
    void DoOwnTurn()
    { //RaycastHit 3D
        
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("마우스다운" + player2.HP);
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {

                Debug.Log("내턴" + player2.HP);
                if (Input.mousePosition.y > Screen.height / 2)
                {
                    Debug.Log("상대껄 왜 건드려");
                }
                if (Input.mousePosition.y < Screen.height / 2)
                {
                    Debug.Log("하단부");
                    if (hit.collider.tag == "Turn") //클릭한 대상이 턴버튼
                    {
                        Debug.Log("턴 버튼 클릭!!");
                        player1.HP = player1.HP + Random.Range(0, 3);
                        player2.HP = player2.HP - Random.Range(0, 10);
                        TextTurn.text = showturn + "턴";
                        turn = (turn == Mark.Player1) ? Mark.Player2 : Mark.Player1;
                        showturn++;
                        Player1HPText.text = System.Convert.ToString(player1.HP);
                        Player2HPText.text = System.Convert.ToString(player2.HP);
                        turnEnd.flip = true;
                        turnEnd.test();
                        Debug.Log(turnButton.GetComponent<turnend>().flip);
                    }
                    if (hit.collider.tag == "Field_Card") //클릭한 대상이 턴버튼
                    {
                        test = GameObject.FindWithTag("Field_Card").GetComponent<CubeScript>();
                        GameObject temp = hit.transform.gameObject;
                        test.FieldMouseDown(temp);

                        mousemove = true;
                        // test.hitPosition = hit.point;
                        //test.isMoveState = true;
                    }
                    else
                    {
                        //Debug.Log("으후루꾸꾸루후으으후루꾸꾸루후으으후루꾸꾸루후으으후루꾸꾸루후으으후루꾸꾸루후으으후루꾸꾸루후으");
                    }
                }

            }
        }
        else
        {
            if (mousemove)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    test = GameObject.FindWithTag("Field_Card").GetComponent<CubeScript>();
                    GameObject temp = hit.transform.gameObject;
                    test.FieldUpdate(temp);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            mousemove = false;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Field_Card") //클릭한 대상이 턴버튼
                {
                    test = GameObject.FindWithTag("Field_Card").GetComponent<CubeScript>();
                    GameObject temp =hit.collider.gameObject;
                    test.FieldMouseUp(temp);

                    // test.hitPosition = hit.point;
                    //test.isMoveState = true;
                }
            }
        }
            return;

    }

    // 상대의 턴일 때의 처리. 현재는 1초 대기
    void DoOppnentTurn()
    {

        currentTime += Time.deltaTime;

        if (currentTime > waitTime)
        {
            Debug.Log("상대방턴");
            TextTurn.text = showturn + "턴";
            player2.HP = player2.HP + Random.Range(0, 3);
            player1.HP = player1.HP - Random.Range(0, 10);
            turn = (turn == Mark.Player1) ? Mark.Player2 : Mark.Player1;
            showturn++;
            currentTime = 0f;
            Player1HPText.text = System.Convert.ToString(player1.HP);
            Player2HPText.text = System.Convert.ToString(player2.HP);
            turnEnd.flip = true;
            turnEnd.test();
        }
    }

    // 체력 체크.
    Winner CheckHP()
    {
        if (player1.HP <= 0)
        {
            return Winner.Player2;
        }
        else if (player2.HP <= 0)
        {
            return Winner.Player1;
        }
        return Winner.None;
    }

    // 게임 리셋.
    void Reset()
    {
        //turn = Turn.Own;
        showturn = 1;
        turn = Mark.Player1;
        progress = GameProgress.None;
    }

    // 결과 표시.
    void DrawWinner()
    {
        //"승리하였습니다."
        TextShow.text = winner + "가 승리하였습니다.";
    }

    // 게임 시작.
    public void GameStart()
    {
        Debug.Log("시작");
        // 게임 시작 상태로 합니다.
        progress = GameProgress.Start;

        // 서버가 먼저 하게 설정합니다.
        turn = Mark.Player1;

        // 자신과 상대의 기호를 설정합니다.
        //if (m_transport.IsServer() == true) //서버용
        myTurn = Mark.Player1;
        enemyTurn = Mark.Player2;

        // 이전 설정을 클리어합니다.
        isGameOver = false;

    }

    // 게임 종료 체크.
    public bool IsGameOver()
    {
        return isGameOver;
    }
}
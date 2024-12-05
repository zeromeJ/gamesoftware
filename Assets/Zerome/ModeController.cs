using System;
using UnityEngine;
using UnityEngine.UI;

public class ModeController : MonoBehaviour
{
    [SerializeField] private GameObject modeSelectPanel; // 모드 선택 패널
    [SerializeField] private CountDown countdown; // CountDown 스크립트
    [SerializeField] private TigerGame tigerGame; // TigerGame controller


    [SerializeField] private Button easyModeBtn; // 이지 모드 버튼
    [SerializeField] private Button hardModeBtn; // hard 모드 버튼

    void Start()
    {
        SelectMode();
    }

    void SelectMode()
    {
        easyModeBtn.onClick.AddListener(() => tigerGame.InitializeGame(false));
        hardModeBtn.onClick.AddListener(() => tigerGame.InitializeGame(true));
        easyModeBtn.onClick.AddListener(delegate { Debug.Log("clicked easy button"); });
        hardModeBtn.onClick.AddListener(delegate { Debug.Log("clicked hard button"); });
    }
    //public void SelectMode(bool isHardMode)
    //{

        //    // 모드 선택 시 실행
        //    modeSelectPanel.SetActive(false); // 모드 선택 패널 숨기기

        //    //tigerGame.InitializeGame(isHardMode);

        //    Debug.Log("발");

        //    //// 카운트다운 완료 후 게임 시작
        //    //countdown.OnCountdownComplete = () =>
        //    //{
        //    //    tigerGame.InitializeGame(isHardMode); // TigerGame 초기화
        //    //};

        //    // 카운트다운 시작
        //    countdown.StopAndGo();
        //}
    }

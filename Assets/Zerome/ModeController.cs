using UnityEngine;
using UnityEngine.UI;

public class ModeController : MonoBehaviour
{
    public TigerGame tigerGame; // TigerGame controller
    public CountDown countdown; // CountDown 스크립트

    [SerializeField] private GameObject modeSelectPannel; // 모드 선택 패널
    [SerializeField] private Button easyModeBtn; // 이지 모드 버튼
    [SerializeField] private Button hardModeBtn; // hard 모드 버튼

    public void SelectMode()
    {
        countdown.GameStop();
        modeSelectPannel.SetActive(true);
        easyModeBtn.onClick.AddListener(() => {
            TigerGame.isHardMode = false;
            tigerGame.InitializeGame(false);
        });
        hardModeBtn.onClick.AddListener(() => {
            TigerGame.isHardMode = true;
            tigerGame.InitializeGame(true);
        });
    }
 }

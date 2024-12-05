using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class FeverController : MonoBehaviour
{
    
    public TigerGame tigerObject;

    public Button[] feverCards; // 9개의 버튼
    [SerializeField] Sprite tigerSprite; // 호랑이 이미지


    public void FeverTimer()
    {
        Debug.Log(" fever timer on ");
        StartCoroutine(FiveSecondTimer());
    }


    private IEnumerator FiveSecondTimer()
    {
        Debug.Log("Fever started!"); // 타이머 시작 로그
        tigerObject.AssignTiger();
        for (int i = 5; i > 0; i--)
        {
            if (tigerObject.feverTimeText != null)
            {
                tigerObject.feverTimeText.text = i.ToString(); // UI에 숫자 표시
            }
            Debug.Log($"Timer: {i}"); // 디버그 출력
            yield return new WaitForSeconds(1f); // 1초 대기
        }


        Debug.Log("Fever completed!");

        OnTimerComplete(); // 5초 후 작업 실행
        yield break; // 코루틴 종료
    }

    private void OnTimerComplete()
    {

        Debug.Log(" count down Complete ");
        tigerObject.bonusScore = tigerObject.tigerCurrentScore;
        tigerObject.ControlFeverObj(false);
        tigerObject.AssignRandomAnimals();
        TigerGame.isFever = false; // 피버 타임 종료 후 플래그 초기화
    }
}

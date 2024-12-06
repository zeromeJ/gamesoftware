using UnityEngine;

public class HeartController : MonoBehaviour
{
    [SerializeField] private GameObject heartContainer; // heart 컨테이너
    [SerializeField] private GameObject brokenHeart2; // 깨진 하트 이미지
    [SerializeField] private GameObject brokenHeart1; // 깨진 하트 이미지 
    [SerializeField] private GameObject brokenHeart0; // 깨진 하트 이미지

    public void ControllLivesUI(int lives)
    {
        if (lives == 2) brokenHeart2.SetActive(true);
        if (lives == 1) brokenHeart1.SetActive(true);
        if (lives == 0) brokenHeart0.SetActive(true);
    }
}

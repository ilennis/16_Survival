using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance;

    [SerializeField] TextMeshProUGUI notificationText;  // 알림 메시지 텍스트
    [SerializeField] Image notificationObject;  // 알림 메시지 오브젝트
    [SerializeField] CanvasGroup canvasGroup;

    public float fadeDuration = 0.5f; // 페이드 인/아웃 시간
    public float moveDistance = 50f; // 위로 이동할 거리
    public float displayDuration = 2f; // 표시 유지 시간

    private float startY; // 초기 Y 좌표를 저장할 변수

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        startY = notificationObject.transform.localPosition.y;
        notificationObject.gameObject.SetActive(false); // 처음에는 숨김
    }

    //인벤토리에 아이템 추가 불가능 알림
    public void ShowFullInventory()
    {
        ShowNotification("<color=red>인벤토리가 꽉 찼습니다!</color>");
    }

    //아이템 획득알림
    public void ShowAddItem(string itemName,int amount)
    {
        ShowNotification($"<color=green>{itemName}</color>{amount}개 획득!");
    }

    public void ShowNotification(string message)
    {
        notificationObject.gameObject.SetActive(true);
        notificationText.text = message;

        // 기존 애니메이션이 실행 중이라면 중지
        notificationObject.DOKill();
        notificationObject.transform.DOKill();
        canvasGroup.DOKill();

        canvasGroup.alpha = 0;
        ResetNotificationObjectPos();

        Vector3 endPos = notificationObject.transform.localPosition + new Vector3(0, moveDistance, 0);

        //DOTween 시퀀스 실행
        Sequence sequence = DOTween.Sequence();
        sequence.Append(canvasGroup.DOFade(1, fadeDuration));
        sequence.Join(notificationObject.transform.DOLocalMove(endPos, fadeDuration));
        sequence.AppendInterval(displayDuration);
        sequence.Append(canvasGroup.DOFade(0, fadeDuration)).OnComplete(() =>
        {
            notificationObject.gameObject.SetActive(false);
            ResetNotificationObjectPos();
        });
    }

    private void ResetNotificationObjectPos()
    {
        notificationObject.transform.localPosition = new Vector3(notificationText.transform.localPosition.x, startY, notificationText.transform.localPosition.z);
    }
}

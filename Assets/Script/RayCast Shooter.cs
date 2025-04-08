using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RayCastShooter : MonoBehaviour
{

    public ParticleSystem flashEffect;                  // 발사 이펙트 변수 선언

    // 탄창 관련 변수 선언
    public int magazinCapacity = 30;                    // 탄찬의 크기
    private int currentAmmo;                             // 현재 총알 개수

    public TextMeshProUGUI ammoUI;                      // 총알 개수를 나타낼 TextMeshProUGUI 선언

    // 재장전 기능 변수 선언
    public Image reloadingUI;                             //재장전 UI
    public float reloadingTIme = 2f;                    // 재장전 시간
    private float timer = 0;                            // 시간 확인용 타이머
    private bool isReloading = false;                   // 재장전 확인용 bool변수

    // 사운드 출력 기능 변수 선언
    public AudioSource audioSource;                     // 오디오 소스
    public AudioClip audioClip;                         // 오디오 클립

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = magazinCapacity;                          // 현재 총알의 갯수를 탄창의 크기만큼 넣기
        // ammoUI.text = currentAmmo + "/" + magazinCapacity;
        ammoUI.text = $"{currentAmmo}/{magazinCapacity}";       // 현재 총알 개수를 UI 표시
        reloadingUI.gameObject.SetActive(false);                // 재장전 UI 비활성화
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && currentAmmo > 0 && isReloading == false)      // 마우스 좌클릭(0번)이 눌리고 현재 총알이 0보다 크면서 재장전이 아닐때
        {
            audioSource.PlayOneShot(audioClip);                 // 발사 사운드 재생
            currentAmmo--;                                      // 총알 1개 감소
            flashEffect.Play();                                 // 이펙트 재생
            ammoUI.text = $"{currentAmmo}/{magazinCapacity}";       // 현재 총알 개수를 UI 표시(총알 소비 후 표시!!!)
            ShootRay();                                         // 레일 발사 함수 호출
        }

        if(Input.GetKeyDown(KeyCode.R))                         // R키를 누르면
        {
            isReloading = true;                                 // 재장전 중으로 변경
            reloadingUI.gameObject.SetActive(true);             // 재장전 UI 활성화
        }


        if (isReloading == true)
        {
            Reloading();
        }
    }

    // 레이가 발사되는 함수
    void ShootRay()
    {
        Ray ray = new Ray(transform.position, transform.forward);    // 발사할 Ray의 시작점, 방향 지정( 메인카메라에서 마우스 커서 방향으로 발사)
        RaycastHit hit;                                                 // Ray를 맞은 대상의 정보를 저장한 저장소

        if(Physics.Raycast(ray, out hit))                               // Raycast의 반환형은 bool로, Ray를 맞았다면 ture 반환
        {
            Destroy(hit.collider.gameObject);                           // 맞은 대상 오브젝트 제거
        }
    }

    void Reloading()
    {
        timer += Time.deltaTime;

        reloadingUI.fillAmount = timer / reloadingTIme;                 // 재장전 UI의 fill 값을 현재 진행률로 업데이트

        if(timer >= reloadingTIme)                                      // 재장전 시간이 지났을 경우
        {
            timer = 0;
            isReloading = false;                                        // 재장전이 완료 됐음을 표시
            currentAmmo = magazinCapacity;                              // 총알을 채워준다.
            ammoUI.text = $"{currentAmmo}/{magazinCapacity}";           // 현재 총알 개수를 표시
            reloadingUI.gameObject.SetActive(false);                    // 재장전 UI 오브젝트를 비활성화
        }
    }
}

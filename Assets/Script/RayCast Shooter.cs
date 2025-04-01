using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastShooter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))     // 마우스 좌클릭(0번)이 눌릴때
        {
            ShootRay();                 //레일 발사 함수 호출
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
}

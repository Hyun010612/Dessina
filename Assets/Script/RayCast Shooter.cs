using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RayCastShooter : MonoBehaviour
{

    public ParticleSystem flashEffect;                  // �߻� ����Ʈ ���� ����

    // źâ ���� ���� ����
    public int magazinCapacity = 30;                    // ź���� ũ��
    private int currentAmmo;                             // ���� �Ѿ� ����

    public TextMeshProUGUI ammoUI;                      // �Ѿ� ������ ��Ÿ�� TextMeshProUGUI ����

    // ������ ��� ���� ����
    public Image reloadingUI;                             //������ UI
    public float reloadingTIme = 2f;                    // ������ �ð�
    private float timer = 0;                            // �ð� Ȯ�ο� Ÿ�̸�
    private bool isReloading = false;                   // ������ Ȯ�ο� bool����

    // ���� ��� ��� ���� ����
    public AudioSource audioSource;                     // ����� �ҽ�
    public AudioClip audioClip;                         // ����� Ŭ��

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = magazinCapacity;                          // ���� �Ѿ��� ������ źâ�� ũ�⸸ŭ �ֱ�
        // ammoUI.text = currentAmmo + "/" + magazinCapacity;
        ammoUI.text = $"{currentAmmo}/{magazinCapacity}";       // ���� �Ѿ� ������ UI ǥ��
        reloadingUI.gameObject.SetActive(false);                // ������ UI ��Ȱ��ȭ
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && currentAmmo > 0 && isReloading == false)      // ���콺 ��Ŭ��(0��)�� ������ ���� �Ѿ��� 0���� ũ�鼭 �������� �ƴҶ�
        {
            audioSource.PlayOneShot(audioClip);                 // �߻� ���� ���
            currentAmmo--;                                      // �Ѿ� 1�� ����
            flashEffect.Play();                                 // ����Ʈ ���
            ammoUI.text = $"{currentAmmo}/{magazinCapacity}";       // ���� �Ѿ� ������ UI ǥ��(�Ѿ� �Һ� �� ǥ��!!!)
            ShootRay();                                         // ���� �߻� �Լ� ȣ��
        }

        if(Input.GetKeyDown(KeyCode.R))                         // RŰ�� ������
        {
            isReloading = true;                                 // ������ ������ ����
            reloadingUI.gameObject.SetActive(true);             // ������ UI Ȱ��ȭ
        }


        if (isReloading == true)
        {
            Reloading();
        }
    }

    // ���̰� �߻�Ǵ� �Լ�
    void ShootRay()
    {
        Ray ray = new Ray(transform.position, transform.forward);    // �߻��� Ray�� ������, ���� ����( ����ī�޶󿡼� ���콺 Ŀ�� �������� �߻�)
        RaycastHit hit;                                                 // Ray�� ���� ����� ������ ������ �����

        if(Physics.Raycast(ray, out hit))                               // Raycast�� ��ȯ���� bool��, Ray�� �¾Ҵٸ� ture ��ȯ
        {
            Destroy(hit.collider.gameObject);                           // ���� ��� ������Ʈ ����
        }
    }

    void Reloading()
    {
        timer += Time.deltaTime;

        reloadingUI.fillAmount = timer / reloadingTIme;                 // ������ UI�� fill ���� ���� ������� ������Ʈ

        if(timer >= reloadingTIme)                                      // ������ �ð��� ������ ���
        {
            timer = 0;
            isReloading = false;                                        // �������� �Ϸ� ������ ǥ��
            currentAmmo = magazinCapacity;                              // �Ѿ��� ä���ش�.
            ammoUI.text = $"{currentAmmo}/{magazinCapacity}";           // ���� �Ѿ� ������ ǥ��
            reloadingUI.gameObject.SetActive(false);                    // ������ UI ������Ʈ�� ��Ȱ��ȭ
        }
    }
}

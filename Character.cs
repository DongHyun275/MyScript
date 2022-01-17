using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class Character : MonoBehaviour
{
    public NavMeshAgent nav;
    public JoyStick sick;
    public Animator animator;
    public Transform movePoint;
    float sum = 0f;


    public Vector3 GETDIRPOS
    {
        get
        {
            return transform.position + sick.DIR * 10f;
        }
    }

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        movePoint = GameObject.FindGameObjectWithTag("MovePoint").transform;
    }

    void Start()
    {
        transform.position = new Vector3(68.42f, 0.11f, 11.01f);
    }

    void Update()
    {
		// ���̽�ƽ���� �̿��� �� �ڵ�
		//nav.destination = GETDIRPOS;

		// ���콺 Ŭ�� �̵��ҽ� �ڵ�
        Vector3 pos = Input.mousePosition; //ĳ���Ͱ� ��ǥ������ �����ϸ� movePoint �� ��Ȱ��ȭ�Ѵ�.
        if (nav.remainingDistance == 0.0f)
	    {
            movePoint.gameObject.SetActive(false);
            animator.SetInteger("Cha_Ani", 0);
        }
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetInteger("Cha_Ani", 1);
            Ray ray;
            ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                movePoint.position = hitInfo.point;
                movePoint.gameObject.SetActive(true);
                nav.SetDestination(hitInfo.point);
                animator.SetInteger("Cha_Ani", 1);
            }
        }

    }
}

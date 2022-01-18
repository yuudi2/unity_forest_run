using UnityEngine;
using System.Collections;

public class Bee_v2 : MonoBehaviour
{
	public Transform target;
	public Vector3 direction;
	public float velocity;
	public float accelaration;


	// Update is called once per frame
	void Update()
	{
		MoveToTarget();
		Move();
	}
	private void Move()
	{
		if (target.transform.position.x > transform.position.x)
			transform.localScale = new Vector3(-0.7f, 0.7f, 1);
		else
			transform.localScale = new Vector3(0.7f, 0.7f, 1);
	}
	public void MoveToTarget()
	{
		// Player의 현재 위치를 받아오는 Object
		target = GameObject.Find("Player").transform;
		// Player의 위치와 이 객체의 위치를 빼고 단위 벡터화 한다.
		direction = (target.position - transform.position).normalized;
		// 가속도 지정 (추후 힘과 질량, 거리 등 계산해서 수정할 것)
		accelaration = 0.05f;
		// 초가 아닌 한 프레임으로 가속도 계산하여 속도 증가
		velocity = (velocity + accelaration * Time.deltaTime);
		// Player와 객체 간의 거리 계산
		float distance = Vector3.Distance(target.position, transform.position);
		// 일정거리 안에 있을 시, 해당 방향으로 무빙
		if (distance <= 10.0f)
		{
			this.transform.position = new Vector3(transform.position.x + (direction.x * velocity),
												   transform.position.y + (direction.y * velocity),
													 transform.position.z);
		}
		// 일정거리 밖에 있을 시, 속도 초기화 
		else
		{
			velocity = 0.0f;
		}
	}
}
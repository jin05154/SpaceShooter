using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 몬스터가 출현할 위치정보를 저장할 리스트
    public List<Transform> points = new List<Transform>();
    // 몬스터 prefab를 저장할 변수
    public GameObject monsterPrefab;
    // 몬스터의 생성 주기
    public float createTime = 3.0f;

    private bool isGameOver = false;

    // 외부에서 접근 가능한 properties
    public bool IsGameOver
    {
        // 외부에서 값 read
        get{
            return isGameOver;
        }
        // 외부에서 값 write
        set{
            isGameOver = value;
            if (isGameOver == true)
            {
                //CancelInvoke("CreateMonster");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // 1. hierarchy view에서 SpawnPointGroup 이름을 갖는 게임오브젝트를 검색
        GameObject spg = GameObject.Find("SpawnPointGroup");
        // 2. SpawnPointGroup 하위에 있는 모든 Transform 컴포넌트를 추출
        spg.GetComponentsInChildren<Transform>(points);

        // Resources 폴더에 있는 Monster Prefab Loading...
        monsterPrefab = Resources.Load<GameObject>("Monster");

        // 몬스터 생성 함수를 반복 호출
        //InvokeRepeating("CreateMonster", 2.0f, createTime);
        StartCoroutine(CreateMonster());
    }

    IEnumerator CreateMonster()
    {
        yield return new WaitForSeconds(2.0f);

        while (!isGameOver)
        {
            // 위치값을 추출
            int idx = Random.Range(0, points.Count);
            // 몬스터 생성
            Instantiate(monsterPrefab, points[idx].position, Quaternion.identity);

            yield return new WaitForSeconds(createTime);  // 3초 간격으로 호출
        }
    }
}

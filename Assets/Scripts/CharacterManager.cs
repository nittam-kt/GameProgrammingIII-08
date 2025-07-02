using UnityEngine;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] GameObject characterPrefab;
    [SerializeField] Vector2 generateRange;
    [SerializeField] int characterNumber;

    public List<Character> characterList { get; private set; } = new List<Character>();

    void Start()
    {
        for (int i = 0; i < characterNumber; i++)
        {
            // キャラクターをインスタンス化
            var obj = Instantiate(characterPrefab, transform);

            // 範囲内にランダムに配置
            var offset = new Vector3(
                generateRange.x * Random.Range(-1f, 1f), 0,
                generateRange.y * Random.Range(-1f, 1f));
            obj.transform.position = transform.position + offset;
            obj.transform.rotation = Quaternion.Euler(0f, Random.Range(0, 360), 0f);

            // 自身を登録
            var ch = obj.GetComponent<Character>();
            ch.manager = this;
            characterList.Add(ch);
        }
    }

    void Update()
    {
    }
}

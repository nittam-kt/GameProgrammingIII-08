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
            // �L�����N�^�[���C���X�^���X��
            var obj = Instantiate(characterPrefab, transform);

            // �͈͓��Ƀ����_���ɔz�u
            var offset = new Vector3(
                generateRange.x * Random.Range(-1f, 1f), 0,
                generateRange.y * Random.Range(-1f, 1f));
            obj.transform.position = transform.position + offset;
            obj.transform.rotation = Quaternion.Euler(0f, Random.Range(0, 360), 0f);

            // ���g��o�^
            var ch = obj.GetComponent<Character>();
            ch.manager = this;
            characterList.Add(ch);
        }
    }

    void Update()
    {
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "ResourceNodeData", menuName = "Scriptable Object/ResourceNodeData", order = int.MaxValue)]
public class ResourceNodeData : ScriptableObject
{
    public int Hp;     //채집 오브젝트의 체력 or 채집 가능한 자원개수  (체력/무기의 채집데미지 = 채집가능한횟수)
    public int Yield; //채집할때 얻는 자원개수
}


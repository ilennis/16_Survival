public enum ItemType
{
    Equip,       //무기
    Food,        //음식
    Water,        //물
    Wood,      //나무
    Iron,         // 철
    Rock,        //돌
    Pill,           //약
    Syringe,   //주사기
    Vaccine    //백신
}
public enum FoodEffectType
{
    HpRecovery,            //체력 회복
    HungerRecovery,    //허기 회복
    ThirstRecovery,      //갈증 회복
    MaxHpBoost,           //최대 체력 상승
    DamageBoost,         //공격력 상승
    MaxStaminaBoost, //최대 스태미너 상승
    SpeedBoost,           // 스피드 상승
}

//레시피 해방 조건
public enum ConditionType
{
    PlayerLevel //플레이어 레벨
}

public enum DamageType
{
    Resource,
    Enemy
}


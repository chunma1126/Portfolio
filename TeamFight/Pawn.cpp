#include "Pawn.h"

#pragma execution_character_set("utf-8")

bool Pawn::init()
{
    if (!Entity::init())
    {
        return false;
    }

    initAnimationSheet("Characters/Knights/Troops/Pawn/Red/Pawn_Red.png", 6, 6);

    _statController->setDefaultStat(STAT_TYPE::ATK, 10);
    _statController->setDefaultStat(STAT_TYPE::SPD, 5);
    _statController->setDefaultStat(STAT_TYPE::DEF, 20);
    _statController->setDefaultStat(STAT_TYPE::HP, 25);

    _healthBar->initMaxHealthBar(_statController->getValue(STAT_TYPE::HP));

    {
        MeleeAttackSkill* meleeAttackSkill = new MeleeAttackSkill();
        meleeAttackSkill->setSkillName("���Ը� ������ �Ͷ�");
        meleeAttackSkill->setPower(10);
        meleeAttackSkill->setType(SKILL_TYPE::DAMAGE);
        meleeAttackSkill->setIconPath("SkillPack/skill_icon_00.png");
        meleeAttackSkill->setDescription("������ ���� ����..!");
        _skillList.push_back(meleeAttackSkill);
    }
    
    {

        MeleeAttackSkill* meleeAttackSkill = new MeleeAttackSkill();
        meleeAttackSkill->setSkillName("���Ը� ������ �Ͷ�");
        meleeAttackSkill->setPower(10);
        meleeAttackSkill->setType(SKILL_TYPE::DAMAGE);
        meleeAttackSkill->setIconPath("SkillPack/skill_icon_00.png");
        meleeAttackSkill->setDescription("������ ���� ����..!");
        _skillList.push_back(meleeAttackSkill);
    }



    return true;
}

void Pawn::update(float dt)
{
}

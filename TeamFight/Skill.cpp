#include "Skill.h"
#include "Entity.h"

void Skill::applyDamage(Entity* caster, Entity* target)
{
	if (_type == SKILL_TYPE::DAMAGE)
	{
		float damage = caster->getStatController()->getValue(STAT_TYPE::ATK);
		damage += _power;
		//���� ������ �ؾ���.
		target->getStatController()->removeStat(STAT_TYPE::HP, damage);
	}

	if (_type == SKILL_TYPE::HEAL)
	{
		float healAmount = caster->getStatController()->getValue(STAT_TYPE::ATK);
		healAmount += _power;
		target->getStatController()->addStat(STAT_TYPE::HP, healAmount);
	}

}

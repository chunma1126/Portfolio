#include "HealReward.h"

#pragma execution_character_set("utf-8")

HealReward::HealReward()
{
	_iconIndexList.push_back({4,4});
	_iconIndexList.push_back({5,4});
	_iconIndexList.push_back({6,4});

	setDescription("������ ȸ���ϴ� ���ִ� ����ũ �Դϴ�. +", _healAmount);
}

void HealReward::execute()
{
	for (auto& entity : _team->getAliveEntities())
	{
		entity->getStatController()->addStat(STAT_TYPE::HP ,_healAmount);
	}

}

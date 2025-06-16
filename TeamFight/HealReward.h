#pragma once
#include "Reward.h"
class HealReward : public Reward
{
public:
	HealReward();
	void execute() override;
private:
	int _healAmount = 5;
};


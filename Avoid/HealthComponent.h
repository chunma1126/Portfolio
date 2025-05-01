#pragma once
#include "Action.h"

class HealthComponent
{
public:
	HealthComponent();
	virtual ~HealthComponent();

	void takeDamage(int _damage);
	void takeHeal(int _heal);
	void setInvincibility(bool _isInvincibility) { isInvincibility = _isInvincibility; }

	float getPercent() 
	{
		return float(currentHp) / float(maxHp);
	};

	Action<> onDamageEvents;
	Action<> onHealEvents;
	Action<> onDeadEvents;
private:
	void init();
	void dead();

private:
	int maxHp = 10;
	int currentHp = 1;

	bool isInvincibility;
	bool isDead = false;
};
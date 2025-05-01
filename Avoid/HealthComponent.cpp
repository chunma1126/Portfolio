#include "HealthComponent.h"
#include "Action.h"
#include "cocos2d.h"

HealthComponent::HealthComponent()
{
	init();
}

HealthComponent::~HealthComponent()
{

}

void HealthComponent::init()
{
	currentHp = maxHp;
}

void HealthComponent::takeDamage(int _damage)
{
	if (isInvincibility)return;

	currentHp -= _damage;

	onDamageEvents.invoke();


	if (currentHp <= 0) 
	{
		dead();
	}
}

void HealthComponent::takeHeal(int _heal)
{
	currentHp += _heal;
	onHealEvents.invoke();

	if (currentHp >= maxHp)
		currentHp = maxHp;
}

void HealthComponent::dead()
{
	if (isDead)return;

	isDead = true;
	onDeadEvents.invoke();
}
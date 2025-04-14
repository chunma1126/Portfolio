#pragma once
#include "Enemy.h"
class wonBoss : public Enemy
{
public:
	wonBoss();
	~wonBoss();
public:
	void Update() override;
	void Render(HDC _hdc) override;
public:
	virtual void EnterCollision(Collider* _other);
	virtual void StayCollision(Collider* _other);
	virtual void ExitCollision(Collider* _other);
private:
	int m_hp;
};


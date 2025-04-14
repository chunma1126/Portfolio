#pragma once
#include "Enemy.h"
#include "BulletManager.h"
class TrashMob1 : public Enemy
{
public:
	TrashMob1(const wstring& _key, const wstring& _path);
	void Update() override;


private:
	float m_shotTime = 0.5f;
	float m_shotTimer = 0;
	int cnt = 0;
	float arr[5] = { 60, 25, 30, 25, 60 };

	float stayTime = 0.75f;
	float stayTimer = 0;

};


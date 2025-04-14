#pragma once
#include "Enemy.h"
class StateMachine;
class MidBoss : public Enemy
{
public:
	MidBoss(const wstring& _key, const wstring& _path);
	~MidBoss();
	void Update() override;
	void Render(HDC _hdc) override;
public:
	void ChangeState(MidBossState state);
	Scene* GetCurrentScene() { return m_curScene; }
	const Vec2& GetMidBossPos() { return GetPos(); }
private:
	StateMachine* m_stateMachine = nullptr;

private:
	bool allowShot = true;
};


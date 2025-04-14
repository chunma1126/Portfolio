#pragma once
#include "Projectile.h"
#include "Object.h"

class FollowProjectile :public Projectile
{
public:
	FollowProjectile();

	void Update() override;
	void SetTarget(Object* _target) { m_target = _target; }
	void SetChangeTime(float _time) { m_followChangeTime = _time; }
private:
	Object* m_target;
	Vec2 m_targetDir;
	bool m_isFollowing;
	float m_followChangeTime;
	float m_changeTimer;
};
	

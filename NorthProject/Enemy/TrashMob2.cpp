#include "pch.h"
#include "TrashMob2.h"
#include "EventManager.h"
#include "TimeManager.h"
#include "ResourceManager.h"
#include "Animator.h"
#include "Collider.h"
#include "BulletManager.h"
#include "SceneManager.h"

TrashMob2::TrashMob2(const wstring& _key, const wstring& _path)
	: Enemy(_key, _path)
{
	AddComponent<Animator>();
	GetComponent<Animator>()->CreateAnimation(L"Enemy_2", m_texture, { 0,0 }, { 48,48 }, { 48,0 }, 4, 0.2f);
	GetComponent<Animator>()->PlayAnimation(L"Enemy_2", true);
	GetComponent<Animator>()->SetSize({ 4,4 });

	GetComponent<Collider>()->SetSize({ 160,125 });

	m_shotTime = 0.1f;
}


void TrashMob2::Update()
{
	Enemy::Update();

	if (m_isDead)return;

	if (m_shotTimer >= m_shotTime)
	{
		GET_SINGLE(BulletManager)->BasicShot(m_vPos, m_curScene, 400, { 0,1 });
		m_shotTimer = 0;
	}

	Vec2 pos = GetPos();

	if (pos.y >= SCREEN_HEIGHT / 2 - 200) 
	{
		stayTimer += fDT;
		if (stayTimer >= stayTime - 1.f)
		{
			dirRight = pos.x < SCREEN_WIDTH / 2 ? false : true;

			if (dirRight)
			{
				pos.x += 100 * fDT;
			}
			else
			{
				pos.x -= 100 * fDT;
			}
		}
	}
	else
	{
		float speed = 55;
		float zigzagAmplitude = 100;
		float zigzagFrequency = 1.5f;
		float time = GET_SINGLE(TimeManager)->GetTime();

		pos.x += zigzagAmplitude * cos(time * zigzagFrequency) * fDT;
		pos.y += speed * fDT;
	}

    SetPos(pos);
}

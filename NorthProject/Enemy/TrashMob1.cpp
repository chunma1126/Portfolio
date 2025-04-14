#include "pch.h"
#include "TrashMob1.h"
#include "EventManager.h"
#include "TimeManager.h"
#include "ResourceManager.h"
#include "Animator.h"
#include "Collider.h"
#include "BulletManager.h"
#include "SceneManager.h"


TrashMob1::TrashMob1(const wstring& _key, const wstring& _path)
	: Enemy(_key, _path)
{
	AddComponent<Animator>();
	GetComponent<Animator>()->CreateAnimation(L"Enemy_1", m_texture, { 0,58 }, { 48,48 }, { 48,0 }, 4, 0.2f);
	GetComponent<Animator>()->PlayAnimation(L"Enemy_1", true);
	GetComponent<Animator>()->SetSize({ 4,4 });
	GetComponent<Collider>()->SetSize({ 125,125 });

}



void TrashMob1::Update()
{
	Enemy::Update();
	
	if (m_isDead)return;

	Vec2 curPos = GetPos();

	if (curPos.y > SCREEN_HEIGHT / 2)
	{
		stayTimer += fDT;

		if (stayTimer >= stayTime)
		{
			dirRight = curPos.x < SCREEN_WIDTH / 2 ? false : true;
			
			if (dirRight)
			{
				curPos.x += 100 * fDT;
			}
			else
			{
				curPos.x -= 100 * fDT;
			}
		}
	}
	else
	{
		
		curPos.y += 150 * fDT;
		
	}
	SetPos(curPos);
	m_shotTimer += fDT;

	if (m_shotTimer >= m_shotTime)
	{
		m_shotTimer = 0;
		int idx = cnt % 5;
		float angle = arr[idx];
		cnt++;
		GET_SINGLE(BulletManager)->CircleShot({ GetPos().x ,GetPos().y + 50 }, m_curScene, angle, 400);
	}



}


	 



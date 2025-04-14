#include "pch.h"
#include "FollowProjectile.h"
#include "EventManager.h"
#include "TimeManager.h"
#include "ResourceManager.h"
#include "Projectile.h"
#include "Animator.h"
FollowProjectile::FollowProjectile()
{
	m_pTex = GET_SINGLE(ResourceManager)->TextureLoad(L"EnemyProjectile" , L"Texture\\EnemyProjectile.bmp");

	AddComponent<Animator>();
	GetComponent<Animator>()->SetSize({ 1,1 });
	GetComponent<Animator>()->CreateAnimation(L"EnemyBullet", m_pTex, { 0,0 }, { 24.f,29.f }, { 24,0 }, 8, 0.08f, false);
	GetComponent<Animator>()->PlayAnimation(L"EnemyBullet", true);
}
void FollowProjectile::Update()
{
	Projectile::Update();
	m_changeTimer += fDT;

	if (m_changeTimer >= m_followChangeTime && m_isFollowing == false)
	{
		m_isFollowing = true;

		Vec2 vPos = GetPos();
		Vec2 targetPos = m_target->GetPos();

		m_targetDir = targetPos - vPos;
	}

	if (m_isFollowing)
	{
		Vec2 vPos = GetPos();

		m_targetDir.Normalize();

		vPos.x += m_targetDir.x * m_speed * 1.5f * fDT;
		vPos.y += m_targetDir.y * m_speed * 1.5f * fDT;

		SetPos(vPos);
	}
	else
	{
		Vec2 vPos = GetPos();
		vPos.x += m_vDir.x * m_speed * fDT;
		vPos.y += m_vDir.y * m_speed * fDT;
		SetPos(vPos);
		Vec2 vSize = GetSize();
	}

	if (m_vPos.y < -m_vSize.y || m_vPos.y > SCREEN_HEIGHT + m_vSize.y ||
		m_vPos.x < -m_vSize.x || m_vPos.x > SCREEN_WIDTH + m_vSize.x)
	{
		GET_SINGLE(EventManager)->DeleteObject(this);
	}
}

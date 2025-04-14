#include "pch.h"
#include "TrashMob3.h"
#include "Collider.h"
#include "EventManager.h"
#include "TimeManager.h"
#include "Animator.h"
#include "BulletManager.h"
#include "HealthComponent.h"
#include "BossScene.h"
TrashMob3::TrashMob3(const wstring& _key, const wstring& _path)
	: Enemy(_key, _path)
{
    additionalItemDropPercentage = 100;
	AddComponent<Animator>();
	GetComponent<Animator>()->CreateAnimation(L"Enemy_3", m_texture, { 0,106 }, { 48,31 }, { 48,0 }, 4, 0.1f);
	GetComponent<Animator>()->PlayAnimation(L"Enemy_3", true);
	GetComponent<Animator>()->SetSize({ 4,4 });

	GetComponent<Collider>()->SetSize({ 125,125 });

	m_shotTime = 0.95f;
    m_center = { SCREEN_WIDTH / 2.f , SCREEN_HEIGHT * 0.25f };
}


void TrashMob3::Update()
{
    Enemy::Update();

    if (m_isDead)return;

    m_angle += fDT * 2.0f; 
    if (m_angle >= 2 * M_PI)
        m_angle -= 2 * M_PI;

    float offsetX = m_radius * cos(m_angle);
    float offsetY = m_radius * sin(m_angle);

    Vec2 pos = m_center + Vec2{offsetX , offsetY};
    SetPos(pos);
    if (m_shotTime <= m_shotTimer)
    {
        m_shotTimer = 0;
        if (m_player == nullptr)
        {
            vector<Object*> player = m_curScene->GetLayerObjects(LAYER::PLAYER);
            m_player = player[0];
        }
        GET_SINGLE(BulletManager)->CircleShotGoToTarget(GetPos() , m_curScene , 45, 120 , m_player, 2.f);
    }
}

void TrashMob3::OnDead()
{
    Enemy::OnDead();
    GET_SINGLE(EventManager)->SetBossEnter(true);
}


#include "pch.h"
#include "MidBoss.h"
#include "BulletManager.h"
#include "SceneManager.h"
#include "EventManager.h"
#include "TimeManager.h"
#include "ResourceManager.h"
#include "StateMachine.h"
#include "State.h"
#include "Texture.h"
#include "HealthComponent.h"
#include "Collider.h"
#include "UIManager.h"
MidBoss::MidBoss(const wstring& _key, const wstring& _path)
	: Enemy(_key, _path)
{
	m_stateMachine = new StateMachine(this);
	GetComponent<Collider>()->SetSize({ 525,250 });
	GetComponent<Collider>()->SetOffSetPos({ 25,0 });

	m_texture = GET_SINGLE(ResourceManager)->TextureLoad(L"Boss", L"Texture\\Boss.bmp");
	GET_SINGLE(ResourceManager)->LoadSound(L"Clear", L"Sound\\Clear.wav", false);
}

MidBoss::~MidBoss()
{
	GET_SINGLE(UIManager)->OnCompleteBossScene();
	GET_SINGLE(ResourceManager)->PlayAudio(L"Clear");
	delete m_stateMachine;
}

void MidBoss::Update()
{
	if (GET_SINGLE(EventManager)->GetPlayerDead())return;

	Enemy::Update();
	Vec2 curPos = GetPos();
	m_stateMachine->UpdateState();

}
void MidBoss::Render(HDC _hdc)
{
	Vec2 vPos = GetPos();
	Vec2 vSize = GetSize();

	int width = m_texture->GetWidth();
	int height = m_texture->GetHeight();

	::TransparentBlt(_hdc
		, (int)(vPos.x - width / 2) - 8
		, (int)(vPos.y - height / 2)
		, width + vSize.x / 2, height + vSize.y / 2,
		m_texture->GetTexDC()
		, 0, 0, width, height, RGB(255, 0, 255));
	this->ComponentRender(_hdc);
}
void MidBoss::ChangeState(MidBossState state)
{
	m_stateMachine->ChangeState(state);
}

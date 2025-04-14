#include "pch.h"
#include "Projectile.h"
#include "TimeManager.h"
#include "Texture.h"
#include "ResourceManager.h"
#include "Collider.h"
#include "EventManager.h"
#include "Enemy.h"
#include "Animator.h"
Projectile::Projectile()
//	: m_dir(-1.f)
	: m_angle(0.f)
	, m_vDir(0.f, 1.f)
	, m_speed(0)
{
	//m_pTex = new Texture;
	//wstring path = GET_SINGLE(ResourceManager)->GetResPath();
	//path += L"Texture\\Bullet.bmp";
	//m_pTex->Load(path);
	m_pTex = GET_SINGLE(ResourceManager)->TextureLoad(L"Projectile", L"Texture\\PlayerProjectile.bmp");
	this->AddComponent<Collider>();
	GetComponent<Collider>()->SetSize({ 20.f,20.f });

	AddComponent<Animator>();
	GetComponent<Animator>()->CreateAnimation(L"Projectile", m_pTex, { 0,0 }, { 24,24 }, {24 , 0} , 4 , 0.1f , false);
	GetComponent<Animator>()->SetSize({2,2});
	GetComponent<Animator>()->PlayAnimation(L"Projectile" , true);

	GET_SINGLE(ResourceManager)->LoadSound(L"Hit", L"Sound\\Hit.wav", false);

}
Projectile::Projectile(const wstring& _key, const wstring& _path)
	: m_angle(0.f)
	, m_vDir(0.f, 1.f)
	, m_speed(0)
{
	m_pTex = GET_SINGLE(ResourceManager)->TextureLoad(_key, _path);
	this->AddComponent<Collider>();
	GetComponent<Collider>()->SetSize({ 20.f,20.f });

	AddComponent<Animator>();
	GetComponent<Animator>()->CreateAnimation(L"Bullet", m_pTex, { 0,0 }, { 24,24 }, { 24 , 0 }, 4, 0.04f, false);
	GetComponent<Animator>()->PlayAnimation(L"Bullet", true);
}

void Projectile::Update()
{
	if (GET_SINGLE(EventManager)->GetPlayerDead())
	{
		GET_SINGLE(EventManager)->DeleteObject(this);
	}


	Vec2 vPos = GetPos();

	vPos.x += m_vDir.x * m_speed * fDT;
	vPos.y += m_vDir.y * m_speed * fDT;
	SetPos(vPos);


	if (m_vPos.y < -m_vSize.y || m_vPos.y > SCREEN_HEIGHT + m_vSize.y ||
		m_vPos.x < -m_vSize.x || m_vPos.x > SCREEN_WIDTH + m_vSize.x)
	{
		GET_SINGLE(EventManager)->DeleteObject(this);
	}

}

void Projectile::Render(HDC _hdc)
{
	ComponentRender(_hdc);
}

void Projectile::EnterCollision(Collider* _other)
{
	Object* pOtherObj = _other->GetOwner();
	TagEnum otherTag = pOtherObj->GetTag();
	TagEnum myTag = this->GetTag();
	
	switch (myTag)
	{
		case TagEnum::EnemyProjectile:
			if (otherTag == TagEnum::Player)
			{
				GET_SINGLE(ResourceManager)->PlayAudio(L"Hit");
			}
			break;
		case TagEnum::PlayerProjectile:
			if (otherTag == TagEnum::Enemy)
			{
				GET_SINGLE(ResourceManager)->PlayAudio(L"Hit");
				GET_SINGLE(EventManager)->DeleteObject(this);
			}
			break;
	}
}

void Projectile::StayCollision(Collider* _other)
{
}

void Projectile::ExitCollision(Collider* _other)
{
}

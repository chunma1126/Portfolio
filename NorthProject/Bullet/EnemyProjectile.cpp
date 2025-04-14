#include "pch.h"
#include "EnemyProjectile.h"
#include "ResourceManager.h"
#include "Animator.h"
#include "EventManager.h"
EnemyProjectile::EnemyProjectile()
{
	m_pTex = GET_SINGLE(ResourceManager)->TextureLoad(L"EnemyBullet",L"Texture\\EnemyProjectile.bmp");

	AddComponent<Animator>();
	GetComponent<Animator>()->SetSize({1,1});
	GetComponent<Animator>()->CreateAnimation(L"EnemyBullet", m_pTex, { 0,0 }, { 24.f,29.f }, {24,0} , 8 ,0.08f, false);
	GetComponent<Animator>()->PlayAnimation(L"EnemyBullet" , true);

}



EnemyProjectile::~EnemyProjectile()
{
}

void EnemyProjectile::Render(HDC _hdc)
{
	ComponentRender(_hdc);
}

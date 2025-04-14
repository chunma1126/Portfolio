#include "pch.h"
#include "EnemyCircleOut.h"
#include "Animator.h"
EnemyCircleOut::EnemyCircleOut(const wstring& _key, const wstring& _path)
	:Enemy(_key, _path)
{
	//this->AddComponent<Animator>();
	//GetComponent<Animator>()->CreateAnimation(L"CircleOutAnim", m_texture, {});
}
EnemyCircleOut::~EnemyCircleOut()
{
}

